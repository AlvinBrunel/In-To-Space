using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : MonoBehaviour
{
    [SerializeField] float Speed;

    [SerializeField] GameObject Projectile;
    [SerializeField] GameObject GameEngine;
    [SerializeField] GameObject GUIScore;
    GameObject Camera;

    [SerializeField] AudioSource Sound_GunShot;
    [SerializeField] AudioSource Sound_Hit;

    [SerializeField] GameObject Particle_Explosion;

    SpriteRenderer SR;

    int movex;
    int movey;
    int Health = 1;

    bool projectileCD = false;

    float targetAngle = 15;
    float turnSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        Camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("GameEngine") == null && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W)))
        {
            GameObject SpawnGameEngine = Instantiate(GameEngine,transform.position, Quaternion.Euler(0, 0, 0));
            SpawnGameEngine.transform.name = "GameEngine";

        }
        if(Input.GetKeyDown(KeyCode.Space) && projectileCD == false)
        {
            Instantiate(Projectile, transform.position, transform.rotation);
            Sound_GunShot.Play();
            Camera.GetComponent<ScreenShakeController>().Shake("VerySmall");
            projectileCD = true;
            Invoke("ProjectileCoolDown",0.2f);
        }

       movex = -(Convert.ToInt32(Input.GetKey(KeyCode.A))) + Convert.ToInt32(Input.GetKey(KeyCode.D));
       movey = -(Convert.ToInt32(Input.GetKey(KeyCode.S))) + Convert.ToInt32(Input.GetKey(KeyCode.W));
       transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -(movex*targetAngle)), turnSpeed * Time.deltaTime);
       transform.position += new Vector3(movex,movey,0)*Speed*Time.deltaTime;
       KeepInBounds();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Explosion")
        {
            Hit(20);
        }
    }

    void ProjectileCoolDown()
    {
        projectileCD = false;
    }
    public void Hit(int Damage)
    {
        Health-=Damage;
        SR.color = Color.red;
        Sound_Hit.Play();
        Camera.GetComponent<ScreenShakeController>().Shake("Big");
        Invoke("ClearColour", 0.3f);

        if(Health<=0 && GameObject.Find("GameEngine") != null)
        {
            GameObject.Find("GameEngine").GetComponent<Script_Game>().EndGame();
            GameObject.Find("Main Camera").GetComponent<CameraController>().Invoke("SpawnPlayer", 1f);
            DestroyObject();
        }
    }
    void ClearColour()
    {
        SR.color = Color.white;
    }
    void KeepInBounds()
    {
        if (transform.position.x < -8f)
        {
            transform.position = new Vector2(-8f, transform.position.y);
        }
        if (transform.position.x > 8f)
        {
            transform.position = new Vector2(8f, transform.position.y);
        }
        if (transform.position.y < -15f)
        {
            transform.position = new Vector2(transform.position.x, -15f);
        }
        if (transform.position.y > 15f)
        {
            transform.position = new Vector2(transform.position.x, 15f);
        }
    }
    public void DestroyObject()
    {
        GameObject ExplosionDebris = Instantiate(Particle_Explosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}