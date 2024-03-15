using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Asteroid : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] GameObject Particle_Explosion;
    int Health = 2;
    SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }
        if(Health <= 0 && GetComponent<BoxCollider2D>() != null)
        {
            DestroyObject();
        }
        transform.Rotate(new Vector3(0, 0, 0.1f));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "PlayerProjectile")
        {
            Destroy(collision.gameObject);
            Hit(1);
        }
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Script_Player>().Hit(1);
            DestroyObject();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Explosion")
        {
            Hit(20);
        }
    }
    void ClearColour()
    {
        SR.color = Color.white;
    }
    void Hit(int Damage)
    {
        Health -= Damage;
        SR.color = Color.red;
        Invoke("ClearColour", 0.06f);
    }
    public void DestroyObject()
    {
        GameObject AsteroidDebris = Instantiate(Particle_Explosion, transform.position, transform.rotation);
        //FloatingScore.GetComponent<Script_FloatingScore>().Score = Worth;
        Destroy(this.gameObject);
    }
}
