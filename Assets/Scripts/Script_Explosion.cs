using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Explosion : MonoBehaviour
{
    [SerializeField] AudioSource Sound_Explosion;

    GameObject Camera;

    public float speed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        Camera.GetComponent<ScreenShakeController>().Shake("Big");
        Sound_Explosion.Play();
        Invoke("DestroyParticle", 3f);
        Invoke("DestroyCollision", 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
    void DestroyParticle()
    {
        Destroy(this.gameObject);
    }
    void DestroyCollision()
    {
        Destroy(transform.GetChild(0).gameObject);
    }
}
