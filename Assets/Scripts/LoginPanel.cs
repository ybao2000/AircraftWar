using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public Button btnLogin;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        // delegate, event
        btnLogin.onClick.AddListener(btnLoginClick); // a method (function)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnLoginClick(){
        // inside this function, you are going to use GameManager to switch scene
        gameManager.LoadScene_02_Options();
    }
}
