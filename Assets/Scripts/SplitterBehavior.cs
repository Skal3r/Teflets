

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterBehavior : MonoBehaviour
{
    [SerializeField]
    private float splitDistance = 1.75f;
    private GameObject spawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void split()
    {

        spawn = Instantiate(this.gameObject, transform.position, transform.rotation);
        spawn.transform.Translate(new Vector3(splitDistance, 0,0), Space.Self );
        //not an elegant solution - exposes director directly to this class
        spawn.GetComponent<BasicEntityController>().setDirector(AIDirector.instance);
        spawn.GetComponent<BasicEntityController>().addToDirector();
        transform.Translate(new Vector3( -splitDistance, 0, 0), Space.Self);

        
    }

}
