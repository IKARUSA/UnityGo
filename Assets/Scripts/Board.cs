using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public static float spacing = 2f;

    public static Vector2[] directions =
    {
        Vector2.up * spacing,
        Vector2.down * spacing,
        Vector2.right * spacing,
        Vector2.left * spacing,
    };

    List<Node> m_allNodes;
    public List<Node> AllNodes { get { return m_allNodes; } }

    PlayerMover m_playerMover;
    public Node PlayerNode { get { return m_playerMover.CuurentNode; } }

    private void Awake()
    {
        Node[] allnodes = Object.FindObjectsOfType<Node>();
        m_allNodes = new List<Node>(allnodes);
        m_playerMover = Object.FindObjectOfType<PlayerMover>();
    }

    public Node GetNodeAt(Vector3 pos)
    {
        return m_allNodes.Find(n => n.Coordinate == Utility.Vector3Round(pos));
    }
}
