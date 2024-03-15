using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_FloatingScore : MonoBehaviour
{
    GameObject GameEngine;

    float speed = 4f;
    TMPro.TextMeshPro TMP;
    float Opacity = 1f;
    public int Score = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameEngine = GameObject.Find("GameEngine");
        Invoke("DestroyObject", .5f);

        TMP = GetComponent<TMPro.TextMeshPro>();
        TMP.text = Score.ToString();
        
        GameEngine.GetComponent<Script_Game>().Score += Score; 
    }

    // Update is called once per frame
    void Update()
    {
        Opacity -= Time.deltaTime * 1f;
        TMP.color = new Color(255,255,255,1f);
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
    void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
