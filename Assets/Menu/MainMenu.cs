using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu<MainMenu> {

    public void OnPlayButtonPressed()
    {
        StageSelectMenu.Open();
    }

    public void OnCreditButtonPressed()
    {
        CreditMenu.Open();
    }

    public override void OnBackPressed()
    {
        Application.Quit();
    }
}
