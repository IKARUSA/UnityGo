using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : ObjectManager {

    [SerializeField]
    List<OnOffObject> bindingObjects = new List<OnOffObject>();

    [SerializeField]
    private int maxDuration = 3;
    private int currentDuration = 0;

    [SerializeField]
    Node targetNode;

    public override void PlayTurn()
    {

        if(targetNode != null && targetNode == m_board.PlayerNode)
        {
            currentDuration = maxDuration;
            TurnBindings(true);
        }
        else
        {
            currentDuration--;
            if(currentDuration > 0)
            {
                TurnBindings(true);
            }
            else
            {
                TurnBindings(false);
            }
        }
        base.PlayTurn();
    }

    private void TurnBindings(bool state)
    {
        foreach (OnOffObject bindingObject in bindingObjects)
        {
            if (state)
                bindingObject.TurnOn();
            else
                bindingObject.TurnOff();
        }
    }
}
