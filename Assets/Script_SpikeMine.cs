using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SpikeMine : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] GameObject Particle_Explosion;
    [SerializeField] GameObject FS;

    int Health = 1;
    int Worth = 25;

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
        if (Health <= 0 && GetComponent<BoxCollider2D>() != null)
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
            GameObject FloatingScore = Instantiate(FS, transform.position, Quaternion.Euler(0f, 0f, 0f));
            FloatingScore.GetComponent<Script_FloatingScore>().Score = Worth;
            DestroyObject();
        }
        if (collision.transform.tag == "Player")
        {
            GameObject FloatingScore = Instantiate(FS, transform.position, Quaternion.Euler(0f, 0f, 0f));
            FloatingScore.GetComponent<Script_FloatingScore>().Score = Worth;
            collision.transform.GetComponent<Script_Player>().Hit(1);
            Destroy(this.gameObject);
            DestroyObject();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Explosion")
        {
            GameObject FloatingScore = Instantiate(FS, transform.position, Quaternion.Euler(0f, 0f, 0f));
            FloatingScore.GetComponent<Script_FloatingScore>().Score = Worth;

            DestroyObject();
        }
    }
    void ClearColour()
    {
        SR.color = Color.white;
    }
    public void DestroyObject()
    {
        GameObject AsteroidDebris = Instantiate(Particle_Explosion, transform.position, transform.rotation);
        //FloatingScore.GetComponent<Script_FxloatingScore>().Score = Worth;
        Destroy(this.gameObject);
    }
}
