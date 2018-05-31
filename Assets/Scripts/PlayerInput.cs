using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private float h, v;
    public float H { get { return h; } }
    public float V { get { return v; } }

    private bool inputEnabled = true;
    public bool InputEnabled { get { return inputEnabled; } set { inputEnabled = value; } }

    public void GetInput()
    {
        if (!inputEnabled)
        {
            h = 0f;
            v = 0f;
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }
    }

    
}
