using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySensor))]
public class EnemyManager : TurnManager {

    public enum EnemyType
    {
        Stationary,
        Patrol,
        Sentry
    }

    [SerializeField]
    private EnemyType m_type = EnemyType.Stationary;

    private EnemySensor m_enemySensor;

    protected override void Awake()
    {
        base.Awake();
        m_enemySensor = GetComponent<EnemySensor>();
    }

    public override void PlayTurn()
    {
        base.PlayTurn();
    }
}
