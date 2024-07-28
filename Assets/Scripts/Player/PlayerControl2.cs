using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl2 : PlayerControl
{
    // range attack
    protected override void shootBullet()
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
}
