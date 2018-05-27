using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtimeMovingPlatform : MovingPlatform {

    [SerializeField]
    float delayBetweenMove = 5f;

    float deltaT = 0f;

    private void Update()
    {
        deltaT += Time.deltaTime;
        if(deltaT >= delayBetweenMove)
        {
            Move();
            deltaT = 0f;
        }
    }
}
