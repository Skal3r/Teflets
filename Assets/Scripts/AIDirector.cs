using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDirector : MonoBehaviour
{
    private enum mood {
        QUIET,
        CHILL,
        NORMAL,
        AGRESSIVE,
        PANIC
    }

    private mood currentMood = mood.NORMAL;
    public GameObject player;

    private List<GameObject> enemyTypes;
 
    private List<GameObject> entityList;
    private BasicEntityController aiController;
    [SerializeField]
    private List<GameObject> playerCardinals;

  
    private Transform[] spawnPoints;

    public int NumKilled;


    // Start is called before the first frame update

    void Start()
    {

       
        spawnPoints = GetComponentsInChildren<Transform>();
        //Random.InitState(675);
        currentMood = (mood)Random.Range(0, 4);


    }

    public void RemoveEntity(GameObject s)
    {
        NumKilled++;
        entityList.Remove(s);
       
        
        for (int i = 0; i < entityList.Count; i++)
        {
            aiController = entityList[i].GetComponent<BasicEntityController>();
            aiController.targetLocationNum(1);

        }
        
    }
    public void ReachedDestination(BasicEntityController entity) {
        entity.targetPlayer();
 
    }

    public void PlayerEnteredArea(AreaController area) {
        enemyTypes = area.getEnemyTypes();
        spawnPoints = area.getSpawnPoints();
        nextMood();
        if (enemyTypes.Count > 0) {
            for (int i = 1; i < spawnPoints.Length; i++)
            {
               // Debug.Log(enemyTypes[0].name);
                GameObject test =  Instantiate(enemyTypes[0], spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
                Debug.Log(test.name);
                try
                {
                    entityList.Add(test);
                }
                catch (System.Exception e) {
                    Debug.Log(e);
                }
                /*
                aiController = entityList[i].GetComponent<BasicEntityController>();


                aiController.setDirector(this);
                aiController.setPlayer(player);
                aiController.targetPlayer();
                for (int k = 0; k < playerCardinals.Count; k++)
                {
                    aiController.addlocation(playerCardinals[k]);
                }
                aiController.idleOff();*/
            }
        area.disableCollider();
        }
    }

    private void nextMood() {
        if (currentMood < mood.PANIC)
        {
            currentMood++;
        }
        else {
            currentMood = mood.QUIET;
        }
    }
}
