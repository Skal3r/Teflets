using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{

    public int damageAmount = 10;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            anim.SetTrigger("inatkrange");
        }
    }

    //deals damage to player - should probably offload to somewhere else
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().DoDamage(damageAmount);
        }
    }
    private void OnDestroy()
    {
        
    }

}
