using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour {

    Renderer r;
    bool glowing = false;
    public Color glowColor;


    void Start()
    {
        r = GetComponent<Renderer>();
    }

    void Update()
    {
        if(glowing)
        {
            glowing = false;
            r.material.SetColor("_GlowColor", glowColor);
        }
        else
        {
            r.material.SetColor("_GlowColor", Color.black);
        }
    }

    void Highlight()
    {
        glowing = true;
    }
}
