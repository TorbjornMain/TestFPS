using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class RenderScreenEffect : MonoBehaviour {

    public Material[] mat;
    public Vector2 targetRes;
    public FilterMode textureFilterMode;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture srt;
        if (targetRes.x > 0 || targetRes.y > 0)
        {
            srt = RenderTexture.GetTemporary((int)targetRes.x > 0 ? (int)targetRes.x : source.width, (int)targetRes.y > 0 ? (int)targetRes.y : source.height);
            srt.filterMode = textureFilterMode;
            Graphics.Blit(source, srt);
        }
        else
        {
            srt = source;
            srt.filterMode = textureFilterMode;
        }
        if (mat.Length > 0)
        {
            for (int i = 0; i < mat.Length - 1; i++)
            {
                RenderTexture drt;
                drt = RenderTexture.GetTemporary(source.width, source.height);
                drt.filterMode = textureFilterMode;
                Graphics.SetRenderTarget(drt);
                Graphics.Blit(srt, mat[i]);
                if (i > 0)
                {
                    srt.Release();
                }
                srt = drt;
            }

            Graphics.SetRenderTarget(destination);
            Graphics.Blit(srt, mat[mat.Length - 1]);
            srt.Release();
        }
        else
        {
            Graphics.Blit(srt, destination);
            srt.Release();
        }
    }
}
