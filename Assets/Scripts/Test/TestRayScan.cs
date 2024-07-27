using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRayScan : MonoBehaviour
{
    private RaySixDirCollision raySixDirCollision;
    // Start is called before the first frame update
    void Start()
    {
        // detect all layers except 6
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
        raySixDirCollision.RaySixDirCollisionUpdate(this.transform);
    }
}
