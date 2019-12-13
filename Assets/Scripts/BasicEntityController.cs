using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BasicEntityController : MonoBehaviour
{

    NavMeshAgent thisAgent;
    public int moveSpeed = 5;
    [SerializeField]
    private GameObject[] moveLocations;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private bool isIdle = true;
    private int numLocations = 10;
    private int curr_Location_size = 0;
    [SerializeField]
    private GameObject currentGoal;

    private float slowspeed = 5;
    private float fastSpeed = 10;
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
        thisAgent.speed = moveSpeed;


    }

    // Update is called once per frame
    void Update()
    {//if not idle, then move(2 states)
        if (!isIdle)
        {
            try
            {
                thisAgent.SetDestination(currentGoal.transform.position);
            }
            catch { }

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
        if (Vector3.Distance(player.transform.position, this.transform.position) < 5) {

            thisAgent.speed = fastSpeed;
        }
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
        thisAgent.speed = slowspeed;
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
        if (director != null)
        {
            director.RemoveEntity(this.gameObject);
        }
    }

    //should only be used once on object creation to link to AI director - perhaps move to another class?
    public void setDirector(AIDirector newDirector)
    {
        director = newDirector;
    }
    public void addToDirector() {
        director.setupAndAddBasicEntityController(this);
    }

    public AIDirector getDirector() {
        return director;
    }

    public float getDistanceToPlayer() {
        return Vector3.Distance(this.transform.position, player.transform.position);
    }
}
