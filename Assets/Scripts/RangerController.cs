using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerController : MonoBehaviour
{
    [SerializeField]
    private GameObject gunEnd;
    [SerializeField]
    private float atkRange = 10.0f;
    [SerializeField]
    private float atkFrequency = 1.0f;
    [SerializeField]
    private float timeBetweenBullets;//can be offloaded to a gun subclass
    [SerializeField]
    private float shootingTime = 1.0f;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject player;
    private bool inAttackMode = true;
    private bool isAttacking = false;
    private float atkTimer = 0;//how long to fire bullets for
 
    private float atkwait = 0.0f;//time between bursts, if attacking
    private float shotTimer =0;
    // Start is called before the first frame update
    void Start()
    {
        shotTimer = timeBetweenBullets;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isAttacking)
        {
            atkTimer += Time.deltaTime;
            shotTimer -= Time.deltaTime;
        }
        else
        {
            atkwait += Time.deltaTime;
        }
        if (atkwait > atkFrequency) {
            isAttacking = true;
        }
        if (atkTimer > shootingTime) {
            isAttacking = false;
            atkTimer = 0;
        }
        if (Vector3.Distance(transform.position, player.transform.position) < atkRange&&inAttackMode)
        {
            attack();
        }


    }

    private void attack() {
        if (isAttacking&&shotTimer<0) {
            atkwait = 0;
            shotTimer = timeBetweenBullets;
         
            Instantiate(bullet, gunEnd.transform.position, gunEnd.transform.rotation);
        }
    }

    private void turnOffAttack() {
        inAttackMode = false;
    }
    private void turnOnAttack() {
        inAttackMode = true;
    }
}
