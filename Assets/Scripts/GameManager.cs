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
    private FightPanel fightPanel;
    public GameObject[] Players;
    public int playerIdx = 0;
    private GameObject curPlayer;
    public AudioSource audioSource;
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
        audioSource.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Play music"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        if (GUI.Button(new Rect(10, 70, 100, 50), "Stop music"))
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
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
        // Debug.Log($"SceneLoadCompleted for scene {scene.name}");
        if (scene.buildIndex == 2)
        {
            // if fight scene
            instantiatePlayer();
        }
    }

    private void instantiatePlayer()
    {
        // clone from the prefab
        curPlayer = Instantiate(Players[playerIdx], new Vector3(0, 0, 0), Quaternion.identity);
    }

    public GameObject GetCurPlayer()
    {
        return curPlayer;
    }
    #endregion
    #region for fight panel
    public void SetFightPanel(FightPanel fightPanel)
    {
        this.fightPanel = fightPanel;
    }
    public void AddScore(int val)
    {
        this.fightPanel.AddScore(val);
    }
    public void SetHP(int hp)
    {
        if (this.fightPanel != null)
        {
            this.fightPanel.SetHP(hp);
        }
    }

    public void SetGameOver()
    {
        this.fightPanel.SetGameOver();
    }
    #endregion
}
