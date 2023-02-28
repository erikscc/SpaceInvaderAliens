using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
	public List<GameObject> skins;
	private int skinIndex;
	private int prevSkin;
	private void OnEnable()
	{
		skinIndex = PlayerPrefs.GetInt("skinIndex", 0);
		skins[skinIndex].SetActive(true);	
	}
	private void Start()
	{
		PlayerPrefs.SetInt("char0",1);
		EventManager.OnCharacterSelect?.Invoke(skinIndex);
	}
	public void Next()
	{
		prevSkin = skinIndex;
		skinIndex++;
		if (skinIndex > skins.Count - 1) skinIndex = 0;
		skins[prevSkin].SetActive(false);
		skins[skinIndex].SetActive(true);
		EventManager.OnCharacterSelect?.Invoke(skinIndex);
	}
	public void Prev()
	{
		prevSkin = skinIndex;
		skinIndex--;
		if (skinIndex < 0) skinIndex = skins.Count - 1;
		skins[prevSkin].SetActive(false);
		skins[skinIndex].SetActive(true);
		EventManager.OnCharacterSelect?.Invoke(skinIndex);
	}
	public void StartGame()
	{
		//AdManager.instance.ShowInterstitial(LoadGameScene);
		GameSettings.skinIndex = skinIndex;
		GameSettings.loadingScene = "GamePlay";
		SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
	}
	private void LoadGameScene()
	{
		GameSettings.skinIndex = skinIndex;
		GameSettings.loadingScene = "GamePlay";
		SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
	}
	private void OnDestroy()
	{
		PlayerPrefs.SetInt("skinIndex", skinIndex);
	}
	public void BuyCharacterForAd()
	{
		AdManager.instance.ShowRewarded(result=> 
		{
			AdManager.instance.ShowInterstitial(CharacterBuy); 
		});
	}
	private void CharacterBuy()
	{
		PlayerPrefs.SetInt("char"+skinIndex.ToString(),1);
		EventManager.OnCharacterSelect?.Invoke(skinIndex);
		EventManager.OnCharacterBuySound?.Invoke();
	}
}
