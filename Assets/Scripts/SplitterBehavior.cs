

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterBehavior : MonoBehaviour
{
    
    private float sizeAmount = 0.5f;
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
        if (transform.localScale.z > sizeAmount && transform.localScale.x > sizeAmount) 
        {
            transform.localScale -= new Vector3(sizeAmount, 0, sizeAmount);
        }



        spawn = Instantiate(GetComponentInParent<Transform>().gameObject, transform.position, transform.rotation);
        spawn.transform.Translate(new Vector3(3, 0,3), Space.Self );

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) { 
            split();
        }
    }
}
