using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Node))]
public class MovingPlatform : MonoBehaviour {

    [SerializeField]
    List<Transform> spotToMove;

    Board m_board;

    Node m_node;

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
        m_node = GetComponent<Node>();
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
        m_node.DeleteAllLink();
        yield return new WaitForSeconds(.5f);
        //이동
        if(m_node == m_board.PlayerNode)
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
        m_node.UpdateNodeStatus();
    }

    private void BindPlayer()
    {
        GameObject player = Object.FindObjectOfType<PlayerManager>().gameObject;
        iTween.Stop(player);
        player.transform.position = transform.position;
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
