

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterBehavior : MonoBehaviour
{
    
    private float sizeAmount = 0.5f;
    private float splitDistance = 1.0f;
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
        transform.Translate(new Vector3( -splitDistance, 0, 0), Space.Self);
        
    }

}
