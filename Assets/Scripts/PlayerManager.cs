using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerManager : TurnManager {

    PlayerInput input;
    PlayerMover m_playerMover;

    protected override void Awake()
    {
        base.Awake();
        input = GetComponent<PlayerInput>();
        m_playerMover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        if (m_isTurnComplete || m_playerMover.IsMoving)
            return;
        input.GetInput();
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (input.H > Mathf.Epsilon)
        {
            m_playerMover.MoveRight();
        }
        else if (input.H < -Mathf.Epsilon)
        {
            m_playerMover.MoveLeft();
        }
        else if (input.V > Mathf.Epsilon)
        {
            m_playerMover.MoveForward();
        }
        else if (input.V < -Mathf.Epsilon)
        {
            m_playerMover.MoveBackward();
        }
    }
}
