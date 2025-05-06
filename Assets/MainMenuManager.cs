using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class MainMenuManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject loadScreenPrefab;

    private void Awake() { LoadScore(); }

    private async void LoadScore()
    {
        int score = await GetHighScore();
        scoreText.text += "\n" + score.ToString();
    }

    public async Task<int> GetHighScore()
    {
        int highScore = await SaveSystem.LoadHighScoreAsync();
        return highScore;
    }

    public void StartGame()
    {
        SceneManagementUtils.AsyncLoadSceneByName("Game", loadScreenPrefab, this);
    }
}
