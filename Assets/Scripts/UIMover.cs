using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIMoverMode
{
    MoveTo,
    ScaleTo,
    MoveFrom
}

public class UIMover : MonoBehaviour {

    [SerializeField]
    UIMoverMode moveMode;

    [SerializeField]
    Transform startXform;
    [SerializeField]
    Transform endXform;

    [SerializeField]
    float moveTime = 1f;
    [SerializeField]
    float delay = 0f;
    [SerializeField]
    iTween.LoopType loopType = iTween.LoopType.none;
    [SerializeField]
    iTween.EaseType easeType = iTween.EaseType.easeOutExpo;

    private void Awake()
    {
        if(startXform == null)
        {
            startXform = new GameObject(gameObject.name + " startXform").transform;
            startXform.position = transform.position;
            startXform.localScale = transform.localScale;
            startXform.rotation = transform.rotation;
        }
        if (endXform == null)
        {
            endXform = new GameObject(gameObject.name + " endXform").transform;
            endXform.position = transform.position;
            endXform.localScale = transform.localScale;
            endXform.rotation = transform.rotation;
        }
        Reset();
    }

    public void Reset()
    {
        switch (moveMode)
        {
            case UIMoverMode.MoveFrom:
                transform.position = endXform.position;
                break;
            case UIMoverMode.MoveTo:
                transform.position = startXform.position;
                break;
            case UIMoverMode.ScaleTo:
                transform.localScale = startXform.localScale;
                break;
        }
        startXform.parent = transform;
        endXform.parent = transform;
    }

    public void Move()
    {
        switch (moveMode)
        {
            case UIMoverMode.MoveFrom:
                iTween.MoveFrom(gameObject, iTween.Hash(
                    "position",startXform.position,
                    "time",moveTime,
                    "delay",delay,
                    "easetype",easeType,
                    "looptype",loopType));
                break;
            case UIMoverMode.MoveTo:
                iTween.MoveTo(gameObject, iTween.Hash(
                    "position",endXform.position,
                    "time",moveTime,
                    "delay",delay,
                    "easetype",easeType,
                    "looptype",loopType));
                break;
            case UIMoverMode.ScaleTo:
                iTween.ScaleTo(gameObject, iTween.Hash(
                    "scale", endXform.localScale,
                    "time", moveTime,
                    "delay", delay,
                    "easetype", easeType,
                    "looptype", loopType));
                break;
        }
    }
}
