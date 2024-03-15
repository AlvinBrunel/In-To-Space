using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PressToStart : MonoBehaviour
{
    GameObject Canvas;
    RectTransform RT;
    TMPro.TextMeshProUGUI TMP;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        RT = GetComponent<RectTransform>();

        Vector2 CanvasSize = Canvas.GetComponent<RectTransform>().sizeDelta;
        RT.sizeDelta = new Vector2(CanvasSize.x, CanvasSize.y);

        //RT.anchoredPosition = new Vector2(RT.sizeDelta.x/4, -RT.sizeDelta.y/4);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
