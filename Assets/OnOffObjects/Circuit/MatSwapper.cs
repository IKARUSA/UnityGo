using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatSwapper : OnOffObject
{
    [SerializeField]
    Material[] offmats;
    [SerializeField]
    Material[] onmats;

    Renderer render;

    private void Awake()
    {
        render = GetComponent<Renderer>();

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
