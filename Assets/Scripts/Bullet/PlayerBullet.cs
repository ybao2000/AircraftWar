using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 50;
    public float hurt = 10;
    private float timer = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            // after 5 seconds
            Destroy(this.gameObject);
        }
        transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
    }
}
