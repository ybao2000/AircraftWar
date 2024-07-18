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
    void Awake()
    {
        // instance = new GameManager();
        if (instance == null)
        { // define instance only when it's not defined
            instance = this;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        // a trigger: after a scened is loaded, function sceneLoadCompleted will be called
        SceneManager.sceneLoaded += sceneLoadCompleted;  // event and delegate
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region load scenes
    public void LoadScene_02_Options()
    {
        SceneManager.LoadScene(1); // inside LoadScene, it could be the scene name or idx
    }

    public void LoadScene_03_Fight01()
    {
        SceneManager.LoadScene(2); // inside LoadScene, it could be the scene name or idx
    }

    private void sceneLoadCompleted(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"SceneLoadCompleted for scene {scene.name}");
    }
    #endregion
}
