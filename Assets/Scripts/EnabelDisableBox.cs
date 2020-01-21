using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabelDisableBox : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> EnableOnEnter;
    [SerializeField]
    private List<GameObject> DisableOnEnter;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<BoxCollider>().isTrigger = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject g in EnableOnEnter) {
            g.SetActive(true);
        }
        foreach (GameObject g in DisableOnEnter) {
            g.SetActive(false);
        }
    }
}
