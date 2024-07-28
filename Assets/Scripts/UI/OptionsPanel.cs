using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{

    public Button btnStart;
    public Button PrevBtn;
    public Button NextBtn;
    public GameObject[] players;
    private int showIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        btnStart.onClick.AddListener(btnStartClick);
        PrevBtn.onClick.AddListener(prevBtnClick);
        NextBtn.onClick.AddListener(nextBtnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void btnStartClick()
    {
        GameManager.instance.LoadScene_03_Fight01();
    }

    private void prevBtnClick()
    {
        players[showIdx].SetActive(false);
        showIdx--;
        if (showIdx < 0) showIdx = players.Length - 1;
        players[showIdx].SetActive(true);
        GameManager.instance.playerIdx = showIdx;
    }

    private void nextBtnClick()
    {
        players[showIdx].SetActive(false);
        showIdx++;
        if (showIdx >= players.Length) showIdx = 0;
        players[showIdx].SetActive(true);
        GameManager.instance.playerIdx = showIdx;
    }
}
