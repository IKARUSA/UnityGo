using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    Node m_goalNode;
    public Node GoalNode { get { return m_goalNode; } set { m_goalNode = value; } }

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

    [SerializeField]
    bool drawOnStart = false;

    private void Awake()
    {
        InitBoard();
    }

    private void Start()
    {
        if (drawOnStart)
            DrawBoard();
    }

    private void InitBoard()
    {
        Node[] allnodes = FindObjectsOfType<Node>();
        m_allNodes = new List<Node>(allnodes);
        m_playerMover = Object.FindObjectOfType<PlayerMover>();
        m_goalNode = FindGoalNode();
        if (m_goalNode == null)
        {
            Debug.LogWarning("No Goal Node!");
        }
    }

    public void DrawBoard()
    {
        foreach(Node n in m_allNodes)
        {
            n.FirstDraw();
        }
    }

    Node FindGoalNode()
    {
        return m_allNodes.Find(n => n.IsGoalNode == true);
    }

    public Node GetNodeAt(Vector3 pos)
    {
        return m_allNodes.Find(n => n.Coordinate == Utility.Vector3Round(pos));
    }
}
