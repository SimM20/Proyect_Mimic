using System.Collections;
using System.Collections.Generic;
using Unity.Services.LevelPlay;
using UnityEngine;

public class InterstitialAd
{
    private LevelPlayInterstitialAd ad;
    public InterstitialAd(string interstitialKey)
    {
        ad = new LevelPlayInterstitialAd(interstitialKey);

        ad.OnAdLoaded += InterstitialOnAdLoadedEvent;
        ad.OnAdLoadFailed += InterstitialOnAdLoadFailedEvent;
        ad.OnAdClosed += InterstitialOnAdClosedEvent;
    }

    public void Load() => ad.LoadAd();
    public void Show() => ad.ShowAd();
    public void Destroy() => ad.DestroyAd();

    void InterstitialOnAdLoadedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdLoadFailedEvent(LevelPlayAdError error) { OnAdFinished(); }
    void InterstitialOnAdClosedEvent(LevelPlayAdInfo adInfo) { OnAdFinished(); }

    private void OnAdFinished()
    {
        ad.LoadAd();
        var gm = GameManager.Instance;
        if (gm != null) gm.GameOver();
    }
}
