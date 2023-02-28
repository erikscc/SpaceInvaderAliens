using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public TMP_Text highScore;
    public GameObject buyChar;
    public GameObject play;
	private void OnEnable()
	{
		EventManager.OnCharacterSelect += ButtonCheck;
	}
	private void OnDisable()
	{
		EventManager.OnCharacterSelect -= ButtonCheck;
	}


	// Start is called before the first frame update
	void Start()
    {
        highScore.text = string.Format("{0:0,0}",PlayerPrefs.GetInt("HighScore",0));
    }
	private void ButtonCheck(int skins)
	{
		Debug.Log("Skin chech now");
		if (PlayerPrefs.GetInt("char" + skins.ToString(), 0) != 0)
		{
			buyChar.SetActive(false);
			play.SetActive(true);
			//charLocked.SetActive(false);
		}
		else
		{
			play.SetActive(false);
			buyChar.SetActive(true);
			//charLocked.SetActive(true);
}
	}

}
