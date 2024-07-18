using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    public float rate = 1f;
    private float rotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        rotation = RenderSettings.skybox.GetFloat("_Rotation");
    }

    // Update is called once per frame
    void Update()
    {
        rotation += rate * Time.deltaTime;
        RenderSettings.skybox.SetFloat("_Rotation", rotation);
    }
}
