using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    private float omega;
    // Start is called before the first frame update
    void Start()
    {
        omega = Random.Range(-15.0f, 15.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * omega * Time.deltaTime);
    }
}
