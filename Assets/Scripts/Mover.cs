using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mover : MonoBehaviour {

    private Node m_currentNode;
    public Node CuurentNode { get { return m_currentNode; } }

    private Node m_nextNode;

    private Board m_board;

    //itween variables
    [SerializeField]
    float moveTime;

    [SerializeField]
    float delay;

    [SerializeField]
    Animator moveAnim;

    [SerializeField]
    iTween.EaseType easeType = iTween.EaseType.linear;

    bool isMoving = false;
    public bool IsMoving { get { return isMoving; } }

    [SerializeField]
    UnityEvent endMoveEvent;

    private void Awake()
    {
        m_board = Object.FindObjectOfType<Board>();
    }

    private void Start()
    {
        m_currentNode = m_board.GetNodeAt(transform.position);
        if (m_currentNode == null)
        {
            Debug.LogWarning("Initializing Error ===== Init pos in " + gameObject.name + " is not valid");
        }
    }

    // move functions

    protected virtual void Move(Vector3 targetPos)
    {
        if (m_board != null)
        {
            Node targetNode;
            targetNode = m_board.GetNodeAt(targetPos);
            if (targetNode != null && m_currentNode.LinkedNodes.Contains(targetNode))
            {
                m_nextNode = targetNode;
                StartCoroutine(MoveRoutine());
            }
        }

    }

    protected virtual IEnumerator MoveRoutine()
    {
        isMoving = true;
        m_currentNode = m_nextNode;
        iTween.MoveTo(gameObject, iTween.Hash(
            "x", m_nextNode.Coordinate.x,
            "y", m_nextNode.Coordinate.y,
            "z", m_nextNode.Coordinate.z,
            "time", moveTime,
            "easetype", easeType,
            "delay", delay));
        if (moveAnim != null)
        {
            moveAnim.SetFloat("Speed", .2f);
            moveAnim.transform.rotation = Quaternion.LookRotation(m_nextNode.Coordinate - transform.position);
        }
        yield return new WaitForSeconds(moveTime + delay);

        if (moveAnim != null)
            moveAnim.SetFloat("Speed", 0f);
        isMoving = false;
        if (endMoveEvent != null)
            endMoveEvent.Invoke();
    }

    public void MoveLeft()
    {
        Move(m_currentNode.Coordinate + Board.spacing * Vector3.left);
    }

    public void MoveRight()
    {
        Move(m_currentNode.Coordinate + Board.spacing * Vector3.right);
    }

    public void MoveForward()
    {
        Move(m_currentNode.Coordinate + Board.spacing * Vector3.forward);
    }

    public void MoveBackward()
    {
        Move(m_currentNode.Coordinate + Board.spacing * Vector3.back);
    }


    //private void FaceDirection()
}
