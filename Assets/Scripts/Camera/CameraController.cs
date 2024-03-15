using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] GameObject Player;

    GameObject Cam;
    Color[] SetBackgrounds = { new Color(16, 3, 12), new Color(3, 7, 16) };

    public Vector3 target = new Vector3(0f,0f,0f);

    private float startY;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
        GameObject.Find("Main Camera");

        InvokeRepeating("ChangeBackGroundColour", 1f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.x, startY, transform.position.z);

        }

    }
    void ChangeBackGroundColour()
    {
        int RandNum = Random.Range(0, SetBackgrounds.Length);
        //Cam.GetComponent<Camera>().backgroundColor = SetBackgrounds[1];
    }
    public void SpawnPlayer()
    {
        if (GameObject.Find("Player") == null)
        {
            GameObject InsPlayer = Instantiate(Player, new Vector3(0f, -20f, 0f), Quaternion.Euler(0f, 0f, 0f));
            InsPlayer.transform.name = "Player";
        }
    }
}
