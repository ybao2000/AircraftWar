using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
}
