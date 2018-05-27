using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    Vector3 m_coordinate;
    public Vector3 Coordinate { get { return m_coordinate; } }

    List<Node> m_neighborNodes = new List<Node>();
    public List<Node> NeighborNodes { get { return m_neighborNodes; } }

    List<Node> m_linkedNodes = new List<Node>();
    public List<Node> LinkedNodes { get { return m_linkedNodes; } }

    Dictionary<Node, Link> m_linkDictionary = new Dictionary<Node, Link>();
    public Dictionary<Node, Link> LinkDictionary { get { return m_linkDictionary; } }

    [SerializeField]
    GameObject graphic;

    [SerializeField]
    Link m_linkPrefab;

    Board m_board;

    #region iTween variables
    [SerializeField]
    float scaleTime = .3f;
    [SerializeField]
    iTween.EaseType easeType = iTween.EaseType.easeInExpo;
    [SerializeField]
    float delay = 1f;
    #endregion

    [SerializeField]
    bool autoRun = false;

    bool isInitialized = false;

    [SerializeField]
    LayerMask obstacleLayerMask = LayerMask.GetMask();
    private void Awake()
    {
        m_board = GameObject.FindObjectOfType<Board>();
        m_coordinate = Utility.Vector3Round(transform.position);
    }

    private void Start()
    {
        if(graphic != null)
        {
            graphic.transform.localScale = Vector3.zero;
            if (m_board != null)
                m_neighborNodes = FindNeighbors(m_board.AllNodes);
            if (autoRun)
                InitNode();
        }
    }

    public void ShowGraphic()
    {
        if(graphic != null)
        {
            iTween.ScaleTo(graphic, iTween.Hash(
                "scale", Vector3.one,
                "time", scaleTime,
                "easetype", easeType,
                "delay", delay));
        }
    }

    public List<Node> FindNeighbors(List<Node> _nodes)
    {
        List<Node> nList = new List<Node>();

        foreach(Vector2 dir in Board.directions)
        {
            Node foundNeighbor = _nodes.Find(n => n.Coordinate == m_coordinate + Utility.V2toV3(dir));
            if(foundNeighbor != null && !nList.Contains(foundNeighbor))
            {
                if (!Physics.Raycast(transform.position, Utility.V2toV3(dir), Board.spacing, obstacleLayerMask))
                {
                    nList.Add(foundNeighbor);
                }
            }
        }

        return nList;
    }

    public void InitNode()
    {
        if (isInitialized == false)
        {
            isInitialized = true;
            ShowGraphic();
        }
        InitNeighbors();
    }

    private void InitNeighbors()
    {
        StartCoroutine(InitNeighborsRoutine());
    }

    IEnumerator InitNeighborsRoutine()
    {
        yield return new WaitForSeconds(.3f);
        foreach(Node neighbor in m_neighborNodes)
        {
            if (!m_linkedNodes.Contains(neighbor))
            {
                LinkNode(neighbor);
                neighbor.InitNode();
            }
        }
    }

    void LinkNode(Node targetNode)
    {
        if(m_linkPrefab != null)
        {
            GameObject linkInstance = Instantiate(m_linkPrefab.gameObject, transform.position, Quaternion.identity);
            linkInstance.transform.parent = transform;

            Link link = linkInstance.GetComponent<Link>();
            link.DrawLink(m_coordinate, targetNode.Coordinate);
            m_linkDictionary.Add(targetNode, link);
            link.transform.name = transform.name + " to " + targetNode.transform.name;

            m_linkedNodes.Add(targetNode);
            targetNode.LinkedNodes.Add(this);
        }
    }

    public void DeleteLink(Node targetNode)
    {
        if (m_linkedNodes.Contains(targetNode))
        {
            m_linkedNodes.Remove(targetNode);
            NeighborNodes.Remove(targetNode);
            if (m_linkDictionary.ContainsKey(targetNode))
            {
                Link linkToDelete = m_linkDictionary[targetNode];
                linkToDelete.DeleteLink();
                m_linkDictionary.Remove(targetNode);
            }
            targetNode.DeleteLink(this);
        }
    }

    public void DeleteAllLink()
    {
        for (int i = m_linkedNodes.Count-1; i >= 0; i--)
        {
            DeleteLink(m_linkedNodes[i]);
        }
    }

    public void UpdateNodeStatus()
    {
        StartCoroutine(UpdateNodeRoutine());
    }

    IEnumerator UpdateNodeRoutine()
    {
        m_coordinate = Utility.Vector3Round(transform.position);
        yield return null;
        m_neighborNodes = FindNeighbors(m_board.AllNodes);
        foreach(Node node in m_neighborNodes)
        {
            if (!node.NeighborNodes.Contains(this))
                node.NeighborNodes.Add(this);
        }

        InitNode();
    }
}
