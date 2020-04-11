using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SignUp_v2: MonoBehaviour
{
    
    public GameObject UI_SignUp;

    [Header("LoginPanel")]
    public UIInput id_Input;
    public UIInput pwd_Input;
    public UIInput pwd_confirm_Input;

    [Header("PopUpWindow")]
    public GameObject PopUpWindow;
    public UILabel PopUpTitle;

    [Header("Url")]
    public string CreateUrl;

    // Start is called before the first frame update
    void Start()
    {
        //PopUpWindow.SetActive(false);
        CreateUrl = "http://jajugoguma.synology.me/CreateAccount.php";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CancleButtonClicked()
    {
        SceneManager.LoadScene("Title_v2");
    }

    public void CreateButtonVlClicked()
    {
        StartCoroutine(CreateAccount());
    }

    IEnumerator CreateAccount()
    {
        if (String.IsNullOrEmpty(id_Input.text) || (String.IsNullOrEmpty(pwd_Input.text)))
        {
            PopUpWindow.SetActive(true);
            PopUpTitle.text = "Complete all input boxs";
            yield break;
        }
        if (!pwd_Input.text.Equals(pwd_confirm_Input.text)) 
        {
            PopUpWindow.SetActive(true);
            PopUpTitle.text = "Password is Not matched";
            yield break;
        }
        

        WWWForm form = new WWWForm();
        form.AddField("param_id", id_Input.text);
        form.AddField("param_pwd", pwd_Input.text);

        WWW webRequest = new WWW(CreateUrl, form);
        yield return webRequest;

        
        if (webRequest.text.Contains("added"))
        {
            
            PopUpWindow.SetActive(true);
            PopUpTitle.text = "Sign Up Complete";
        }
        else
        {
            PopUpWindow.SetActive(true);

            PopUpTitle.text = "Sign Up Failure";
        }
    }

    public void PopUpOKButtonClicked()
    {
        PopUpWindow.SetActive(false);

        if (PopUpTitle.text.Equals("Sign Up Complete"))
        {
            SceneManager.LoadScene("Title_v2");
        }
    }

    public void onSubmit()
    {
        CreateButtonVlClicked();
    }
}
