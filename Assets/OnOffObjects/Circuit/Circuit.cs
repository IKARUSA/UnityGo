using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circuit : OnOffObject
{
    [SerializeField]
    Material offMat;
    [SerializeField]
    Material onMat;

    Renderer render;

    Material[] offmats;
    Material[] onmats;

    private void Awake()
    {
        render = GetComponent<Renderer>();
        offmats = new Material[1];
        onmats = new Material[1];
        offmats[0] = offMat;
        onmats[0] = onMat;

        TurnOff();
    }

    public override void TurnOff()
    {
        render.materials = offmats;
    }

    public override void TurnOn()
    {
        render.materials = onmats;
    }
}
