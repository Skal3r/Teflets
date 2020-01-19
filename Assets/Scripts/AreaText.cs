using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaText : MonoBehaviour
{   [SerializeField]
    private Text displayText;
    [SerializeField]
    string displayString = "";
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        displayText.text = displayString;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
            displayText.transform.position = namePos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
            displayText.text = displayString;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = false;
            displayText.text = "";
        }
    }

}
