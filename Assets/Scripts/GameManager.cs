using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Action<int,int> OnUpdatePoints;
    [SerializeField] private GameObject ballSpawner;
    [SerializeField] private GameObject loadCanvasPrefab;
    private BallController actualBall;
    private int score = 0;
    private int highScore = 0;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        LoadScore();
    }
    private async void LoadScore() 
    { 
        highScore = await GetHighScore();
        OnUpdatePoints?.Invoke(score, highScore);
    }

    private void OnEnable()
    {
        actualBall = ballSpawner.GetComponent<BallSpawner>().SpawnBall();
        actualBall.OnOutOfBounds += OnDefeat;
    }

    public void OnDefeat() 
    { 
        /*Aca iria todo lo que tiene que ver con la derrota en si*/ 
        actualBall.OnOutOfBounds -= OnDefeat;
        SaveScoreAsync();
        SceneManagementUtils.AsyncLoadSceneByName("MainMenu", loadCanvasPrefab, this);
    }

    private async void SaveScoreAsync() { await SaveSystem.SaveScoreAsync(score); }
    public async Task<int> GetHighScore()
    {
        int highScore = await SaveSystem.LoadHighScoreAsync();
        return highScore;
    }

    public void UpdatePoints()
    {
        score += 1;
        OnUpdatePoints?.Invoke(score, highScore);
    }

    private void OnDisable() {  actualBall.OnOutOfBounds -= OnDefeat; }
}
