using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectMenu : Menu<StageSelectMenu> {

    [SerializeField]
    ScreenFader graphic;

    bool isLoading;

    private void OnEnable()
    {
        graphic.FadeOff();
    }

    public override void OnBackPressed()
    {
        if(!isLoading)
            base.OnBackPressed();
    }

    public void LoadLevel(string levelName)
    {
        if (isLoading)
            return;
        isLoading = true;
        StartCoroutine(LoadLevelRoutine(levelName));
        isLoading = false;
    }

    private IEnumerator LoadLevelRoutine(string levelName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(levelName,LoadSceneMode.Single);
        async.allowSceneActivation = false;
        graphic.FadeOn();
        yield return new WaitForSeconds(1f);
        async.allowSceneActivation = true;
        yield return null;
        MenuManager.Instance.CloseAll();
    }
}
