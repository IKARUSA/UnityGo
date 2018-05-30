using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : ButtonObject {
    
    public override void PlayTurn()
    {
        StartCoroutine(UpdateStatusRoutine());
    }

    private IEnumerator UpdateStatusRoutine()
    {
        if (m_gameManager.GetMoverAtPoint(targetNode) != null)
        {
            TurnBindings(true);
        }
        else
        {
            TurnBindings(false);
        }
        yield return new WaitForSeconds(.5f);
        FinishTurn();
    }
}
