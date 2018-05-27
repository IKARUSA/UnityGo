using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectManager : TurnManager {

    protected Board m_board;

    [SerializeField]
    UnityEvent objectTurnEvent;

    protected override void Awake()
    {
        base.Awake();
        m_board = Object.FindObjectOfType<Board>();
    }

    public override void PlayTurn()
    {
        objectTurnEvent.Invoke();
        base.PlayTurn();
        m_isTurnComplete = true;
    }

}
