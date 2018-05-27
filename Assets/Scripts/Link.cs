using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {
    [SerializeField]
    float borderWidth = .07f;
    [SerializeField]
    float lineThickness = .3f;
    [SerializeField]
    float scaleTime = .3f;
    [SerializeField]
    float delay = .1f;
    [SerializeField]
    iTween.EaseType easeType = iTween.EaseType.easeInOutSine;

    public void DrawLink(Vector3 startPos,Vector3 endPos)
    {
        transform.localScale = new Vector3(lineThickness, 1f, 0f);

        Vector3 dirvector = endPos - startPos;

        float zScale = dirvector.magnitude - borderWidth * 2f;

        Vector3 newScale = new Vector3(lineThickness, 1f, zScale);

        transform.rotation = Quaternion.LookRotation(dirvector);

        transform.position = transform.position + dirvector.normalized * borderWidth;

        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", newScale,
            "time", scaleTime,
            "easetype", easeType,
            "delay", delay));
    }

    public void DeleteLink()
    {
        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", new Vector3(lineThickness, 1f, 0f),
            "time", scaleTime,
            "easetype", easeType,
            "delay", delay));
        Invoke("DestroySelf", delay + scaleTime);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

}
