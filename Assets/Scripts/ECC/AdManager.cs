using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// #if UNITY_IOS
// using Unity.Advertisement.IosSupport;
// using UnityEngine.iOS;
// #endif

public class AdManager : MonoBehaviour
{
	private static AdManager _instance;
	public static AdManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<AdManager>();
				if (_instance == null)
				{
					GameObject admanagerObject = new GameObject("AdManager");
					_instance = admanagerObject.AddComponent<AdManager>();
				}

			}
			return _instance;
		}
	}

	public bool dontDestroy = true;
	private void Awake()
	{

		if (dontDestroy)
			DontDestroyOnLoad(gameObject);

		if (instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
		{
			Debug.Log("Destroy Admanager: Already Exists");
			Destroy(gameObject);
		}

		Advertisements.Instance.Initialize();


	}

	private void Start()
	{

		//StartCoroutine(InitDelay());

	}

	//	IEnumerator InitDelay()
	//	{
	//		yield return new WaitForSeconds(0.3f);

	//#if UNITY_IOS

	//		var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
	//		Debug.Log("IDFA STATUS " + status.ToString());

	//		if (Application.platform != RuntimePlatform.WindowsEditor)
	//		{
	//			Version currentVersion = new Version(Device.systemVersion);
	//			Version ios14 = new Version("14.5");
	//			Debug.Log("currentVersion: " + currentVersion.ToString());
	//			if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED && currentVersion >= ios14)
	//			{

	//				Debug.Log("NOT_DETERMINED, request window");
	//				ATTrackingStatusBinding.RequestAuthorizationTracking(TranckingRequestHandler);
	//			}
	//			else
	//			{
	//				TranckingRequestHandler((int)status);
	//			}

	//		}
	//		else {
	//			Advertisements.Instance.Initialize();
	//			Advertisements.Instance.SetUserConsent(true);
	//		}


	//#else
	//		Advertisements.Instance.Initialize();
	//		Advertisements.Instance.SetUserConsent(true);
	//		Debug.Log("Init Gley");
	//#endif


	//	}

	void TranckingRequestHandler(int status)
	{

		if (status == 3)
		{
			Advertisements.Instance.SetUserConsent(true);
			Debug.Log("IDFA ACCEPTED " + status.ToString());
		}
		else
		{
			Advertisements.Instance.SetUserConsent(false);
			Debug.Log("IDFA DENIED " + status.ToString());
		}

		Advertisements.Instance.Initialize();
		Debug.Log("AD INIT " + status.ToString());

	}


	public void ShowRewarded(Action<bool> callback = null)
	{
		if (Advertisements.Instance.IsRewardVideoAvailable())
		{
			
			Advertisements.Instance.ShowRewardedVideo((x, y) => CompleteMethod(x, y, callback));
		}
		else
		{
			Debug.Log("[AdManager]: No Rewarded Ad Available");
			callback?.Invoke(false);
		}
	}
	public void ShowInterstitial(Action callback = null)
	{
		if (Advertisements.Instance.IsInterstitialAvailable())
		{
			Advertisements.Instance.ShowInterstitial((x) => InterstitialClosed(x, callback));
		}
		else
		{
			Debug.Log("[AdManager]: No Interstitial Ad Available");
			callback?.Invoke();
		}
	}

	Coroutine bannerCo = null;
	public void ShowBanner()
	{
		bannerCo = StartCoroutine(ShowBannerIE());
	}

	IEnumerator ShowBannerIE()
	{
		while (!Advertisements.Instance.IsBannerOnScreen())
		{
			Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM, BannerType.SmartBanner);
			Debug.Log("Try show banner");
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void HideBanner()
	{
		StopCoroutine(bannerCo);
		Advertisements.Instance.HideBanner();
	}

	public void RemoveAds()
	{
		Advertisements.Instance.RemoveAds(true);
	}



	//callback called each time an interstitial is closed
	private void InterstitialClosed(string advertiser, Action callback = null)
	{
		if (Advertisements.Instance.debug)
		{
			Debug.Log("Interstitial closed from: " + advertiser + " -> Resume Game ");
			GleyMobileAds.ScreenWriter.Write("Interstitial closed from: " + advertiser + " -> Resume Game ");
		}
		callback?.Invoke();

	}

	//callback called each time a rewarded video is closed
	//if completed = true, rewarded video was seen until the end
	private void CompleteMethod(bool completed, string advertiser, Action<bool> callback = null)
	{
		if (Advertisements.Instance.debug)
		{
			Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
			GleyMobileAds.ScreenWriter.Write("Closed rewarded from: " + advertiser + " -> Completed " + completed);

			if (completed == true)
			{
				//give the reward
			}
			else
			{
				//no reward
			}
		}
		callback?.Invoke(completed);
	}
}
