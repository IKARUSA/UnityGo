using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : ObjectManager {

    [SerializeField]
    protected List<OnOffObject> bindingObjects = new List<OnOffObject>();

    [SerializeField]
    protected int maxDuration = 3;
    protected int currentDuration = 0;

    [SerializeField]
    protected Node targetNode;
    
    protected void TurnBindings(bool state)
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
