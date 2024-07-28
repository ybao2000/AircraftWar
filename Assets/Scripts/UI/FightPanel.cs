using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightPanel : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI TxtScore;
    public TextMeshProUGUI TxtHP; // this depends on the player
    public Button backBtn;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.SetFightPanel(this);
        gameOverPanel.SetActive(false);
        backBtn.onClick.AddListener(backBtnClick);
        TxtScore.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void backBtnClick()
    {
        Time.timeScale = 1;  // keep Rime normal
        GameManager.instance.LoadScene_02_Options();
    }

    // the big problem: how to update HP and Score
    // the solution is to provide a function which can be called
    public void AddScore(int val)
    {
        score += val;
        TxtScore.text = score.ToString();
    }

    public void SetHP(int hp)
    {
        TxtHP.text = hp.ToString();
    }

    public void SetGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
