using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public enum MoveMode
    {
        Rotate,
        Move,
        Both
    }
    [SerializeField]
    MoveMode moveMode = MoveMode.Both;

    [SerializeField]
    List<Transform> spotToMove;

    Board m_board;

    [SerializeField]
    List<Node> m_node;

    protected GameManager m_gameManager;

    [SerializeField]
    float moveTime = 2f;
    [SerializeField]
    iTween.EaseType easeType = iTween.EaseType.linear;
    [SerializeField]
    float delay = .1f;

    int nextSpotIndex = 1;

    private void Awake()
    {
        m_gameManager = Object.FindObjectOfType<GameManager>();
        m_board = Object.FindObjectOfType<Board>();
        if (spotToMove != null && spotToMove.Count > 0)
        {
            this.transform.position = spotToMove[0].position;
        }
    }

    public void Move()
    {
        StartCoroutine(PlatformMoveRoutine());
    }

    protected virtual IEnumerator PlatformMoveRoutine()
    {
        if(spotToMove == null)
        {
            StopAllCoroutines();
            Debug.LogWarning("NoSpotToMove in movingPlatform");
        }
        //노드 링크 지우고
        foreach(Node n in m_node)
        {
            n.DeleteAllLinkExcept(m_node);
        }
        yield return new WaitForSeconds(.2f);
        //이동
        foreach(Node n in m_node)
        {
            if (m_gameManager == null)
                break;
            Mover mover = m_gameManager.GetMoverAtPoint(n);
            if(mover != null)
            {
                BindMover(mover);
            }
        }
        if(nextSpotIndex >= spotToMove.Count)
        {
            nextSpotIndex = 0;
        }
        Transform nextXform = spotToMove[nextSpotIndex];
        if (moveMode == MoveMode.Both || moveMode == MoveMode.Move)
        {
            iTween.MoveTo(gameObject, iTween.Hash(
                "x", nextXform.position.x,
                "y", nextXform.position.y,
                "z", nextXform.position.z,
                "easetype", easeType,
                "time", moveTime,
                "delay", delay));
        }
        if (moveMode == MoveMode.Both || moveMode == MoveMode.Rotate)
        {
            iTween.RotateTo(gameObject, iTween.Hash(
                "y", nextXform.rotation.eulerAngles.y,
                "easetype", easeType,
                "time", moveTime,
                "delay", delay));
        }
        yield return new WaitForSeconds(moveTime+delay);
        nextSpotIndex++;
        //노드 상태 업데이트
        UnBindMover();
        foreach(Node n in m_node)
        {
            n.UpdateNodeStatus();
        }
    }

    private void BindMover(Mover m)
    {
        GameObject moveObject = m.gameObject;
        iTween.Stop(moveObject);
        moveObject.transform.position = m.CuurentNode.transform.position;
        moveObject.transform.parent = this.transform;
    }

    private void UnBindMover()
    {
        Mover[] movers = transform.GetComponentsInChildren<Mover>();
        foreach(Mover m in movers)
        {
            m.transform.SetParent(null);
        }
    }
}
