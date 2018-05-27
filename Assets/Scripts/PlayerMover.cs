using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : Mover {

    protected override void Move(Vector3 targetPos)
    {
        base.Move(targetPos);
    }

    protected override IEnumerator MoveRoutine()
    {
        yield return base.MoveRoutine();

    }
}
