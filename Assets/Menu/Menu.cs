using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class Menu<T> : Menu where T : Menu<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = (T)this;
        }
            
    }

    public static void Open()
    {
        if(MenuManager.Instance != null && instance != null)
        {
            MenuManager.Instance.OpenMenu(instance);
        }
    }
    
    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
}

public class Menu : MonoBehaviour
{
    [SerializeField]
    protected ScreenFader screenFader;

    protected bool isTransitioning = false;

    protected virtual void OnEnable()
    {
        if (screenFader != null)
            screenFader.FadeOff();
    }

    public virtual void OnBackPressed()
    {
        if (screenFader != null && !isTransitioning)
            StartCoroutine(BackRoutine());
    }

    private IEnumerator BackRoutine()
    {
        yield return StartCoroutine(FadeOn());
        MenuManager.Instance.CloseMenu();
    }
    
    protected IEnumerator FadeOn()
    {
        isTransitioning = true;
        screenFader.FadeOn();
        yield return new WaitForSeconds(.5f);
        isTransitioning = false;
    }
}
