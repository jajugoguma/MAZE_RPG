using System;
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

    [Header("Object")]
    public GameObject PopUpWindow;
    public UILabel PopUpTitle;

    // Start is called before the first frame update
    void Start()
    {
        CreateUrl = "http://jajugoguma.synology.me/LogIn.php";
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
        StartCoroutine(LogIn());
    }

    IEnumerator LogIn()
    {
        if (String.IsNullOrEmpty(id_Input.text) || (String.IsNullOrEmpty(pwd_Input.text)))
        {
            PopUpWindow.SetActive(true);
            PopUpTitle.text = "Complete all input boxs";
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("param_id", id_Input.text);
        form.AddField("param_pwd", pwd_Input.text);

        WWW webRequest = new WWW(CreateUrl, form);
        yield return webRequest;

        if (webRequest.text.Contains("complete"))
        {
            // todo : change start game
            PopUpWindow.SetActive(true);
            PopUpTitle.text = webRequest.text;
        }
        else
        {
            PopUpWindow.SetActive(true);
            PopUpTitle.text = webRequest.text;
        }

    }

    public void PopUpOKButtonClicked()
    {
        PopUpWindow.SetActive(false);
    }
}
