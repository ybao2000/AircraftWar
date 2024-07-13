using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLoadScene : MonoBehaviour
{
    // idea: when user click Login button,
    // it loads _02_Options
    // Start is called before the first frame update
    // first thing: let's learn how to load scene

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene_02_Options() {
        SceneManager.LoadScene(1); // inside LoadScene, it could be the scene name or idx
    }
}
