using UnityEngine;
using TMPro;

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

    public void SendUser()
    {
        string username = inputField.text;
        if (!string.IsNullOrEmpty(username))
        {
            SaveSystem.CurrentUsername = username;
            PlayerPrefs.SetString("username", username);
            PlayerPrefs.Save();
            warningText.text = "Loged with: " + username;
            SceneManagementUtils.LoadSceneByName("MainMenu");
        }
        else warningText.text = "Username can't be empty";
    }
}
