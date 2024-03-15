using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Enemy : MonoBehaviour
{
    float speed = 3f;
    int Health = 20;
    float point;
    bool pause = true;
    bool projectileCD = false;
    int Worth = 100;

    Vector3 Target;
    SpriteRenderer SR;

    GameObject Player;

    [SerializeField] GameObject Projectile;
    [SerializeField] AudioSource Sound_Hit;
    [SerializeField] GameObject Particle_Explosion;
    [SerializeField] GameObject FS;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        point = Random.Range(5,11);
        InvokeRepeating("Pause",1,.1f);

        Target = new Vector3(Player.transform.position.x, point, 0);
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= point)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        MoveToPlayer();
        transform.position = Vector3.Lerp(transform.position, Target, speed/3 * Time.deltaTime);

        if (Health <= 0)
        {
            GameObject FloatingScore = Instantiate(FS, transform.position, Quaternion.Euler(0f, 0f, 0f));
            FloatingScore.GetComponent<Script_FloatingScore>().Score = Worth;
            DestroyObject();
        }
        if (((transform.position.x - Target.x < 0.5f) || (transform.position.x - Target.x > -0.5f)) && projectileCD == false)
        {
            Instantiate(Projectile, transform.position, transform.rotation);
            projectileCD = true;
            Invoke("ProjectileCoolDown", 1f);
        }
    }
    void MoveToPlayer()
    {
        if (Player != null && pause == false)
        {
            Target = new Vector3(Player.transform.position.x, point, 0);
        }
        Debug.Log(Player.transform.position);
    }
    void Pause()
    {
        pause = !pause;
        if (Mathf.RoundToInt(Random.Range(0,20)) == 1)
        {
            point = Random.Range(5, 11);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "PlayerProjectile")
        {
            Destroy(collision.gameObject);
            Hit(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Explosion")
        {
            Destroy(collision.gameObject);
            Hit(20);
        }
    }
    void ClearColour()
    {
        SR.color = Color.white;
    }
    void ProjectileCoolDown()
    {
        projectileCD = false;
    }
    void Hit(int Damage)
    {
        Sound_Hit.Play();
        Health-=Damage;
        SR.color = Color.red;
        Invoke("ClearColour", 0.06f);
    }
    public void DestroyObject()
    {
        GameObject ExplosionDebris = Instantiate(Particle_Explosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
