using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterShieldBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private SplitterBehavior parent;
    void Start()
    {
        parent = GetComponentInParent<SplitterBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) {
            //TODO: add behavior to shrink shield before splitting
            parent.split();
        }
    }
}
