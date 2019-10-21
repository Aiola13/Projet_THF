using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GameManager : MonoBehaviour
{
    protected static GameManager instance = null;

    public GameObject arm;
    public GameObject pole;
    public GameObject platform;
    public Transform prefab;
    public List<GameObject> platforms = new List<GameObject>();
    Coroutine maCoroutine;
    

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //if(platform != null)
           
    }

    // Update is called once per frame
    void Update()
    {
        /*if(gameObject.name == GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject().name)
        {
            platform.GetComponent<Animator>().SetBool("Open", true);
            arm.GetComponent<Animator>().SetBool("In", true);
        }*/
    }
  
    
}
