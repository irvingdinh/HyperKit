using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigProvider : MonoBehaviour
{
    public static ConfigProvider Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AdsConfig GetAdsConfig()
    {
        AdsConfig adsConfig = null;

#if UNITY_IOS
        adsConfig = new AdsConfig
        {
            GameId = "",
            BannerUnitId = "",
            InterstitialUnitId = "",
            TestMode = false
        };
#elif UNITY_ANDROID
        adsConfig = new AdsConfig
        {
            GameId = "",
            BannerUnitId = "",
            InterstitialUnitId = "",
            TestMode = false
        };
#else
        adsConfig = new AdsConfig
        {
            GameId = "5353535",
            BannerUnitId = "Banner_Editor",
            InterstitialUnitId = "Interstitial_Editor",
            TestMode = true
        };
#endif

        return adsConfig;
    }
}

public class AdsConfig
{
    public string GameId { get; set; }

    public string BannerUnitId { get; set; }

    public string InterstitialUnitId { get; set; }

    public bool TestMode { get; set; }
}