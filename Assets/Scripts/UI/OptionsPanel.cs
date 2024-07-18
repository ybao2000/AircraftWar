using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    public Button btnStart;
    // Start is called before the first frame update
    void Start()
    {
        btnStart.onClick.AddListener(btnStartClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void btnStartClick()
    {
        GameManager.instance.LoadScene_03_Fight01();
    }
}
