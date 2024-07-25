using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject[] bullets;
    public GameObject[] launchPads;
    public float speed = 4;
    public float hp = 100;
    private float timer = 0;
    public float attackInterval = 5; // how frequent enemy attacks
    private new Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        attack();

    }

    private void move()
    {
        // transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        // kinematic
        // using dynamics
        transform.eulerAngles = new Vector3(-rigidbody.velocity.z,
            transform.rotation.y, rigidbody.velocity.x * 3);
        if (this.transform.position.y > 0)
        {
            rigidbody.AddForce(Vector3.down * 3);
        }
        else if (this.transform.position.y < 0)
        {
            rigidbody.AddForce(Vector3.up * 3);
        }
        rigidbody.AddForce(Vector3.back * speed);
    }


    private void attack()
    {
        timer += Time.deltaTime;
        if (timer >= attackInterval)
        {
            bool shoot = Random.Range(0, 100) % 2 == 0;
            if (shoot)
            {
                shootBullet();
            }
            timer = 0;
        }
    }

    private void shootBullet()
    {
        if (bullets.Length > 0)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                var bullet = bullets[i];
                GameObject clone = Instantiate(bullet);
                clone.transform.position = launchPads[i].transform.position;
                clone.transform.rotation = launchPads[i].transform.rotation;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // this is to detect the collision
        // Debug.Log($"collide with {other.gameObject.name}");
        // we only want the enemy collide with the PlayerBullet
        if (other.gameObject.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            hp -= other.gameObject.GetComponent<PlayerBullet>().hurt;
            Debug.Log(hp);
            if (hp < 0)
            {
                die();
            }
        }
    }

    private void die()
    {
        Destroy(this.gameObject);
    }
}
