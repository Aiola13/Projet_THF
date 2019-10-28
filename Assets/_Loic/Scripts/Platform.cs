using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public bool animationEnded = false;
    [SerializeField] public bool stateOpen = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenPlatform(string _tag)
    {
        if(_tag == "PlatformPrinter")
        {
            Debug.Log("OpenPlatform" + _tag);
            this.GetComponent<Animator>().SetBool("CloseComplete", false);
            this.GetComponent<Animator>().SetBool("OpenComplete", true);
        }
        else
        {
            Debug.Log("OpenPlatform" + _tag);
            this.GetComponent<Animator>().SetBool("Close", false);
            this.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    public void ClosePlatform(string _tag)
    {
        if(_tag == "PlatformPrinter")
        {
            Debug.Log("OpenPlatform" + _tag);
            this.GetComponent<Animator>().SetBool("CloseComplete", false);
            this.GetComponent<Animator>().SetBool("OpenComplete", true);
        }
        else
        {
            Debug.Log("ClosePlatform");
            this.GetComponent<Animator>().SetBool("Open", false);
            this.GetComponent<Animator>().SetBool("Close", true);
        } 
    }
    
    public IEnumerator OpenPlatformDelay(string _tag, float time)
    {
        if(this.tag == _tag)
        {
            Debug.Log("OpenPlatformDelay");
            this.GetComponent<Animator>().SetBool("Close", false);
            this.GetComponent<Animator>().SetBool("Open", true);
            yield return new WaitForSeconds(time);
        }

        yield return null;
        
    }

    void AlertEvents(string message)
    {
        if (message.Equals("AnimationOver"))
        {
            Debug.Log("Animation Over");
            animationEnded = true;
            stateOpen = !stateOpen;
        } 
    }
}
