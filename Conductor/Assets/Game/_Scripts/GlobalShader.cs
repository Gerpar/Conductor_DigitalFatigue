using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalShader : MonoBehaviour
{
    // Start is called before the first frame update
    public Shader gblShader;
    void Start()
    {
        Camera.main.SetReplacementShader(gblShader, "RenderType");//sets a global shader in that camera
        //Camera.main.RenderWithShader(gblShader, "RenderType");
    }
}
