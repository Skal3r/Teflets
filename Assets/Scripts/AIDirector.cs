using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDirector : MonoBehaviour
{


    public GameObject player;
    public int numEnemies = 5;
    [SerializeField]
    private List<GameObject> enemyTypes;
    [SerializeField]
    private List<GameObject> entityList;
    private BasicEntityController aiController;

    public List<GameObject> playerCardinals;

    public int NumKilled;


    // Start is called before the first frame update
    private Transform[] spawnPoints;

    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();

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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RemoveEntity(GameObject s)
    {
        NumKilled++;
        entityList.Remove(s);
        Debug.Log("Gone");
        
        for (int i = 0; i < entityList.Count; i++)
        {
            aiController = entityList[i].GetComponent<BasicEntityController>();
            aiController.targetLocationNum(1);

        }
        
    }
    public void ReachedDestination(BasicEntityController entity) {
        entity.targetPlayer();
        Debug.Log("wop");
    }
}
