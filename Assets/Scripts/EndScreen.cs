using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class EndScreen : MonoBehaviour {
    [SerializeField]
    PostProcessingBehaviour cam;
    [SerializeField]
    PostProcessingProfile blurProfile;
    [SerializeField]
    PostProcessingProfile normalProfile;

    public void EnableCamBlur(bool state)
    {
        if(cam != null && blurProfile != null && normalProfile != null)
        {
            cam.profile = state ? blurProfile : normalProfile;
        }
    }
}
