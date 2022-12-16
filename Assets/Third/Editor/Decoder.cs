using UnityEditor;
using UnityEngine;

public class Decoder : EditorWindow
{
	string emulatedGeo;
	string containerJsonString;
	Container container;
	EncryptData encryptData;

	[MenuItem("Tools/Decoder")]
	static void Init()
	{
		GetWindow<Decoder>("Decoder");
	}

	bool ContainerJsonStringLenghtZero
    {
		get => string.IsNullOrWhiteSpace(containerJsonString) || string.IsNullOrEmpty(containerJsonString);
	}

	private void OnGUI()
	{
		GUI.color = Color.cyan;
		EditorGUILayout.BeginVertical(EditorStyles.helpBox);
		emulatedGeo = EditorGUILayout.TextField("Emulated Geo", emulatedGeo);
		containerJsonString = EditorGUILayout.TextField("String to decode", containerJsonString);
		EditorGUILayout.EndVertical();

		GUI.color = Color.green;

		if (ContainerJsonStringLenghtZero)
		{
			return;
		}

		if (GUILayout.Button("Decode"))
		{
			container = Decriptor.GetData(containerJsonString, out encryptData);
		}

		if (container == null)
		{
			return;
		}

		GUI.color = Color.cyan;

		EditorGUILayout.BeginVertical(EditorStyles.helpBox);



		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("param_sim_geo");
		EditorGUILayout.SelectableLabel(encryptData.huw_sim_geo, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
		EditorGUILayout.EndHorizontal();


		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("param_bundle");
		EditorGUILayout.SelectableLabel(encryptData.huw_bundle, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
		EditorGUILayout.EndHorizontal();




		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("param_amidentificator");
		EditorGUILayout.SelectableLabel(encryptData.huw_amidentificator, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
		EditorGUILayout.EndHorizontal();


		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("param_afidentificator");
		EditorGUILayout.SelectableLabel(encryptData.huw_afidentificator, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
		EditorGUILayout.EndHorizontal();



		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("param_googleid");
		EditorGUILayout.SelectableLabel(encryptData.huw_googleID, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
		EditorGUILayout.EndHorizontal();



		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("param_subcodename");
		EditorGUILayout.SelectableLabel(encryptData.huw_subcodename, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
		EditorGUILayout.EndHorizontal();



		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("bundle");
		EditorGUILayout.SelectableLabel(encryptData.bundle_prop, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
		EditorGUILayout.EndHorizontal();



		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("subcodename");
		EditorGUILayout.SelectableLabel(encryptData.subcodename_prop, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
		EditorGUILayout.EndHorizontal();



		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("domen");
		EditorGUILayout.SelectableLabel(encryptData.domen_prop, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
		EditorGUILayout.EndHorizontal();



		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("space");
		EditorGUILayout.SelectableLabel(encryptData.space_prop, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
		EditorGUILayout.EndHorizontal();



		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("requestCampaign");
		EditorGUILayout.SelectableLabel(encryptData.requestCampaign_prop, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
		EditorGUILayout.EndHorizontal();



		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("appmetricaAppId");
		EditorGUILayout.SelectableLabel(container.initData.appmetricaAppId_prop, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
		EditorGUILayout.EndHorizontal();


		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("oneSignalAppId");
		EditorGUILayout.SelectableLabel(container.initData.oneSignalAppId_prop, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
		EditorGUILayout.EndHorizontal();


		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("appsFlyerAppId");
		EditorGUILayout.SelectableLabel(container.initData.appsFlyerAppId_prop, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
		EditorGUILayout.EndHorizontal();



		EditorGUILayout.EndVertical();

		GUI.color = Color.green;

		if (GUILayout.Button("Send Request"))
		{
			string _base = string.Concat(encryptData.huw_protocol, encryptData.domen_prop, ".", encryptData.space_prop, "/", encryptData.requestCampaign_prop, "?", encryptData.huw_sim_geo, "=", emulatedGeo);

			Application.OpenURL(_base);
		}
	}
}
