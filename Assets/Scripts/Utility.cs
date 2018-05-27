using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility {
    
    public static Vector3 Vector3Round(Vector3 _vector3)
    {
        return new Vector3(Mathf.Round(_vector3.x), Mathf.Round(_vector3.y), Mathf.Round(_vector3.z));
    }
    
    public static Vector3 V2toV3(Vector2 _vector2)
    {
        return Vector3Round(new Vector3(_vector2.x, 0f, _vector2.y));
    }
}
