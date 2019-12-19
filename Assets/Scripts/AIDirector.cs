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

    public static AIDirector instance =null;

    private mood currentMood = mood.NORMAL;
    [SerializeField]
    private GameObject player;

    private List<GameObject> enemyTypes;
 
    private List<GameObject> entityList = new List<GameObject>();
    private BasicEntityController aiController;
    [SerializeField]
    private List<GameObject> playerCardinals;

  
    private Transform[] spawnPoints;
    [SerializeField]
    private int NumKilled;


    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        currentMood = (mood)Random.Range(0, 4);
    }

    public void RemoveEntity(GameObject s)
    {
        NumKilled++;
        entityList.Remove(s);

        for (int i = 0; i < entityList.Count; i++)
        {
            if (entityList[i] != null) { 
                aiController = entityList[i].GetComponent<BasicEntityController>();
                aiController.targetLocationNum(1);
            }
        }
        
    }
    public void ReachedDestination(BasicEntityController entity) {
        entity.targetPlayer();
 
    }

    public void SpawnEnemiesInArea(AreaController area) {
        enemyTypes = area.getEnemyTypes();
        spawnPoints = area.getSpawnPoints();
        BasicEntityController controller;
        nextMood();
        int numEnemies =0;
        switch (currentMood) {
            case mood.QUIET:
                numEnemies = area.getMinEnemies();
                break;
            case mood.CHILL:
                numEnemies = Random.Range(area.getMinEnemies(), area.getNormalEnemies());
                break;
            case mood.NORMAL:
                numEnemies = area.getNormalEnemies();
                break;
            case mood.AGRESSIVE:
                numEnemies = Random.Range(area.getNormalEnemies(), area.getMaxEnemies());
                break;
            case mood.PANIC:
                numEnemies = area.getMaxEnemies();
                break;
        }
        Debug.Log(numEnemies);

        if (enemyTypes.Count > 0) {

            int currentSpawnPoint = 0;
            for (int i = 0; i < numEnemies; i++) {
                if (currentSpawnPoint >= spawnPoints.Length){
                    currentSpawnPoint = 0;
                }
                controller = Instantiate(enemyTypes[0], spawnPoints[currentSpawnPoint].transform.position,
                        spawnPoints[currentSpawnPoint].transform.rotation).GetComponent<BasicEntityController>();
                try
                {
                    
                    entityList.Add(controller.gameObject);
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }

                setupAndAddBasicEntityController(controller);

                currentSpawnPoint++;
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

    public void setupAndAddBasicEntityController(BasicEntityController entity) {

        entityList.Add(entity.gameObject);
        entity.setDirector(this);
        entity.setPlayer(player);
        entity.targetPlayer();
        Debug.Log("SETUP and ADD");
        for (int k = 0; k < playerCardinals.Count; k++)
        {
            entity.addlocation(playerCardinals[k]);
        }
        entity.idleOff();
    }


}
