using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairController : MonoBehaviour
{
    [SerializeField]
    private Texture2D crosshair;
    private Rect position;
    [SerializeField]
    private float scale = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position = new Rect(Input.mousePosition.x-(crosshair.width*scale/2.0f), -1.0f*Input.mousePosition.y-(crosshair.height*scale / 2.0f)+Screen.height, crosshair.width*scale, crosshair.height*scale);
    }

    private void OnGUI()
    {
        GUI.DrawTexture(position, crosshair);
    }
}
