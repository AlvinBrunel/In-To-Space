using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Background : MonoBehaviour
{
    [SerializeField] float speed = 20f;

    [SerializeField] GameObject Background;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if (transform.position.y <= -30.5f)
        {
            GameObject SpawnBackground = Instantiate(Background, new Vector3(0, 30f, 0), transform.rotation);
            SpawnBackground.transform.name = "Background";
            Destroy(this.gameObject);
        }
    }

}
