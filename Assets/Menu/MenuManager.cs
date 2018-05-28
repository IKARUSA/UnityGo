using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    MainMenu mainMenuPrefab;

    [SerializeField]
    Transform m_menuParent;

    Stack<Menu> m_menuStack = new Stack<Menu>();

    private static MenuManager instance;
    public static MenuManager Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitMenu();
    }

    void InitMenu()
    {
        if (m_menuParent == null)
        {
            GameObject menuParentObject = new GameObject("Menus");
            m_menuParent = menuParentObject.transform;
        }

        BindingFlags flags = BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance;
        Type myType = this.GetType();
        FieldInfo[] fields = myType.GetFields(flags);
        foreach(FieldInfo field in fields)
        {
            Menu menuPrefab = field.GetValue(this) as Menu;
            if(menuPrefab != null)
            {
                Menu menuInstance = Instantiate(menuPrefab, m_menuParent);
                if(menuPrefab != mainMenuPrefab)
                {
                    menuInstance.gameObject.SetActive(false);
                }
                else
                {
                    OpenMenu(menuInstance);
                }
            }
        }
    }

    public void OpenMenu(Menu menuInstance)
    {
        if(menuInstance != null)
        {
            if(m_menuStack.Count >= 1)
            {
                m_menuStack.Peek().gameObject.SetActive(false);
            }
            menuInstance.gameObject.SetActive(true);
            m_menuStack.Push(menuInstance);
        }
    }


    public void CloseMenu()
    {
        if(m_menuStack.Count > 1)
        {
            m_menuStack.Pop().gameObject.SetActive(false);
            m_menuStack.Peek().gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Attempt to close menu | only one in stack!");
        }
    }
}
