using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dome : MonoBehaviour
{
    public Material materialTransparent;
    public Material materialData;
    public MeshRenderer render;
    [SerializeField] public AudioSource sound;

    public bool once = true;

    // animate the game object from -1 to +1 and back
    public float minimum = -1.0F;
    public float maximum =  1.0F;

    // starting value for the Lerp
    static float t = 0.0f;

    public bool isFading = true;

    IEnumerator DataTransfered()
    {
        yield return new WaitForSeconds(0.1f);

        float emission = Mathf.PingPong (Time.time, 5.0f);
        //Color baseColor = Color.yellow; //Replace this with whatever you want for your base color at emission level '1'
        Color baseColor = new Vector4(21.36126f, 21.36126f, 21.36126f, 0);
        Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
 
        render.material.SetColor ("_EmissionColor", finalColor);

        yield return new WaitForSeconds(0.1f);
        

        emission = Mathf.PingPong (Time.time, 1.0f);
        //baseColor = Color.yellow; //Replace this with whatever you want for your base color at emission level '1'
        baseColor = new Vector4(21.36126f, 21.36126f, 21.36126f, 10);
        finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
 
        render.material.SetColor ("_EmissionColor", finalColor);

        StartCoroutine(DataTransfered());
    }

    public void StartSubCourotine()
    {
        sound.Play();

        render.material = materialData;
        StartCoroutine(SetAlphaOverTime(render.material, "Opaque"));
        StartCoroutine(DataTransfered());
    }

    public void StopSubCourotine()
    {
        sound.enabled = false;
        StopCoroutine(DataTransfered());
        if(once)
        {
            render.material = materialTransparent;
            once = false;
        }
            
        if(isFading)
            StartCoroutine(SetAlphaOverTime(render.material, "Fade"));
        
        
    }


    public IEnumerator SetAlphaOverTime (Material material, string blendMode)
    {
        switch (blendMode)
        {
            case "Opaque":
                render.gameObject.layer = 9;
                render.tag = "Data";
                for(float f = 0.05f; f <= 1; f+= 0.1f)
                {
                    Color color = material.color;
                    //color.a = Mathf.Lerp( 0f, 1f, 25.0f * Time.deltaTime);
                    color.a = f;
                    material.color = color;
                    yield return new WaitForSeconds(0.01f);
                }
            break;
            case "Fade":
                render.gameObject.layer = 0;
                render.tag = "Untagged";
                for(float f = 1f; f >= 0.05f; f-= 0.1f)
                {
                    Color color = material.color;
                    //color.a = Mathf.Lerp( 0f, 1f, 25.0f * Time.deltaTime);
                    color.a = f;
                    material.color = color;
                    yield return new WaitForSeconds(0.01f);
                }
                isFading = false;
            break;
        }
    }
}
