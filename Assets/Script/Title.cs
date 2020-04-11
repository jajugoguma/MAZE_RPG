using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;



public class Title : MonoBehaviour
{

    [Header("Url")]
    public string CreateUrl;

    [Header("InputData")]
    public UIInput id_Input;
    public UIInput pwd_Input;

    // Start is called before the first frame update
    void Start()
    {
        
        // DonDestroyOnLoad(gameobject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Onsubmit()
    {
        LoginButtonClicked();
    }
    

    public void SignUpButtonClicked()
    {
        SceneManager.LoadScene("SignUp_v2");
    }

    public void LoginButtonClicked()
    {
        
    }
}
