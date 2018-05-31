using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerManager : TurnManager {

    PlayerInput input;
    public PlayerInput Input { get { return input; } }
    
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
        Input.GetInput();
        ProcessInput();
    }

    public void HandleMouseClick(Node node)
    {
        if (m_isTurnComplete || m_playerMover.IsMoving)
            return;
        if (m_playerMover.CuurentNode.NeighborNodes.Contains(node))
        {
            if (m_gameManager.GetMoverAtPoint(node) == null)
            {
                Vector3 relativeVec = node.Coordinate - m_playerMover.CuurentNode.Coordinate;
                if (relativeVec.x > 1f)
                {
                    m_playerMover.MoveRight();
                }else if(relativeVec.x < -1f)
                {
                    m_playerMover.MoveLeft();
                }
                if(relativeVec.z > 1f)
                {
                    m_playerMover.MoveForward();
                }else if(relativeVec.z < -1f)
                {
                    m_playerMover.MoveBackward();
                }
            }
        }
    }

    private void ProcessInput()
    {
        if (Input.H > Mathf.Epsilon)
        {
            m_playerMover.MoveRight();
        }
        else if (Input.H < -Mathf.Epsilon)
        {
            m_playerMover.MoveLeft();
        }
        else if (Input.V > Mathf.Epsilon)
        {
            m_playerMover.MoveForward();
        }
        else if (Input.V < -Mathf.Epsilon)
        {
            m_playerMover.MoveBackward();
        }
    }
}
