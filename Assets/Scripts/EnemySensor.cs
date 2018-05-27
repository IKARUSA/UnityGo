using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour {
    
    Board m_board;

    [SerializeField]
    List<Vector2> sensorSpots;

    [SerializeField]
    bool ignoreBlocking = false;

    bool playerDetected = false;
    public bool PlayerDetected { get { return playerDetected; } }

    private void Awake()
    {
        m_board = Object.FindObjectOfType<Board>();
    }

    public void Detect(Node currentNode)
    {
        foreach(Vector2 spot in sensorSpots)
        {
            Vector3 detectSpot = Utility.Vector3Round(Utility.V2toV3(spot) + currentNode.Coordinate);
            if(m_board.PlayerNode == m_board.GetNodeAt(detectSpot))
            {
                if (!ignoreBlocking)
                {
                    //raycasting 할 것
                }
                playerDetected = true;
            }
            else
            {
                playerDetected = false;
            }
        }
    }
}
