using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string saveFilePath = Application.persistentDataPath + "/savedata.json";

    public static void SaveScore(int score)
    {
        int currentHighScore = 0;
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            currentHighScore = data.highScore;
        }
        if (score > currentHighScore)
        {
            GameData data = new GameData
            {
                highScore = score
            };

            string newJson = JsonUtility.ToJson(data, true);
            File.WriteAllText(saveFilePath, newJson);
        }
    }

    public static GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);

            GameData data = JsonUtility.FromJson<GameData>(json);
            return data;
        }
        else return null;
    }

    public static void DeleteAllData()
    {
        if (File.Exists(saveFilePath)) File.Delete(saveFilePath);
    }
}