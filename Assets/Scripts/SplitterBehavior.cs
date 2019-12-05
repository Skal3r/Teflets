

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterBehavior : MonoBehaviour
{
    
    private float sizeAmount = 0.5f;
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
        transform.localScale += new Vector3(sizeAmount, 0, sizeAmount);
    
        Instantiate(this, transform).transform.Translate(new Vector3(0, 1,0), Space.Self );
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) { 
            split();
        }
    }
}
