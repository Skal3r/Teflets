using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private int camHeight = 75;
    // Start is called before the first frame update
    void Start()
    {

    }


    //follows player at camheight
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, camHeight, Player.transform.position.z);
    }
}
