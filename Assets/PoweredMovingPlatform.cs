using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredMovingPlatform : OnOffObject {

    bool isTurnedOn = false;
    [SerializeField]
    MovingPlatform movingPlatform;

    public override void TurnOff()
    {
        if (isTurnedOn)
        {
            isTurnedOn = false;
            movingPlatform.Move();
        }
    }

    public override void TurnOn()
    {
        if (!isTurnedOn)
        {
            isTurnedOn = true;
            movingPlatform.Move();
        }
    }
    
}
