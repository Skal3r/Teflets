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
    public int numEnemies = 5;
    [SerializeField]
    private List<GameObject> enemyTypes;
    [SerializeField]
    private List<GameObject> entityList;
    private BasicEntityController aiController;

    public List<GameObject> playerCardinals;

    AudioSource audioSource;

    private Transform[] spawnPoints;

    public int NumKilled;


    // Start is called before the first frame update

    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        spawnPoints = GetComponentsInChildren<Transform>();


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
        Debug.Log(spawnPoints.Length);
        spawnPoints = area.getSpawnPoints();
        Debug.Log(spawnPoints.Length);
        for (int i = 0; i < spawnPoints.Length; i++)
        {

            entityList.Add(Instantiate(enemyTypes[0], spawnPoints[i].transform));

            aiController = entityList[i].GetComponent<BasicEntityController>();


            aiController.setDirector(this);
            aiController.setPlayer(player);
            aiController.targetPlayer();
            for (int k = 0; k < playerCardinals.Count; k++)
            {
                aiController.addlocation(playerCardinals[k]);
            }
            aiController.idleOff();
        }
        area.disableCollider();
        
    }
}
