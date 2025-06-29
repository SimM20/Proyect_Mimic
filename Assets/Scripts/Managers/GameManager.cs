using System;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Action<int,int> OnUpdatePoints;
    [SerializeField] private GameObject ballSpawner;
    [SerializeField] private GameObject loadCanvasPrefab;
    private BallController actualBall;
    public int score { get; private set; } = 0;
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

    private void OnEnable() { StartRound(); }

    private void StartRound()
    {
        if (actualBall != null) Destroy(actualBall);
        actualBall = ballSpawner.GetComponent<BallSpawner>().SpawnBall();
        actualBall.OnOutOfBounds += OnDefeat;
        TopPaddle.OnHit += UpdatePoints;
    }

    public void OnDefeat()
    {
        actualBall.OnOutOfBounds -= OnDefeat;
        TopPaddle.OnHit -= UpdatePoints;
        AdsManager.Instance.ShowAd(
        onSuccess: () => { StartRound(); },
        onFailure: () =>
        {
            SaveScoreAsync();
            SceneManagementUtils.AsyncLoadSceneByName("Input", loadCanvasPrefab, this);
        } );
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

    public void ChangeGameplayTime(float timeUntilChange) { StartCoroutine(RenueveGameplayTime(timeUntilChange)); }
    private IEnumerator RenueveGameplayTime(float timeUntilChange)
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(timeUntilChange);
        Time.timeScale = 1f;
    }

    private void OnDisable() {  actualBall.OnOutOfBounds -= OnDefeat; }
}
