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
        //다음레벨이 존재하는지 확인
        //현재레벨을 클리어했는지 확인
        //다음레벨 로드
    }
}
