using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterShieldBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float shrinkAmount = 0.5f;
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
       
        if (collision.gameObject.CompareTag("Bullet")&&transform.localScale.x>0.5) {
           
            transform.localScale -= new Vector3(shrinkAmount, 0, 0);
            
            parent.split();
        }
    }
}
