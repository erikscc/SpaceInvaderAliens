using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	private void Start()
	{
		//AdManager.instance.ShowRewarded();
	}
	public void ReloadLevel()
	{
		AdManager.instance.ShowInterstitial(Reload); 
	}

	private void Reload()
	{
		GameSettings.loadingScene = "GamePlay";
		SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
	}
	private void LoadMain()
	{
		GameSettings.loadingScene = "MainMenu";
		SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
	}
	public void MainMenu()
	{
		AdManager.instance.ShowInterstitial(LoadMain);
	}
}
