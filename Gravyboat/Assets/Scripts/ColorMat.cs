using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ColorMat : MonoBehaviour
{
    private Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
        light.color = Color.HSVToRGB(1,.6f,.6f);
    }

    // Update is called once per frame
    void Update()
    {
        light.color = Color.HSVToRGB(Mathf.PingPong(Time.time / 3, 1), .6f, .6f);
    }
}
