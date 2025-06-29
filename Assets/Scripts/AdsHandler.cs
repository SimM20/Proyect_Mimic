using System;
using UnityEngine.Advertisements;

public class AdsHandler : IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private const string androidGameId = "5888215";
    private const string androidAdsId = "Rewarded_Android";
    private const string androidBannerId = "Banner_Android";

    private bool isInitialized = false;
    private Action onAdSuccessCallback;
    private Action onAdFailureCallback;

    public void Initialize(bool testMode)
    {
        if (!Advertisement.isInitialized)
            Advertisement.Initialize(androidGameId, testMode, this);
    }

    public void ShowAdvertisement(Action onSuccess, Action onFailure)
    {
        onAdSuccessCallback = onSuccess;
        onAdFailureCallback = onFailure;
        Advertisement.Load(androidAdsId, this);
    }

    public void ShowBanner()
    {
        if (!Advertisement.isInitialized) return;

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(androidBannerId);
    }

    public void HideBanner() { Advertisement.Banner.Hide(); }

    public void OnInitializationComplete() { isInitialized = true; }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        //Fallo la inicializacion de las ads
    }

    public void OnUnityAdsAdLoaded(string placementId) { Advertisement.Show(placementId, this); }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        //Fallo la carga del ad
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == androidAdsId)
        {
            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
                onAdSuccessCallback?.Invoke();
            else
                onAdFailureCallback?.Invoke();
        }

        onAdSuccessCallback = null;
        onAdFailureCallback = null;
    }

    public void OnUnityAdsShowStart(string placementId) { }
    public void OnUnityAdsShowClick(string placementId) { }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        onAdFailureCallback?.Invoke();
        onAdSuccessCallback = null;
        onAdFailureCallback = null;
    }
}