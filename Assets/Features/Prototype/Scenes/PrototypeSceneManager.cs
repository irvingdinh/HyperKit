using UnityEngine;
using UnityEngine.Advertisements;

public class PrototypeSceneManager :
    MonoBehaviour,
    IUnityAdsInitializationListener,
    IUnityAdsLoadListener,
    IUnityAdsShowListener
{
    private void Start()
    {
        InitializeAds();
    }

    #region UnityAds

    void InitializeAds()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            var adsConfig = ConfigProvider.Instance.GetAdsConfig();

            Advertisement.Initialize(adsConfig.GameId, adsConfig.TestMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("OnInitializationComplete");
        
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        
        ShowBannerAds();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"OnInitializationFailed; {error.ToString()}; {message}");
    }

    #endregion

    #region UnityAdsBanner

    public void ShowBannerAds()
    {
        var adsConfig = ConfigProvider.Instance.GetAdsConfig();

        var options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(adsConfig.BannerUnitId, options);
    }

    void OnBannerLoaded()
    {
        Debug.Log("OnBannerLoaded");

        var adsConfig = ConfigProvider.Instance.GetAdsConfig();

        var options = new BannerOptions
        {
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(adsConfig.BannerUnitId, options);
    }

    void OnBannerError(string message)
    {
        Debug.Log($"OnBannerError {message}");
    }

    void OnBannerShown()
    {
        Debug.Log("OnBannerShown");
    }

    #endregion

    #region UnityAdsInterstitial

    public void ShowInterstitialAds()
    {
        var adsConfig = ConfigProvider.Instance.GetAdsConfig();

        Advertisement.Load(adsConfig.InterstitialUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("OnUnityAdsAdLoaded");

        var adsConfig = ConfigProvider.Instance.GetAdsConfig();

        Advertisement.Show(adsConfig.InterstitialUnitId, this);
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"OnUnityAdsFailedToLoad; {adUnitId}; {error.ToString()}; {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"OnUnityAdsShowFailure; {adUnitId}; {error.ToString()}; {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        Debug.Log($"OnUnityAdsShowStart; {adUnitId}");
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        Debug.Log($"OnUnityAdsShowClick; {adUnitId}");
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"OnUnityAdsShowComplete; {adUnitId}; {showCompletionState.ToString()}");
    }

    #endregion
}