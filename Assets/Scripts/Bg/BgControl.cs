using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgControl : MonoBehaviour
{
    public Transform[] Bgs;
    public float Speed = 2;
    public float BgSize = 60;
    private float totalSize;
    // Start is called before the first frame update
    void Start()
    {
        totalSize = BgSize * Bgs.Length;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Bgs.Length; i++)
        {
            // loop all backgrounds
            Bgs[i].Translate(new Vector3(0, 0, -1) * Speed * Time.deltaTime);
            // when the bg is too forward, then move it back
            if (Bgs[i].position.z <= -BgSize)
            {
                Bgs[i].position += new Vector3(0, 0, totalSize);
            }
        }
    }
}
