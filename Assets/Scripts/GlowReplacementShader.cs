using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowReplacementShader : MonoBehaviour {

    public Shader replacementShader;
    public Shader glowProcessEffect;
    public Shader glowCompositeEffect;
    Camera c;


    private void OnEnable()
    {
        c = GetComponent<Camera>();
        if (replacementShader != null)
            c.SetReplacementShader(replacementShader, "Glowable");

    }

    private void OnDisable()
    {
        c.ResetReplacementShader();
    }

    private void Update()
    {
        c.targetTexture.Release();
        c.targetTexture.width = Screen.width;
        c.targetTexture.height = Screen.height;
        c.targetTexture.Create();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture processedGlow = RenderTexture.GetTemporary(source.width, source.height);
        Graphics.SetRenderTarget(processedGlow);
        Graphics.Blit(source, new Material(glowProcessEffect));
        Material m = new Material(glowCompositeEffect);
        m.SetTexture("_ProcessedGlow", processedGlow);
        Graphics.SetRenderTarget(destination);
        Graphics.Blit(source, m);
        processedGlow.Release();
    }

}
