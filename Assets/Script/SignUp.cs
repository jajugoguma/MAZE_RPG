using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SignUp : MonoBehaviour
{
    [Header("LoginPanel")]
    public InputField IDInputField;
    public InputField PWInputField;
    public InputField PWConfirmField;

    [Header("PopUpPanel")]
    public GameObject PopUpPanel;
    public Text PopUpTitle;

    [Header("Url")]
    public string CreateUrl;

    // Start is called before the first frame update
    void Start()
    {
        PopUpPanel.SetActive(false);
        CreateUrl = "http://jajugoguma.synology.me/CreateAccount.php";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CancleButtonClicked()
    {
        SceneManager.LoadScene("Title");
    }

    public void CreateButtonVlClicked()
    {
        StartCoroutine(CreateAccount());
    }

    IEnumerator CreateAccount()
    {
        if (String.IsNullOrEmpty(IDInputField.text) || (String.IsNullOrEmpty(PWInputField.text)))
        {
            PopUpPanel.SetActive(true);
            PopUpTitle.text = "Complete all input boxs";
            yield break;
        }
        if (!PWInputField.text.Equals(PWConfirmField.text))
        {
            PopUpPanel.SetActive(true);
            PopUpTitle.text = "Password is Not matched";
            yield break;
        }
        

        WWWForm form = new WWWForm();
        form.AddField("param_id", IDInputField.text);
        form.AddField("param_pwd", PWInputField.text);

        WWW webRequest = new WWW(CreateUrl, form);
        yield return webRequest;

        if (webRequest.text.Contains("added"))
        {
            PopUpPanel.SetActive(true);
            PopUpTitle.text = "Sign Up Complete";
        }
        else
        {
            PopUpPanel.SetActive(true);
            PopUpTitle.text = "Sign Up Failure";
        }
    }

    public void PopUpOKButtonClicked()
    {
        PopUpPanel.SetActive(false);
        if (PopUpTitle.text.Equals("Sign Up Complete"))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
