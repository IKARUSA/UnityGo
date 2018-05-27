using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BlockingEditor : MonoBehaviour {

    private void Update()
    {
        this.transform.position = Utility.Vector3Round(transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}
