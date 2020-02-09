using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 5;
    [SerializeField]
    private int speed = 5;
    [SerializeField]
    private int DamageAmount = 25;
    private GameObject player;
    // Start is called before the first frame update
    [SerializeField]
    private AudioClip impact;
    [SerializeField]
    private AudioClip fire1;
    AudioSource audioSource;
    [SerializeField]
    private GameObject sparks;
    [SerializeField]
    private string targetTag = "Enemy";

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(fire1, 0.7f);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed);
        if (lifetime < 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            lifetime -= Time.fixedDeltaTime;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            collision.gameObject.GetComponent<HealthBehavior>().DoDamage(DamageAmount);
        }
        //ugly line, but it instantiates sparks at the location of impact of this bullet and sets the player reference
        GameObject reference = Instantiate(sparks, this.transform.position, this.transform.rotation);
        reference.GetComponent<SparksBehaviour>().SetPlayerReference(player);


        Destroy(this.gameObject);
    }

    public void SetPlayerReference(GameObject otherPlayer)
    {
        player = otherPlayer;


    }

    public void setTargetTag(string otherTag)
    {
        targetTag = otherTag;
    }

}
