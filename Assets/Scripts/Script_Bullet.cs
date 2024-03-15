using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Bullet : MonoBehaviour
{
    float speed = 20;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up*speed*Time.deltaTime;

        if (transform.position.y >= 20)
        {
            Destroy(this.gameObject);
        }
    }
}
