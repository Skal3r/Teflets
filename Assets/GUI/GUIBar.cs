using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIBar : MonoBehaviour
{
    float barDisplay = 0;
    Vector2 pos = new Vector2(20, 40);
    Vector2 size = new Vector2(60,20);
    public Texture2D progressBarEmpty;
    public Texture2D progressBarFull;
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        barDisplay += Time.deltaTime*0.2f;
        if (barDisplay > 1) {
            barDisplay = 0;
        }
        
    }

    private void OnGUI()
    {
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), progressBarFull);
        // draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), progressBarFull);
        GUI.EndGroup();
        GUI.EndGroup();
    }
}
