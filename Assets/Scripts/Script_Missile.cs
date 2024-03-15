using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Missile : MonoBehaviour
{
    GameObject Player;

    [SerializeField] GameObject Particle_Explosion;
    [SerializeField] GameObject FS;

    Vector3 Target;

    SpriteRenderer SR;

    float speed = 6f;
    int Health = 2;
    int Worth = 50;

    Quaternion RotationPoint;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Target = new Vector3(Player.transform.position.x, Player.transform.position.y, 0);
        //transform.position = Vector3.Lerp(transform.position, Target, speed / 3 * Time.deltaTime);
        transform.position += transform.up * speed * Time.deltaTime;
        Target.z = 0f;

        Vector3 objectPos = transform.position;
        Target.x = Target.x - objectPos.x;
        Target.y = Target.y - objectPos.y;

        float angle = Mathf.Atan2(Target.y, Target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));

        if (transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }
        if (Health <= 0 && GetComponent<BoxCollider2D>() != null)
        {
            GameObject FloatingScore = Instantiate(FS, transform.position, Quaternion.Euler(0f, 0f, 0f));
            FloatingScore.GetComponent<Script_FloatingScore>().Score = Worth;
            DestroyObject();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "PlayerProjectile")
        {
            Destroy(collision.gameObject);
            Hit(1);
        }
        if (collision.transform.tag == "Player" || collision.transform.tag == "Debris" || collision.transform.tag == "Enemy")
        {
            GameObject FloatingScore = Instantiate(FS, transform.position, Quaternion.Euler(0f, 0f, 0f));
            FloatingScore.GetComponent<Script_FloatingScore>().Score = Worth;
            //collision.transform.GetComponent<Script_Player>().Hit();
            DestroyObject();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Explosion")
        {
            Destroy(collision.gameObject);
            Health -= 20;
            SR.color = Color.red;

            Invoke("ClearColour", 0.06f);
        }
    }
    void ClearColour()
    {
        SR.color = Color.white;
    }
    public void DestroyObject()
    {
        GameObject AsteroidDebris = Instantiate(Particle_Explosion, transform.position, transform.rotation);
        //FloatingScore.GetComponent<Script_FloatingScore>().Score = Worth;
        Destroy(this.gameObject);
    }
    void Hit(int Damage)
    {
        Health -= Damage;
        SR.color = Color.red;
        Invoke("ClearColour", 0.06f);
    }
}
