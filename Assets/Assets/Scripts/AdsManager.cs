using GoogleMobileAds.Api;
using UnityEngine;
using System.Collections;

public class AdsManager : MonoBehaviour
{
    //Per activar els anuncis descomenta les següents línies, a més d'anar a game manager i descomentar totes les linies que posin: AdsManager(.LoadAd(), .LoadInetsticialAd(), .ShowIntersticialAd(), etc.)
    /*public GameManager gameManager;
    public InterstitialAd _interstitialAd;
    private float FinishTime = 0.0f;
    private float Period = 30.0f;
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) => { });
    }
    private void Awake()
    {
        LoadAd();
    }

#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
    //private string _adUnitId = "ca-app-pub-4359271377803074/3739249355";

#else
  private string _adUnitId = "unused";
#endif

    public void LoadInterstitialAd()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        var adRequest = new AdRequest();

        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    return;
                }

                _interstitialAd = ad;
            });


    }
    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();
            RegisterReloadHandler(_interstitialAd);
        }
        else
        {
            LoadInterstitialAd();
        }
    }
    public void RegisterReloadHandler(InterstitialAd interstitialAd)
    {
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            if (gameManager.CanPlayAudio)
            {
                if (gameManager.score >= 20000)
                {
                    gameManager.GameMusic2.Play();
                }
                else
                {
                    gameManager.GameMusic1.Play();
                }
            }

            LoadInterstitialAd();
        };
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadInterstitialAd();
        };
    }


    private string _adUnityBanner = "ca-app-pub-3940256099942544/6300978111";
    //private string _adUnityBanner = "ca-app-pub-4359271377803074/3065166208";
    BannerView _bannerView;
    public void CreateBannerView()
    {
        if (_bannerView != null)
        {
            DestroyAd();
        }
        _bannerView = new BannerView(_adUnityBanner, AdSize.IABBanner, AdPosition.Top);
        LoadAd();
    }
    public void LoadAd()
    {
        if (_bannerView == null)
        {
            CreateBannerView();
        }
        var adRequest = new AdRequest();

        _bannerView.LoadAd(adRequest);
    }
    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            _bannerView.Destroy();
            _bannerView = null;
        }
    }
    private void Update()
    {
        if (_bannerView != null)
        {
            _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                LoadAd();
            };
            if (Time.time > FinishTime)
            {
                FinishTime += Period;
                LoadEvery30Sec();
            }
        }

        if (_bannerView == null)
        {
            CreateBannerView();
        }

    }
    private void LoadEvery30Sec()
    {
        DestroyAd();
        LoadAd();
    }*/
}
