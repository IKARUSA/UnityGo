using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    enum Turn
    {
        Player,
        Enemy,
        Object
    };

    private Turn currentTurn;

    Board m_board;

    PlayerManager m_player;

    List<EnemyManager> m_enemies;

    List<ObjectManager> m_objects;

    bool m_hasLevelStarted = false;
    public bool HasLevelStarted { get { return m_hasLevelStarted; } set { m_hasLevelStarted = value; } }
    
    bool m_isGamePlaying = false;
    public bool IsGamePlaying { get { return m_isGamePlaying; } set { m_isGamePlaying = value; } }
    
    bool m_isGameOver = false;
    public bool IsGameOver { get { return m_isGameOver; } set { m_isGameOver = value; } }
    
    bool m_hasLevelFinished = false;
    public bool HasLevelFinished { get { return m_hasLevelFinished; } set { m_hasLevelFinished = value; } }

    [SerializeField]
    float delay = 1f;

    [SerializeField]
    UnityEvent startLevelEvent;
    [SerializeField]
    UnityEvent playLevelEvent;
    [SerializeField]
    UnityEvent endLevelEvent;

    private void Awake()
    {
        m_board = GameObject.FindObjectOfType<Board>();
        m_player = GameObject.FindObjectOfType<PlayerManager>();
        m_enemies = new List<EnemyManager>(GameObject.FindObjectsOfType<EnemyManager>());
        m_objects = new List<ObjectManager>(GameObject.FindObjectsOfType<ObjectManager>());
    }

    private void Start()
    {
        if(m_player != null)
        {
            StartCoroutine(RunGameLoop());
        }
    }

    private IEnumerator RunGameLoop()
    {
        yield return StartLevelRoutine();
        yield return PlayLevelRoutine();
        yield return EndLevelRoutine();
    }

    private IEnumerator StartLevelRoutine()
    {
        if (startLevelEvent != null)
            startLevelEvent.Invoke();
        m_player.Input.InputEnabled = false;
        yield return null;
        while (!m_hasLevelStarted)
        {
            yield return null;
        }
    }

    private IEnumerator PlayLevelRoutine()
    {
        if (playLevelEvent != null)
            playLevelEvent.Invoke();
        yield return new WaitForSeconds(3f);
        m_player.Input.InputEnabled = true;
        while(!m_isGameOver){
            yield return null;
            m_isGameOver = IsWinner();
        }
    }

    private bool IsWinner()
    {
        if (m_board.PlayerNode != null && m_board.GoalNode != null)
        {
            return m_board.PlayerNode == m_board.GoalNode;
        }
        else
            return false;
    }

    private IEnumerator EndLevelRoutine()
    {
        m_player.Input.InputEnabled = false;
        if (endLevelEvent != null)
            endLevelEvent.Invoke();
        yield return null;
    }

    public void PlayLevel()
    {
        m_hasLevelStarted = true;
    }

    #region turnManaging

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
    #endregion
    
}
