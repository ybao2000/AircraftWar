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
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (hp <= 0)
            {
                die();
            }
            move();
            attack();
        }
    }

    private void move()
    {
        float h = Input.GetAxis("Horizontal");  // x direction
        float v = Input.GetAxis("Vertical");    // z direction
        // Debug.Log($"h: {h}, v: {v}"); // h, v are a float between [-1, 1];
        // check if h or v is not zero
        // we should restrict the move of the player
        if (transform.position.x <= -50 && h < 0) h = 0;
        else if (transform.position.x >= 50 && h > 0) h = 0;
        if (transform.position.z >= 30 && v > 0) v = 0;
        else if (transform.position.z <= 0 && v < 0) v = 0;
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

    protected virtual void shootBullet()
    {
        Debug.Log("shoot bullet");
        // this is just one bullet
        GameObject clone = Instantiate(bullet);
        clone.transform.position = launchPad.transform.position;
    }



    public AudioSource audio_hit;
    public AudioSource audio_dead;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            hp -= other.gameObject.GetComponent<EnemyBullet>().hurt;
            Destroy(other.gameObject);
            audio_hit.Play();
            Debug.Log($"player hp: {hp} ");
            GameManager.instance.SetHP((int)hp);
        }
        // else if (other.gameObject.tag == "EnemyBullet2")
        // {
        //     audio_hit.Play();
        //     hp -= other.gameObject.GetComponent<EnemyBullet2>().hurt;
        //     Destroy(other.gameObject);
        // }
        else if (other.gameObject.tag == "Enemy")
        {
            hp -= other.gameObject.GetComponent<Enemy>().hurt;
            Destroy(other.gameObject);
            audio_hit.Play();
            Debug.Log($"player hp: {hp} ");
            GameManager.instance.SetHP((int)hp);
        }
    }

    private void die()
    {
        isDead = true;
        Time.timeScale = 0; //0 will pause the game        
        audio_dead.Play();
        // 1. we should pause the game, no more enemy and bullet
        // 2. need a pause page, then click it back to the option page
        GameManager.instance.SetGameOver();
    }
}
