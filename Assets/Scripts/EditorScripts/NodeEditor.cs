using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NodeEditor : MonoBehaviour {

    private void Update()
    {
        this.transform.position = Utility.Vector3Round(this.transform.position);
    }

}
