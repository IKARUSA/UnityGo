using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

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
        if(m_node.Contains(m_board.PlayerNode))
        {
            BindPlayer();
        }
        if(nextSpotIndex >= spotToMove.Count)
        {
            nextSpotIndex = 0;
        }
        Transform nextXform = spotToMove[nextSpotIndex];
        iTween.MoveTo(gameObject, iTween.Hash(
            "x", nextXform.position.x,
            "y", nextXform.position.y,
            "z", nextXform.position.z,
            "easetype", easeType,
            "time", moveTime,
            "delay", delay));
        yield return new WaitForSeconds(moveTime+delay);
        nextSpotIndex++;
        //노드 상태 업데이트
        UnBindPlayer();
        foreach(Node n in m_node)
        {
            n.UpdateNodeStatus();
        }
    }

    private void BindPlayer()
    {
        GameObject player = Object.FindObjectOfType<PlayerManager>().gameObject;
        iTween.Stop(player);
        player.transform.position = m_board.PlayerNode.transform.position;
        player.transform.parent = this.transform;
    }

    private void UnBindPlayer()
    {
        PlayerManager player = transform.GetComponentInChildren<PlayerManager>();
        if(player != null)
        {
            player.transform.SetParent(null);
        }
    }
}
