using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10;
    public float hurt = 10;
    protected float timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    protected virtual void shoot()
    {
        timer += Time.deltaTime;
        if (timer > 15)
        {
            // after 5 seconds
            Destroy(this.gameObject);
        }
        transform.rotation = new Quaternion(0, 0, 0, -1);
        transform.Translate(new Vector3(0, 0, -1) * speed * Time.deltaTime);
    }
}
