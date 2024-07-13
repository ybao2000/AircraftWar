using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // design pattern: singleton
    public static GameManager instance;
    // Start is called before the first frame update
    // problem is when to initialize the instance?

    // too late!!! You have to initialize instance before Start!
    // how???
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region load scenes
    public void LoadScene_02_Options() {
        SceneManager.LoadScene(1); // inside LoadScene, it could be the scene name or idx
    }

    public void LoadScene_03_Fight01() {
        SceneManager.LoadScene(2); // inside LoadScene, it could be the scene name or idx
    }
    #endregion
}
