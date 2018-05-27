using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    enum Turn
    {
        Player,
        Enemy,
        Object
    };

    private Turn currentTurn;

    PlayerManager m_player;

    List<EnemyManager> m_enemies;

    List<ObjectManager> m_objects;

    private void Awake()
    {
        m_player = Object.FindObjectOfType<PlayerManager>();
        m_enemies = new List<EnemyManager>(Object.FindObjectsOfType<EnemyManager>());
        m_objects = new List<ObjectManager>(Object.FindObjectsOfType<ObjectManager>());
        List<TurnManager> tempList = new List<TurnManager>(Object.FindObjectsOfType<TurnManager>());
        Debug.Log(tempList.Count);
    }

    public void UpdateTurn()
    {
        switch (currentTurn)
        {
            case Turn.Player:
                PlayEnemyTurn();
                break;
            case Turn.Enemy:
                PlayObjectTurn();
                break;
            case Turn.Object:
                PlayPlayerTurn();
                break;
        }
    }

    private bool IsFinished(List<TurnManager> target)
    {
        foreach(TurnManager turnObj in target)
        {
            if (turnObj.IsTurnComplete == false)
                return false;
        }
        return true;
    }
    

    private void PlayEnemyTurn()
    {
        if (m_player.IsTurnComplete)
        {
            if(m_enemies.Count == 0)
            {
                PlayObjectTurn();
                return;
            }
            currentTurn = Turn.Enemy;
            foreach(EnemyManager enemy in m_enemies)
            {
                enemy.IsTurnComplete = false;
            }
            foreach(EnemyManager enemy in m_enemies)
            {
                enemy.PlayTurn();
            }
        }
    }

    private void PlayObjectTurn()
    {
        if (IsFinished(new List<TurnManager>(m_enemies.ToArray())))
        {
            if (m_objects.Count == 0)
            {
                PlayPlayerTurn();
                return;
            }
            currentTurn = Turn.Object;
            foreach(ObjectManager obj in m_objects)
            {
                obj.IsTurnComplete = false;
            }
            foreach(ObjectManager obj in m_objects)
            {
                obj.PlayTurn();
            }
        }
    }

    private void PlayPlayerTurn()
    {
        if (IsFinished(new List<TurnManager>(m_objects.ToArray())))
        {
            currentTurn = Turn.Player;
            m_player.IsTurnComplete = false;
        }
    }
}
