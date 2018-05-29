using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {
    
    public void OnBackPressed()
    {
        SceneManager.LoadScene("Title");
        MainMenu.Open();
    }
    
    public void OnRetryPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnNextLevelPressed()
    {
        //다음레벨이 존재하는지 확인
        //현재레벨을 클리어했는지 확인
        //다음레벨 로드
    }
}
