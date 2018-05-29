using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MaskableGraphic))]
public class ScreenFader : MonoBehaviour {

    [SerializeField]
    Color solidColor = Color.white;
    [SerializeField]
    Color fadeColor = new Color(1f, 1f, 1f, 0f);

    [SerializeField]
    float fadeTime = 2f;
    [SerializeField]
    float delay = 0f;
    [SerializeField]
    iTween.EaseType easeType = iTween.EaseType.linear;

    MaskableGraphic graphic;

    private void Awake()
    {
        graphic = GetComponent<MaskableGraphic>();
    }

    void UpdateColor(Color newColor)
    {
        graphic.color = newColor;
    }

    public void FadeOff()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", solidColor,
            "to", fadeColor,
            "time", fadeTime,
            "delay", delay,
            "easetype", easeType,
            "onupdatetarget", gameObject,
            "onupdate", "UpdateColor"));
    }
    public void FadeOn()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", fadeColor,
            "to", solidColor,
            "time", fadeTime,
            "delay", delay,
            "easetype", easeType,
            "onupdatetarget", gameObject,
            "onupdate", "UpdateColor"));
    }
}
