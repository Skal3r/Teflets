using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBehavior
    : MonoBehaviour
{

    //simple spawnpoints for the Director to use to spawn things
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position, 0.5f);
    }
}