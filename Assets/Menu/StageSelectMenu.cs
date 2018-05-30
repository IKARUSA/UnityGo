using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectMenu : Menu<StageSelectMenu> {
    
    bool isLoading;

    protected override void OnEnable()
    {
        //sync with stage clear data
        base.OnEnable();
    }

    public override void OnBackPressed()
    {
        if (!isLoading)
        {
            base.OnBackPressed();
        }
    }

    public void LoadLevel(string levelName)
    {
        if (isLoading)
            return;
        isLoading = true;
        StartCoroutine(LoadLevelRoutine(levelName));
    }

    private IEnumerator LoadLevelRoutine(string levelName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(levelName,LoadSceneMode.Single);
        async.allowSceneActivation = false;
        screenFader.FadeOn();
        yield return new WaitForSeconds(1f);
        isLoading = false;
        async.allowSceneActivation = true;
        yield return null;
        MenuManager.Instance.CloseAll();
    }
}
