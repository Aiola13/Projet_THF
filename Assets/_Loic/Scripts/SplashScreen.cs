using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public float timer = 2f;
    public string levelToLoad = "TestScene";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DisplayScene");
    }

    // Update is called once per frame
    IEnumerator DisplayScene()
    {
        //yield return new WaitForSeconds(timer);
        //SceneManager.LoadScene(levelToLoad);
        //SceneManager.LoadSceneAsync("levelToLoad");

        yield return new WaitForSeconds(timer);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad);
         

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
            
        }
    }
}
