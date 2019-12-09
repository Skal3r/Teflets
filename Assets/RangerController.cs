using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerController : MonoBehaviour
{
    [SerializeField]
    private GameObject gunEnd;
    [SerializeField]
    private float atkRange;
    [SerializeField]
    private float atkFrequency;
    [SerializeField]
    private float atkSpeed;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject player;

    private bool isAttacking = false;
    private float atkTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        atkTimer += Time.deltaTime;
    }

    private void attack() {
        if (isAttacking) {
            Instantiate(bullet, gunEnd.transform.position, gunEnd.transform.rotation);
        }
    }
}
