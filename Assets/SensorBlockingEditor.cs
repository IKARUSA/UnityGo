using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SensorBlockingEditor : MonoBehaviour {
    private void Update()
    {
        this.transform.position = Utility.Vector3Round(transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}
