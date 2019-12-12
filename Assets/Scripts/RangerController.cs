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

    private bool inAttackMode = true;
    private bool isAttacking = false;
    private float atkTimer = 0;//how long to fire bullets for
 
    private float atkwait = 0.0f;//time between bursts, if attacking
    private float shotTimer =0;

    private BasicEntityController movement;
    // Start is called before the first frame update
    void Start()
    {
        shotTimer = timeBetweenBullets;
        movement = GetComponent<BasicEntityController>();
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
            movement.idleOff();
            atkTimer = 0;
        }
        if (movement.getDistanceToPlayer() < atkRange && inAttackMode)
        {

            movement.idleOn();
            movement.lookAtPlayer();
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
