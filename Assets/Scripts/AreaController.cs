using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    /*
     * 
     * Add a ratio/chance to spawn for a given entity - 
    [SerializeField]
    private Dictionary<GameObject, int> entityCounts = new Dictionary<GameObject, int>();
    */
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private List<GameObject> enemyTypes = new List<GameObject>();
    [SerializeField]
    private int minEnemies=0;
    [SerializeField]
    private int normalEnemies=0;
    [SerializeField]
    private int maxEnemies=0;

    public AIDirector director;
    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoints.Length==0) {
            //redundancy- might change later if design warrants it
            Debug.Log("Spawn Points empty at start");
            spawnPoints = GetComponentsInChildren<Transform>();
            
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("entered");
            director.SpawnEnemiesInArea(this);
        }
    }
    public Transform[] getSpawnPoints() {
        if (spawnPoints == null)
        {
            //redundancy- might change later if design warrants it
            Debug.Log("Spawn Points empty at getSpawnPoints");
            spawnPoints = GetComponentsInChildren<Transform>();
        }
        return spawnPoints;
    }
    public List<GameObject> getEnemyTypes() {
        return enemyTypes;
    }
    public int getMinEnemies() {
        return minEnemies;
    }

    public int getMaxEnemies() {
        return maxEnemies;
    }
    public int getNormalEnemies() {
        return normalEnemies;
    }

    public void disableCollider() {
        GetComponent<BoxCollider>().enabled = false;
    }
    public void enableCollider() {
        GetComponent<BoxCollider>().enabled = true;
    }
}
