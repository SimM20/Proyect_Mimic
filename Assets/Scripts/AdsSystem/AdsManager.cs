using UnityEngine;
using Unity.Services.LevelPlay;
using Mediation = com.unity3d.mediation;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    private const string appKey = "2297f6c75";
    private const string bannerKey = "jxj4y5uja1bs02b5";
    private const string interstitialKey = "tjaqyxu7d4abcfwt";

    private BannerAd bannerAdBottom;
    private InterstitialAd interstitialAd;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() { LevelPlay.Init(appKey); }

    private void OnEnable()
    {
        LevelPlay.OnInitSuccess += OnInitSuccess;
        LevelPlay.OnInitFailed += OnInitFailed;
        SceneManagementUtils._OnSceneChanged += HandleBannerBehaviour;
    }

    private void OnDisable()
    {
        LevelPlay.OnInitSuccess -= OnInitSuccess;
        LevelPlay.OnInitFailed -= OnInitFailed;
        SceneManagementUtils._OnSceneChanged -= HandleBannerBehaviour;
    }

    //private void OnApplicationPause(bool pause) { IronSource.Agent.onApplicationPause(pause); }


    private void OnInitSuccess(LevelPlayConfiguration config)
    {
        bannerAdBottom = new BannerAd(bannerKey, Mediation.LevelPlayAdSize.CreateAdaptiveAdSize(), Mediation.LevelPlayBannerPosition.BottomCenter);
        interstitialAd = new InterstitialAd(interstitialKey);
        bannerAdBottom.Load();
        interstitialAd.Load();
    }

    private void OnInitFailed(LevelPlayInitError error) { Debug.LogWarning(error); }

    public void ShowInterstitialAd() { interstitialAd.Show(); }

    private void HandleBannerBehaviour(string sceneName)
    {
        if (bannerAdBottom != null)
        {
            if (sceneName != "Input") bannerAdBottom.Hide();
            else bannerAdBottom.Load();
        }
    }
}
