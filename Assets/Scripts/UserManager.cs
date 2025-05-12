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
        Debug.Log("Username desde PlayerPrefs al iniciar: " + savedUsername);
        if (!string.IsNullOrEmpty(savedUsername))
        {
            SaveSystem.CurrentUsername = savedUsername;
            Debug.Log("Cargando usuario guardado: " + SaveSystem.CurrentUsername);
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
            {
                warningText.text = $"Usuario encontrado. Logeado como: {username}";
            }
            else
            {
                warningText.text = $"Nuevo usuario creado: {username}";
            }

            SaveSystem.CurrentUsername = username;
            PlayerPrefs.SetString("username", username);
            PlayerPrefs.Save();

            SceneManagementUtils.LoadSceneByName("MainMenu");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error verificando usuario en la nube: {ex.Message}");
            warningText.text = "Error conectando con la nube";
        }
    }
}
