using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySensor))]
[RequireComponent(typeof(EnemyMover))]
public class EnemyManager : TurnManager {
    
    private EnemySensor m_enemySensor;
    EnemyMover m_enemyMover;

    protected override void Awake()
    {
        base.Awake();
        m_enemySensor = GetComponent<EnemySensor>();
        m_enemyMover = GetComponent<EnemyMover>();
    }

    public override void PlayTurn()
    {
        StartCoroutine(PlayTurnRoutine());
    }

    IEnumerator PlayTurnRoutine()
    {
        m_enemyMover.Move();
        yield return new WaitForSeconds(.5f);
        FinishTurn();
    }
}
