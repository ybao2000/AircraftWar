using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset; // this is the intial distance between player and camera
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.GetCurPlayer();
        offset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
        // Lerp means move to the pos within a certain time
    }
}
