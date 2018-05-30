using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : Mover {
    public enum EnemyMoveType
    {
        Stationary,
        Patrol,
        Centry
    }

    [SerializeField]
    EnemyMoveType moveType;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public void Move()
    {
        StartCoroutine(MoveRoutine());
    }

    protected override IEnumerator MoveRoutine()
    {
        switch (moveType)
        {
            case EnemyMoveType.Stationary:
                yield return null;
                break;
            case EnemyMoveType.Patrol:
                Node tempNextNode = m_board.GetNodeAt(transform.position + transform.forward * Board.spacing);
                if (tempNextNode == null || !tempNextNode.NeighborNodes.Contains(m_currentNode))
                {
                    FaceDirection(transform.position - transform.forward);
                    yield return new WaitForSeconds(rotateTime);
                    m_nextNode = m_board.GetNodeAt(transform.position + transform.forward * Board.spacing);
                    if (m_nextNode != null)
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
