using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectMenu : Menu<StageSelectMenu> {
    
    bool isLoading;

    [SerializeField]
    Button[] stageButtons;

    protected override void OnEnable()
    {
        if (stageButtons != null)
        {
            int maxLevelCleared = 0;
            if (PlayerPrefs.HasKey("MaxLevelCleared"))
            {
                maxLevelCleared = PlayerPrefs.GetInt("MaxLevelCleared");
            }
            else
            {
                PlayerPrefs.SetInt("MaxLevelCleared", 0);
            }
            for (int i = 0; i < stageButtons.Length; i++)
            {
                if (i < maxLevelCleared + 1)
                {
                    stageButtons[i].enabled = true;
                    stageButtons[i].animator.SetTrigger("Normal");
                }
                else
                {
                    stageButtons[i].enabled = false;
                    stageButtons[i].animator.SetTrigger("Disabled");
                }
            }
        }
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
