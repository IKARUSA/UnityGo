using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedMovingPlatform : MovingPlatform {

    protected override IEnumerator PlatformMoveRoutine()
    {
        yield return base.PlatformMoveRoutine();
        m_gameManager.UpdateTurn();
    }
}
