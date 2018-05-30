using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu<MainMenu> {

    public void OnPlayButtonPressed()
    {
        if(!isTransitioning)
            StartCoroutine(PlayButtonRoutine());
    }

    private IEnumerator PlayButtonRoutine()
    {
        yield return StartCoroutine(FadeOn());
        StageSelectMenu.Open();
    }

    public void OnCreditButtonPressed()
    {
        if (!isTransitioning)
            StartCoroutine(CreditButtonRoutine());
    }

    private IEnumerator CreditButtonRoutine()
    {
        yield return StartCoroutine(FadeOn());
        CreditMenu.Open();
    }

    public override void OnBackPressed()
    {
        if (!isTransitioning)
            Application.Quit();
    }
}
