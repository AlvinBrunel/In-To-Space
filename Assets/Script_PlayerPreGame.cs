using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PlayerPreGame : MonoBehaviour
{
    Vector3 FinalPosition;
    float speed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        FinalPosition = new Vector3(0f, -9f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        if (transform.position.y >= -9f)
        {
            GameObject.Find("GUI PressToStart").GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            GameObject.Find("GUI DisplayScore").GetComponent<TMPro.TextMeshProUGUI>().enabled = true;

            GetComponent<Script_Player>().enabled = true;
            Destroy(GetComponent<Script_PlayerPreGame>());
        }
    }
}
