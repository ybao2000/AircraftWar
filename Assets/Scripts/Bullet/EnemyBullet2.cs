using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet2 : EnemyBullet
{
    protected override void shoot()
    {
        timer += Time.deltaTime;
        if (timer > 15)
        {
            // after 5 seconds
            Destroy(this.gameObject);
        }
        transform.rotation = new Quaternion(0, transform.eulerAngles.y, 0, 0.2f);
        transform.Translate(new Vector3(0, 0, -1) * speed * Time.deltaTime);
    }
}
