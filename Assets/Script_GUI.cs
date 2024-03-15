using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GUI : MonoBehaviour
{
    public GameObject GameEngine;
    TMPro.TextMeshProUGUI TMP;
    GameObject Canvas;

    RectTransform RT;
    // Start is called before the first frame update
    void Start()
    {
        TMP = GetComponent<TMPro.TextMeshProUGUI>();
        RT = GetComponent<RectTransform>();

        GameEngine = GameObject.Find("GameEngine");
        Canvas = GameObject.Find("Canvas");

        Vector2 CanvasSize = Canvas.GetComponent<RectTransform>().sizeDelta;
        RT.sizeDelta = new Vector2(CanvasSize.x, CanvasSize.y);
        RT.anchoredPosition = new Vector2(CanvasSize.x/2,CanvasSize.y/2);
        RT.position = new Vector2(640, 960);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameEngine") != null)
        {
            TMP.text = GameEngine.GetComponent<Script_Game>().Score.ToString();
        }
    }
}
