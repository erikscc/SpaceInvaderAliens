using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartScenes());
    }

    public IEnumerator StartScenes()
	{
        yield return new WaitForSeconds(2f);
        GameSettings.loadingScene = "MainMenu";
        SceneManager.LoadScene("LoadingScene",LoadSceneMode.Single);
	}
}
