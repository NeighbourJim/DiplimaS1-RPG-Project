using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeTransparent : MonoBehaviour, IFadable
{
    bool beingHit = false;

    public void FadeIn()
    {

    }
    
    public void FadeOut()
    {
        Material mat = gameObject.GetComponent<Renderer>().material;
        ChangeRenderMode(mat, BlendMode.Transparent);
        Debug.Log("Hi");
        while(mat.color.a > 0.3f)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - 0.1f);
            Debug.Log(mat.color.a);
        }
    }

    public enum BlendMode
    {
        Opaque,
        Transparent
    }

    public void ChangeRenderMode(Material mat, BlendMode mode)
    {
        switch (mode)
        {
            case BlendMode.Opaque:
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = -1;
                break;

            case BlendMode.Transparent:
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;
                break;
        }
    }
}
