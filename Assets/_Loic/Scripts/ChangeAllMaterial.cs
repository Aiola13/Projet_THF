using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ChangeAllMaterial
{
    public static void ChangeMaterial(MeshRenderer mr, string blendMode)
    {
        SetupMaterialWithBlendMode(mr.material, blendMode);
        SetAlpha(mr.material, 0);
    }


     /// <summary>
    /// Permet de changer le mode du shader standard
    /// </summary>
    /// <param name="material">le material à utiliser</param>
    /// <param name="blendMode"> le mode voulu : Opaque , Cutout, Fade, Transparent</param>
    public static void SetupMaterialWithBlendMode(Material material, string blendMode)
    {
        switch (blendMode)
        {
            case "Opaque":
                material.SetFloat("_Mode", 0);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case "Cutout":
                material.SetFloat("_Mode", 1);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case "Fade":
                material.SetFloat("_Mode", 2);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case "Transparent":
                material.SetFloat("_Mode", 3);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }

    public static void SetAlpha (Material material, float value)
    {
        Color color = material.color;
        color.a = value;
        material.color = color;
    }

    public static IEnumerator SetAlphaOverTime (Material material, string blendMode)
    {
        switch (blendMode)
        {
            case "Opaque":
                for(float f = 0.05f; f <= 1; f+= 0.1f)
                {
                    Color color = material.color;
                    //color.a = Mathf.Lerp( 0f, 1f, 25.0f * Time.deltaTime);
                    color.a = f;
                    material.color = color;
                    yield return new WaitForSeconds(0.001f);
                }
                SetupMaterialWithBlendMode(material, blendMode);  
            break;
            case "Fade":
                SetupMaterialWithBlendMode(material, blendMode);  
                for(float f = 1f; f >= 0.05f; f-= 0.1f)
                {
                    Color color = material.color;
                    //color.a = Mathf.Lerp( 0f, 1f, 25.0f * Time.deltaTime);
                    color.a = f;
                    material.color = color;
                    yield return new WaitForSeconds(0.001f);
                }
            break;
        }
        yield return material.color;
    }
}
