using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using UnityEngine;
using System.IO;
using System;

public static class SaveSystem
{
    private static bool isInitialized = false;
    private static string localPath => Application.persistentDataPath + $"/{CurrentUsername}_savedata.json";
    private static string _cachedUsername;

    public static string CurrentUsername
    {
        get
        {
            if (string.IsNullOrEmpty(_cachedUsername))
            {
                _cachedUsername = PlayerPrefs.GetString("username", "default");
            }
            return _cachedUsername;
        }
        set
        {
            _cachedUsername = value;
            PlayerPrefs.SetString("username", value);
            PlayerPrefs.Save();
        }
    }

    public static async Task InitializeServicesAsync()
    {
        if (isInitialized) return;
        try
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            CurrentUsername = PlayerPrefs.GetString("username", "default");
            isInitialized = true;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error inicializando Cloud Save: " + ex.Message);
        }
    }

    public static void DeleteLocalData()
    {
        if (File.Exists(localPath))
            File.Delete(localPath);
    }

    public static async Task SaveScoreAsync(int score)
    {
        SaveLocal(score);

        await InitializeServicesAsync();

        try
        {
            int cloudScore = await LoadHighScoreFromCloudAsync();
            if (score > cloudScore)
            {
                var data = new Dictionary<string, object>
            {
                { $"highScore_{CurrentUsername}", score }
            };
                await CloudSaveService.Instance.Data.Player.SaveAsync(data);
            }
        }
        catch (Exception ex)
        {
            int localScore = LoadLocal();
            if (localScore > 0)
            {
                var data = new Dictionary<string, object>
            {
                { $"highScore_{CurrentUsername}", localScore }
            };
                await CloudSaveService.Instance.Data.Player.SaveAsync(data);
            }
        }
    }

    public static async Task<int> LoadHighScoreAsync()
    {
        await InitializeServicesAsync();

        try { return await LoadHighScoreFromCloudAsync(); }
        catch { return LoadLocal(); }
    }

    private static async Task<int> LoadHighScoreFromCloudAsync()
    {
        var key = $"highScore_{CurrentUsername}";
        var keys = new HashSet<string> { key };
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

        if (data.TryGetValue(key, out var value))
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
        }
    }

    private static int LoadLocal()
    {
        if (!File.Exists(localPath)) return 0;
        string json = File.ReadAllText(localPath);
        GameData data = JsonUtility.FromJson<GameData>(json);
        return data.highScore;
    }
}