using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject[] bullets;
    public GameObject[] launchPads;
    public float speed = 4;
    public float hp = 100;
    public float hurt = 10;
    public int score = 20;
    private float timer = 0;
    public float attackInterval = 5; // how frequent enemy attacks
    private new Rigidbody rigidbody;
    private RaySixDirCollision raySixDirCollision;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        raySixDirCollision = new RaySixDirCollision(~(1 << 6));
        raySixDirCollision.AddRayLayer(new Vector3(0, 0, 0), 1.5f, Color.red);
        raySixDirCollision.AddRayLayer(new Vector3(0, 0, 1.5f), 1.5f, Color.blue);
        raySixDirCollision.SetDistance(0, DIR.LEFT, 5);
        raySixDirCollision.SetDistance(0, DIR.RIGHT, 5);
        raySixDirCollision.SetDistance(1, DIR.LEFT, 5);
        raySixDirCollision.SetDistance(1, DIR.RIGHT, 5);
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
            else
            {
                raySixDirCollision.RaySixDirCollisionUpdate(this.transform);
                move();
                attack();

                if (timer > 30)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else
        { // marked as dead
            timer += Time.deltaTime;
            if (timer >= 1.5)
            { // wait for 1.5 seconds
                Destroy(this.gameObject);
            }
        }
    }

    private void move()
    {
        // transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        // kinematic
        // using dynamics
        // transform.eulerAngles = new Vector3(-rigidbody.velocity.z,
        //     transform.rotation.y, rigidbody.velocity.x * 3);
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(new Vector3(0, 0, -100)), 0.02f);
        if (this.transform.position.y > 0)
        {
            rigidbody.AddForce(Vector3.down * 6);
        }
        else if (this.transform.position.y < 0)
        {
            rigidbody.AddForce(Vector3.up * 6);
        }
        raySixDirCollision.SixRaycast((dir, hit) =>
        {
            switch (dir)
            {
                case DIR.FRONT:
                    rigidbody.AddRelativeForce(Vector3.right * 30);
                    break;
                case DIR.LEFT:
                    rigidbody.AddRelativeForce(Vector3.right * 30);
                    break;
                case DIR.RIGHT:
                    rigidbody.AddRelativeForce(Vector3.left * 30);
                    break;
                default:
                    break;
            }
        }, null);
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
            GameObject obj = Instantiate(boomEffect01);
            obj.transform.position = this.transform.position;
            Destroy(obj, 1f);
            hp -= other.gameObject.GetComponent<PlayerBullet>().hurt;
            Destroy(other.gameObject);
        }
    }

    public GameObject boomEffect01; // when it is hit
    public GameObject boomEffect02; // when it dies

    private void die()
    {
        isDead = true;
        timer = 0;
        audioSource.Play();
        // animation
        rigidbody.useGravity = true;
        rigidbody.AddForceAtPosition(Vector3.up * 5,
            transform.position + new Vector3(3, 0, 0), ForceMode.Impulse
        );
        GameObject obj = Instantiate(boomEffect02);
        obj.transform.position = this.transform.position;
        Destroy(obj, 1f);
        // you need to update the score
        // this is a hard problem: answer is using GameManager
        GameManager.instance.AddScore(score);
    }
}
