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

    [SerializeField]
    TextMesh informText;
    [SerializeField]
    GameObject informGameObject;
    public override void PlayTurn()
    {
        if(targetNode != null && targetNode == m_board.PlayerNode)
        {
            iTween.ScaleTo(informGameObject, iTween.Hash("scale", Vector3.one, "time", .5f));
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
        if(currentDuration >= 0)
        {
            informText.text = currentDuration.ToString();
        }
        else
        {
            iTween.ScaleTo(informGameObject, iTween.Hash("scale", Vector3.zero, "time", .5f));
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
