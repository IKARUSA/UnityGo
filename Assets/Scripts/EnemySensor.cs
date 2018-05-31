using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour {

    [SerializeField]
    GameObject detectIcon;

    Board m_board;

    [SerializeField]
    List<Vector3> sensorSpots;

    [SerializeField]
    LayerMask enemySensorBlockMask;

    bool playerDetected = false;
    public bool PlayerDetected { get { return playerDetected; } }

    private void Awake()
    {
        m_board = Object.FindObjectOfType<Board>();
    }

    public void Detect(Node currentNode)
    {
        foreach(Vector3 spot in sensorSpots)
        {
            Vector3 detectSpot = Utility.Vector3Round(transform.TransformVector(spot) + currentNode.Coordinate);
            Node targetNode = m_board.GetNodeAt(detectSpot);
            if(targetNode!=null &&
                m_board.PlayerNode == m_board.GetNodeAt(detectSpot) &&
                !Physics.Raycast(transform.position,targetNode.Coordinate - transform.position,Vector3.Magnitude(targetNode.Coordinate-transform.position),enemySensorBlockMask))
            {
                playerDetected = true;
            }
            else
            {
                playerDetected = false;
            }
        }
        if (playerDetected)
        {
            iTween.ScaleTo(detectIcon, iTween.Hash(
                "scale", new Vector3(.4f,1f,1f),
                "time", .3f,
                "delay", 0f,
                "easetype", iTween.EaseType.easeOutQuad));
        }
        else
        {
            iTween.ScaleTo(detectIcon, iTween.Hash(
                "scale", Vector3.zero,
                "time", .3f,
                "delay", 0f,
                "easetype", iTween.EaseType.easeOutQuad));
        }
    }
}
