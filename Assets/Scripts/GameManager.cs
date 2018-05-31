using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    enum Turn
    {
        Player,
        Enemy,
        Object
    };

    private Turn currentTurn;

    private Turn prevTurn;

    Board m_board;

    PlayerManager m_player;

    List<EnemyManager> m_enemies;

    List<ObjectManager> m_objects;

    List<Mover> m_allMover;

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
    [SerializeField]
    UnityEvent gameOverEvent;

    private void Awake()
    {
        m_board = GameObject.FindObjectOfType<Board>();
        m_player = GameObject.FindObjectOfType<PlayerManager>();
        m_enemies = new List<EnemyManager>(GameObject.FindObjectsOfType<EnemyManager>());
        m_objects = new List<ObjectManager>(GameObject.FindObjectsOfType<ObjectManager>());
        m_allMover = new List<Mover>(GameObject.FindObjectsOfType<Mover>());
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
        yield return new WaitForSeconds(1.5f);
        m_player.Input.InputEnabled = true;
        while(!m_isGameOver){
            yield return null;
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

    public void GameOver()
    {
        m_player.Input.InputEnabled = false;
        StartCoroutine(GameOverRoutine());
    }

    public IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(.5f);
        if (gameOverEvent != null)
        {
            gameOverEvent.Invoke();
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #region turnManaging

    public void UpdateTurn()
    {
        switch (currentTurn)
        {
            case Turn.Player:
                prevTurn = Turn.Player;
                PlayObjectTurn();
                break;
            case Turn.Enemy:
                prevTurn = Turn.Enemy;
                PlayObjectTurn();
                break;
            case Turn.Object:
                if (prevTurn == Turn.Player)
                    PlayEnemyTurn();
                else
                    PlayPlayerTurn();
                break;
        }
        m_isGameOver = IsWinner();
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
        if (IsFinished(new List<TurnManager>(m_objects.ToArray())))
        {
            if(m_enemies.Count == 0)
            {
                PlayPlayerTurn();
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
        bool isturnFinished = (prevTurn == Turn.Player) ? m_player.IsTurnComplete : IsFinished(new List<TurnManager>(m_enemies.ToArray()));
        if (isturnFinished)
        {
            if (m_objects.Count == 0)
            {
                if (prevTurn == Turn.Player)
                    PlayEnemyTurn();
                else
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
    
    public Mover GetMoverAtPoint(Node node)
    {
        return m_allMover.Find(n => n.CuurentNode == node);
    }
}
