using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : OnOffObject {

    [SerializeField]
    Node bindingNode1;

    [SerializeField]
    Node bindingNode2;

    [SerializeField]
    Animator anim;

    bool opened = false;

    public override void TurnOff()
    {
        StartCoroutine(TurnOffRoutine());
    }

    private IEnumerator TurnOffRoutine()
    {
        if (opened)
        {
            opened = false;
            anim.Play("DoorClose");
            bindingNode1.DeleteLink(bindingNode2);
        }
        yield return null;
    }

    public override void TurnOn()
    {
        StartCoroutine(TurnOnRoutine());
    }
    private IEnumerator TurnOnRoutine()
    {
        if (!opened)
        {
            opened = true;
            anim.Play("DoorOpen");
            yield return new WaitForSeconds(.5f);
            bindingNode1.UpdateNodeStatus();
        }
        yield return null;
    }
}
