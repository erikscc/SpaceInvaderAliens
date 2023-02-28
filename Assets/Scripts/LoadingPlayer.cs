using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPlayer : MonoBehaviour
{
    //public GameObject logo;
    public Slider progressBar;
    public TMP_Text percentage;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadYourAsyncScene());
    }
    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameSettings.loadingScene);
        while (!asyncLoad.isDone)
        {
            progressBar.value = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            percentage.text = Mathf.Round(progressBar.value * 100) + "%";
            yield return null;
        }
    }
}
