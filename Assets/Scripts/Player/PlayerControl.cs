using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject bullet;
    public GameObject launchPad;
    public float hp = 100;
    public float speed = 10;
    private float timer = 0.5f; // it should be loaded initially
    public float LoadTime = 0.5f;
    private float totTimer = 0; // this is the count all (both pressed or not-pressed)
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            die();
        }
        move();
        attack();
    }

    private void move()
    {
        float h = Input.GetAxis("Horizontal");  // x direction
        float v = Input.GetAxis("Vertical");    // z direction
        // Debug.Log($"h: {h}, v: {v}"); // h, v are a float between [-1, 1];
        // check if h or v is not zero
        if (h != 0 || v != 0)
        {
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime, Space.World); // global coordinator
            transform.eulerAngles = new Vector3(v * 15, 0, h * -30);
        }
    }

    private void attack()
    {
        totTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space)) // this one is to avoid when holding, shoot continuously
        {
            timer += Time.deltaTime;
            if (timer >= LoadTime)
            {
                shootBullet();
                timer = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (totTimer >= LoadTime)
            {
                timer = LoadTime;
                totTimer = 0;
            }
        }
        // if (Input.GetKey(KeyCode.R))
        // {
        //     timer += Time.deltaTime;
        //     if (timer >= LoadTime)
        //     {
        //         shootBullets();
        //         timer = 0;
        //     }
        // }
    }

    private void shootBullet()
    {
        Debug.Log("shoot bullet");
        // this is just one bullet
        GameObject clone = Instantiate(bullet);
        clone.transform.position = launchPad.transform.position;
    }

    private void shootBullets()
    {
        float interval = 10;
        // this is going to shoot 5 bullets
        for (int i = 0; i < 5; i++)
        {
            GameObject clone = Instantiate(bullet);
            clone.transform.position = launchPad.transform.position;
            clone.transform.eulerAngles = new Vector3(0, (i - 2) * interval, 0);
        }
    }

    public AudioSource audio_hit;
    public AudioSource audio_dead;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            audio_hit.Play();
            hp -= other.gameObject.GetComponent<EnemyBullet>().hurt;
            Debug.Log($"player hp: {hp} ");
            Destroy(other.gameObject);
        }
        // else if (other.gameObject.tag == "EnemyBullet2")
        // {
        //     audio_hit.Play();
        //     hp -= other.gameObject.GetComponent<EnemyBullet2>().hurt;
        //     Destroy(other.gameObject);
        // }
        else if (other.gameObject.tag == "Enemy")
        {
            audio_hit.Play();
            hp -= other.gameObject.GetComponent<Enemy>().hurt;
            Debug.Log($"player hp: {hp} ");
            Destroy(other.gameObject);
        }
    }

    private void die()
    {
        audio_dead.Play();
        // todo - go back to the option page
    }
}
