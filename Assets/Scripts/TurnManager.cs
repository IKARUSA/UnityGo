using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    protected GameManager m_gameManager;

    protected bool m_isTurnComplete = false;
    public bool IsTurnComplete { get { return m_isTurnComplete; } set { m_isTurnComplete = value; } }

    protected virtual void Awake()
    {
        m_gameManager = Object.FindObjectOfType<GameManager>(); //todo - consider make this singleton
    }


    public void FinishTurn()
    {
        m_isTurnComplete = true;
        m_gameManager.UpdateTurn();
    }

    public virtual void PlayTurn()
    {

    }
}
