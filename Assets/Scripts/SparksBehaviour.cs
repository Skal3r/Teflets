using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksBehaviour : MonoBehaviour
{

    [SerializeField]
    private AudioClip impact;
   // public GameObject playerLocation;
    AudioSource audioSource;
    // Start is called before the first frame update
    private GameObject playerReference;
  
    public SparksBehaviour(int i) {

    }
    //sets volume of audio to be relative to where player is 
    void Start()
    {
        float vol = 0.0f;
        if (playerReference!= null) {
            vol =
                10.0f / Vector3.Distance(playerReference.transform.position, this.transform.position);
        }
        Debug.Log(vol);
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(impact, vol);
   
    }

    // destroys this gameobject when done playing
    void Update()
    {
        if (!audioSource.isPlaying) {
            Destroy(this.gameObject);
        }
    }
    //sets player reference - should only be called once during creation
    public void SetPlayerReference(GameObject other) {
        playerReference = other;
     
    }


}
