using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_EnemyBullet : MonoBehaviour
{
    float speed = 20;

    [SerializeField] AudioSource Sound_Projectile;
    // Start is called before the first frame update
    void Start()
    {
        Sound_Projectile.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down*speed*Time.deltaTime;

        if (transform.position.y <= -30)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.name == "Player")
        {
            collision.gameObject.GetComponent<Script_Player>().Hit(1);
            Destroy(this.gameObject);
        }
    }
}
