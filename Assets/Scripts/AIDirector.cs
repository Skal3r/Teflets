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
 
    private List<GameObject> entityList = new List<GameObject>();
    private BasicEntityController aiController;
    [SerializeField]
    private List<GameObject> playerCardinals;

  
    private Transform[] spawnPoints;

    public int NumKilled;


    // Start is called before the first frame update

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
            aiController = entityList[i].GetComponent<BasicEntityController>();
            aiController.targetLocationNum(1);

        }
        
    }
    public void ReachedDestination(BasicEntityController entity) {
        entity.targetPlayer();
 
    }

    public void SpawnEnemiesInArea(AreaController area) {
        enemyTypes = area.getEnemyTypes();
        spawnPoints = area.getSpawnPoints();
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
                try
                {
                    entityList.Add(Instantiate(enemyTypes[0], spawnPoints[currentSpawnPoint].transform.position, 
                        spawnPoints[currentSpawnPoint].transform.rotation));
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }

                setupAndAddBasicEntityController(entityList[i].GetComponent<BasicEntityController>());

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
        for (int k = 0; k < playerCardinals.Count; k++)
        {
            entity.addlocation(playerCardinals[k]);
        }
        entity.idleOff();
    }
}
