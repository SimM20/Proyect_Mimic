using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    [SerializeField] private bool testMode = false;

    private AdsHandler adsHandler;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        adsHandler = new AdsHandler();
        adsHandler.Initialize(testMode);
    }

    public void ShowAd(Action onSuccess, Action onFailure = null) => adsHandler.ShowAdvertisement(onSuccess, onFailure);
    public void ShowBanner() => adsHandler.ShowBanner();
    public void HideBanner() => adsHandler.HideBanner();
}
