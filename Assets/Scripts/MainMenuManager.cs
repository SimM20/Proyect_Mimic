using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("GameObjects")]
    [SerializeField] private GameObject loadScreenPrefab;
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI userText;
    [Header("Sprites")]
    [SerializeField] private Sprite onSound;
    [SerializeField] private Sprite offSound;
    [SerializeField] private Image musicSprite;
    [SerializeField] private Image sfxSprite;
    [Header("Audio")]
    [SerializeField] private AudioMixer audiomixer;

    private bool isMusicOn = true;
    private bool isSFXOn = true;

    private void Start() { LoadScore(); }

    private async void LoadScore()
    {
        await GetHighScore();
        int score = await GetHighScore();
        scoreText.text += "\n" + score.ToString();
        userText.text = SaveSystem.CurrentUsername;
    }

    public async Task<int> GetHighScore()
    {
        int highScore = await SaveSystem.LoadHighScoreAsync();
        return highScore;
    }

    public void DeleteUser()
    {
        SaveSystem.DeleteLocalData();

        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.Save();

        SaveSystem.CurrentUsername = "";
        SceneManagementUtils.LoadSceneByName("Input");
    }

    public void ToggleMusic() 
    {
        isMusicOn = !isMusicOn;
        audiomixer.SetFloat("MusicVolume", isMusicOn ? 0f : -80f);
        musicSprite.sprite = isMusicOn ? onSound : offSound;
    }
    public void ToggleSFX() 
    {
        isSFXOn = !isSFXOn;
        audiomixer.SetFloat("SFXVolume", isSFXOn ? 0f : -80f);
        sfxSprite.sprite = isSFXOn ? onSound : offSound;
    }

    public void StartGame() { SceneManagementUtils.AsyncLoadSceneByName("Game", loadScreenPrefab, this); }
}
