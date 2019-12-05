using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BasicEntityController : MonoBehaviour
{

    NavMeshAgent thisAgent;
    public int moveSpeed;
    private GameObject[] moveLocations;
    private GameObject player;
    private bool isIdle = true;
    private int numLocations = 10;
    private int curr_Location_size = 0;
    private GameObject currentGoal;

    private AIDirector director;



    public BasicEntityController()
    {
        moveLocations = new GameObject[numLocations];
    }
    //default behaviour is to to look at player- might have to change later
    private bool lookAtPLayer = true;

    void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {//if not idle, then move(2 states)
        if (!isIdle)
        {
            thisAgent.SetDestination(currentGoal.transform.position);


        }
        //looks at player or current goal
        if (player && lookAtPLayer)
        {
            transform.LookAt(player.transform.position);
        }
        else if (currentGoal != null)
        {
            transform.LookAt(currentGoal.transform.position);
        }

        if (thisAgent.remainingDistance < 0.9) {
            director.ReachedDestination(this);
        }

    }

    //sets player object as a reference
    public void setPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }
    //sets player as current target
    public void targetPlayer()
    {
        currentGoal = player;
    }
    //adds gameobject to target list,
    public void addlocation(GameObject newLocation)
    {
        if (curr_Location_size < numLocations)
        {
            moveLocations.SetValue(newLocation, curr_Location_size);
            curr_Location_size++;
        }
    }

    //targets a specific gameobject number
    //TODO: specify to set a gameobject as an overload
    public void targetLocationNum(int locNum)
    {
        if (locNum < curr_Location_size)
        {
            currentGoal = moveLocations[locNum];
        }
    }



    //set idle off
    public void idleOff()
    {
        isIdle = false;
    }
    //set idle on 
    public void idleOn()
    {
        isIdle = true;
    }
    //sets whether or not to look at player
    public void lookAtPlayer()
    {
        lookAtPLayer = true;
    }

    private void stopLookingAtPLayer()
    {
        lookAtPLayer = false;
    }

    /// <summary>
    /// Remove below functions and offload them to another class to define entity behaviors more in scope with functions intentions
    /// </summary>


    //currently a test function - unsure if ondestroy reponsibility shoud be here or somewhere else
    private void OnDestroy()
    {
        director.RemoveEntity(this.gameObject);
    }

    //should only be used once on object creation to link to AI director - perhaps move to another class?
    public void setDirector(AIDirector newDirector)
    {
        director = newDirector;
    }


}
