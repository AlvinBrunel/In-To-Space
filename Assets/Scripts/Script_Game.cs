using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Game : MonoBehaviour
{
    [SerializeField] GameObject Asteroid;
    [SerializeField] GameObject SpikeMine;
    [SerializeField] GameObject EnemySpaceShip;
    [SerializeField] GameObject EnemyMissile;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject GUIScore;

    GameObject Canvas;

    [SerializeField] string seed;
    [SerializeField] string[] SetSeeds;
    char[] charSeedSplit;
    public string[] seedSplit = new string[10];

    public int Score = 0;
    public int PB;

    int current = 0;
    int AsteroidDir = -1;

    public float Boundary;

    // Start is called before the first frame update
    void Start()
    {
        PB = PlayerPrefs.GetInt("PB");

        Canvas = GameObject.Find("Canvas");
        GameObject.Find("GUI PressToStart").GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        GameObject.Find("GUI DisplayScore").GetComponent<TMPro.TextMeshProUGUI>().enabled = false;

        GameObject SpawnGUIScore = Instantiate(GUIScore, transform.position, Quaternion.Euler(0, 0, 0));
        SpawnGUIScore.transform.name = "GUI Score";
        SpawnGUIScore.transform.SetParent(GameObject.Find("Canvas").transform);

        ResetSeed();
    }

    // Update is called once per frame
    void Update()
    {
        Score++;
    }

    void a() //Spawn randomly placed Asteroids
    {
        GameObject GameObject_Asteroid = Instantiate(Asteroid, new Vector2(Random.Range(-Boundary, Boundary), 20), transform.rotation);
        GameObject_Asteroid.transform.name = "Asteroid";
        Invoke("a", 0.5f);
    }

    void b() //Spawn Asteroid Line Random
    {
        if(Random.Range(0,10) <= 4)
        {
            for (int i = -(int)Boundary; i <= 0; i++)
            {
                if(Random.Range(0,20) == 4)
                {
                    GameObject GameObject_SpikeMine = Instantiate(SpikeMine, new Vector2(i, 20), transform.rotation);
                    GameObject_SpikeMine.transform.name = "Spike Mine";
                }
                else
                {
                    GameObject GameObject_Asteroid = Instantiate(Asteroid, new Vector2(i, 20), transform.rotation);
                    GameObject_Asteroid.transform.name = "Asteroid";
                }

            }
        }
        else
        {
            for (int i = (int)Boundary; i >= 0; i--)
            {
                if (Random.Range(0, 20) == 4)
                {
                    GameObject GameObject_SpikeMine = Instantiate(SpikeMine, new Vector2(i, 20), transform.rotation);
                    GameObject_SpikeMine.transform.name = "Spike Mine";
                }
                else
                {
                    GameObject GameObject_Asteroid = Instantiate(Asteroid, new Vector2(i, 20), transform.rotation);
                    GameObject_Asteroid.transform.name = "Asteroid";
                }
            }
        }
        Invoke("b", 1f);
    }
    void c() //Spawn Asteroid Line Alternate
    {
        if (AsteroidDir == -1)
        {
            for (int i = (int)Boundary; i >= 0; i--)
            {
                if (Random.Range(0, 20) == 4)
                {
                    GameObject GameObject_SpikeMine = Instantiate(SpikeMine, new Vector2(i, 20), transform.rotation);
                    GameObject_SpikeMine.transform.name = "Spike Mine";
                }
                else
                {
                    GameObject GameObject_Asteroid = Instantiate(Asteroid, new Vector2(i, 20), transform.rotation);
                    GameObject_Asteroid.transform.name = "Asteroid";
                }
            }
            AsteroidDir = 1;
        }
        else
        {
            for (int i = -(int)Boundary; i <= 0; i++)
            {
                if (Random.Range(0, 20) == 4)
                {
                    GameObject GameObject_SpikeMine = Instantiate(SpikeMine, new Vector2(i, 20), transform.rotation);
                    GameObject_SpikeMine.transform.name = "Spike Mine";
                }
                else
                {
                    GameObject GameObject_Asteroid = Instantiate(Asteroid, new Vector2(i, 20), transform.rotation);
                    GameObject_Asteroid.transform.name = "Asteroid";
                }
            }
            AsteroidDir = -1;
        }
        Invoke("c", 1f);
    }
    void d() //Spawn Enemnyship
    {
        GameObject SpawnEnemyShip = Instantiate(EnemySpaceShip, new Vector2(Random.Range(-Boundary, Boundary), 20), transform.rotation);
        SpawnEnemyShip.transform.name = "Enemy";
        SpawnEnemyShip.transform.eulerAngles = new Vector3(0, 0, 180f);
    }
    void e() //Spawn Missile
    {
        GameObject SpawnMissile = Instantiate(EnemyMissile, new Vector2(Random.Range(-Boundary, Boundary), 20), transform.rotation);
        SpawnMissile.transform.name = "Missile";
        SpawnMissile.transform.eulerAngles = new Vector3(0, 0, 180f);
    }
    void Switch()
    {
        current++;
        if (current > 9)
        {
            ResetSeed();
        }
        else
        {
            CancelInvoke(seedSplit[current - 1]);
            Invoke(seedSplit[current], 1f);
            Invoke("Switch", 3f);
        }
    }
    void ResetSeed()
    {
        current = 0;
        charSeedSplit = SetSeeds[Random.Range(0,SetSeeds.Length-1)].ToCharArray();
        for (int i = 0; i <= charSeedSplit.Length - 1; i++)
        {
            seedSplit[i] = charSeedSplit[i].ToString();
        }

        Invoke(seedSplit[current], 1f);
        Switch();
    }
    public void EndGame()
    {
        if(PB == 0 || PB < Score)
        {
            PlayerPrefs.SetInt("PB", Score);
            PB = PlayerPrefs.GetInt("PB");
        }
        GameObject.Find("GUI DisplayScore").GetComponent<TMPro.TextMeshProUGUI>().text = "Personal Best: "  + PB + "\n Score: " + Score;
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] MultipleDebris = GameObject.FindGameObjectsWithTag("Debris");
        if (Enemies.Length > 0)
        {
            foreach (GameObject Enemy in Enemies)
            {
                if (Enemy.name == "Enemy")
                {
                    Enemy.GetComponent<Script_Enemy>().DestroyObject();
                }
                if (Enemy.name == "Missile")
                {
                    Enemy.GetComponent<Script_Missile>().DestroyObject();
                }
            }
        }
        if (MultipleDebris.Length > 0)
        {
            foreach (GameObject Debris in MultipleDebris)
            {
                if (Debris.name == "Spike Mine")
                {
                    Debris.GetComponent<Script_SpikeMine>().DestroyObject();
                }
                if (Debris.name == "Asteroid")
                {
                    Debris.GetComponent<Script_Asteroid>().DestroyObject();
                }
            }
        }
        Destroy(GameObject.Find("GUI Score"));
        Destroy(this.gameObject);
    }
}
