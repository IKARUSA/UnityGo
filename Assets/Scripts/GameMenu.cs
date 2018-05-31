using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {
    [SerializeField]
    private ScreenFader fader;

    bool isTransitioning = false;

    public void OnBackPressed()
    {
        if(!isTransitioning)
            StartCoroutine(BackButtonRoutine());
    }

    IEnumerator BackButtonRoutine()
    {
        isTransitioning = true;
        AsyncOperation async = SceneManager.LoadSceneAsync("Title", LoadSceneMode.Single);
        async.allowSceneActivation = false;
        fader.FadeOn();
        yield return new WaitForSeconds(1f);
        async.allowSceneActivation = true;
        isTransitioning = false;
        MainMenu.Open();
    }
    
    public void OnRetryPressed()
    {
        if (!isTransitioning)
            StartCoroutine(RetryRoutine());
    }

    IEnumerator RetryRoutine()
    {
        isTransitioning = true;
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        async.allowSceneActivation = false;
        fader.FadeOn();
        yield return new WaitForSeconds(1f);
        async.allowSceneActivation = true;
        isTransitioning = false;
    }

    public void OnNextLevelPressed()
    {
        if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            if (!isTransitioning)
                StartCoroutine(LoadNextRoutine());
        }
        else
        {
            //do nothing;
        }
    }
    IEnumerator LoadNextRoutine()
    {
        isTransitioning = true;
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1, LoadSceneMode.Single);
        async.allowSceneActivation = false;
        fader.FadeOn();
        yield return new WaitForSeconds(1f);
        async.allowSceneActivation = true;
        isTransitioning = false;
    }
}
