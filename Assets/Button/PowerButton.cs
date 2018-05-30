using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : ButtonObject {

    [SerializeField]
    TextMesh informText;
    [SerializeField]
    GameObject informGameObject;
    public override void PlayTurn()
    {
        if(targetNode != null && targetNode == m_board.PlayerNode)
        {
            if(informGameObject != null)
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
            if(informText!=null)
                informText.text = currentDuration.ToString();
        }
        else
        {
            if(informGameObject != null)
                iTween.ScaleTo(informGameObject, iTween.Hash("scale", Vector3.zero, "time", .5f));
        }
        base.PlayTurn();
    }
    


}
