using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using Unity.Services.CloudSave;

public class UserManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI warningText;

    private void Start()
    {
        string savedUsername = PlayerPrefs.GetString("username", "");
        QualitySetter.Apply();
        if (!string.IsNullOrEmpty(savedUsername))
        {
            SaveSystem.CurrentUsername = savedUsername;
            SceneManagementUtils.LoadSceneByName("MainMenu");
        }
    }

    public async void SendUser()
    {
        string username = inputField.text.Trim();

        if (string.IsNullOrEmpty(username))
        {
            warningText.text = "Username can't be empty";
            return;
        }

        await SaveSystem.InitializeServicesAsync();

        string key = $"highScore_{username}";
        var keys = new HashSet<string> { key };

        try
        {
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);
            if (data.ContainsKey(key))
                warningText.text = $"User finded. LoginIn: {username}";
            else
                warningText.text = $"New user created: {username}";

            SaveSystem.CurrentUsername = username;
            PlayerPrefs.SetString("username", username);
            PlayerPrefs.Save();

            SceneManagementUtils.LoadSceneByName("MainMenu");
        }
        catch (Exception) { warningText.text = "Error connecting to the cloud"; }
    }
}
