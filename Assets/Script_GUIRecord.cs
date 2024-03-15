using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GUIRecord : MonoBehaviour
{
    GameObject Canvas;
    RectTransform RT;
    TMPro.TextMeshProUGUI TMP;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        RT = GetComponent<RectTransform>();
        TMP = GetComponent<TMPro.TextMeshProUGUI>();
        Vector2 CanvasSize = Canvas.GetComponent<RectTransform>().sizeDelta;
        RT.sizeDelta = new Vector2(CanvasSize.x, CanvasSize.y);

        //RT.anchoredPosition = new Vector2(64, -64);

        TMP.text = "Personal Best: " + PlayerPrefs.GetInt("PB");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
