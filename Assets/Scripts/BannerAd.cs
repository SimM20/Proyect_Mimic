using UnityEngine;
using Unity.Services.LevelPlay;
using Mediation = com.unity3d.mediation;

public class BannerAd
{
    private LevelPlayBannerAd ad;
    
    public BannerAd(string bannerKey, Mediation.LevelPlayAdSize size, Mediation.LevelPlayBannerPosition position)
    {
        ad = new LevelPlayBannerAd(bannerKey, size, position, null, false, true);
        ad.OnAdLoaded += BannerOnAdLoadedEvent;
        ad.OnAdLoadFailed += BannerOnAdLoadFailedEvent;
        ad.OnAdLeftApplication += BannerOnAdLeftApplicationEvent;
        Debug.Log("Banner created");
    }

    public void Load() => ad.LoadAd();

    public void Hide() => ad.HideAd();

    public void Destroy() => ad.DestroyAd();

    public void Show() => ad.ShowAd();
    
    void BannerOnAdLoadedEvent(LevelPlayAdInfo adInfo) { ad.ShowAd(); }
    void BannerOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) { /* Que hacer cuando falla */ Debug.LogError(ironSourceError); }
    void BannerOnAdLeftApplicationEvent(LevelPlayAdInfo adInfo) { ad.DestroyAd(); }
}
