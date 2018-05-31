using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySensor))]
public class EnemyMover : Mover {
    public enum EnemyMoveType
    {
        Stationary,
        Patrol,
        Centry
    }

    [SerializeField]
    EnemyMoveType moveType;

    EnemySensor m_enemySensor;
    
    protected override void Awake()
    {
        base.Awake();
        m_enemySensor = GetComponent<EnemySensor>();
        m_gameManager = Object.FindObjectOfType<GameManager>();
    }
    
    public void Move()
    {
        StartCoroutine(MoveRoutine());
    }

    protected override IEnumerator MoveRoutine()
    {
        m_enemySensor.Detect(m_currentNode);
        if (m_enemySensor.PlayerDetected)
        {
            m_gameManager.GameOver();
        }
        else
        {
            switch (moveType)
            {
                case EnemyMoveType.Stationary:
                    yield return null;
                    break;
                case EnemyMoveType.Patrol:
                    Node tempNextNode = m_board.GetNodeAt(transform.position + transform.forward * Board.spacing);
                    if (tempNextNode == null || !tempNextNode.NeighborNodes.Contains(m_currentNode) || m_gameManager.GetMoverAtPoint(tempNextNode) != null)
                    {
                        FaceDirection(transform.position - transform.forward);
                        yield return new WaitForSeconds(rotateTime);
                        m_enemySensor.Detect(m_currentNode);
                        if (m_enemySensor.PlayerDetected)
                        {
                            m_gameManager.GameOver();
                        }
                        m_nextNode = m_board.GetNodeAt(transform.position + transform.forward * Board.spacing);
                        if (m_nextNode != null && m_nextNode.NeighborNodes.Contains(m_currentNode))
                            StartCoroutine(base.MoveRoutine());
                    }
                    else
                    {
                        m_nextNode = tempNextNode;
                        yield return StartCoroutine(base.MoveRoutine());
                    }
                    break;
                case EnemyMoveType.Centry:
                    FaceDirection(transform.position - transform.forward);
                    yield return new WaitForSeconds(rotateTime);
                    break;
            }
        }
    }
	
    
}
