using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    private static bool isInitialized = false;
    private static string localPath = Application.persistentDataPath + "/savedata.json";

    public static async Task InitializeServicesAsync()
    {
        if (isInitialized) return;
        try
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            isInitialized = true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error inicializando Cloud Save: " + ex.Message);
        }
    }

    public static async Task SaveScoreAsync(int score)
    {
        SaveLocal(score);
        await InitializeServicesAsync();

        try
        {
            int currentHighScore = await LoadHighScoreFromCloudAsync();
            if (score > currentHighScore)
            {
                var data = new Dictionary<string, object>
                {
                    { "highScore", score }
                };
                await CloudSaveService.Instance.Data.Player.SaveAsync(data);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error guardando en la nube: " + ex.Message);
        }
    }

    public static async Task<int> LoadHighScoreAsync()
    {
        await InitializeServicesAsync();

        try
        {
            return await LoadHighScoreFromCloudAsync();
        }
        catch
        {
            Debug.LogWarning("Fallo al cargar desde la nube, cargando local.");
            return LoadLocal();
        }
    }

    private static async Task<int> LoadHighScoreFromCloudAsync()
    {
        var keys = new HashSet<string> { "highScore" };
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

        if (data.TryGetValue("highScore", out var value))
            return value.Value.GetAs<int>();

        return 0;
    }

    private static void SaveLocal(int score)
    {
        int currentLocal = LoadLocal();
        if (score > currentLocal)
        {
            GameData data = new GameData { highScore = score };
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(localPath, json);
            Debug.Log("Guardado en local.");
        }
    }

    private static int LoadLocal()
    {
        if (!File.Exists(localPath)) return 0;
        string json = File.ReadAllText(localPath);
        GameData data = JsonUtility.FromJson<GameData>(json);
        return data.highScore;
    }

    public static void DeleteLocal()
    {
        if (File.Exists(localPath)) File.Delete(localPath);
    }
}