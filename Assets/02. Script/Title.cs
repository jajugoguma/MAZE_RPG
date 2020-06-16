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
        if (id_Input.isSelected && Input.GetKeyDown(KeyCode.Tab))
        {
            id_Input.isSelected = false;
            pwd_Input.isSelected = true;
           

        }
        if (pwd_Input.isSelected && Input.GetKeyDown(KeyCode.Tab))
            id_Input.isSelected = true;

       
    }

    public void Onsubmit()
    {
        LoginButtonClicked();
    }
    

    public void SignUpButtonClicked()
    {
        SceneManager.LoadScene("SignUp_v3");
    }

    public void LoginButtonClicked()
    {
        StartCoroutine(LogIn());
    }

    IEnumerator LogIn()
    {
        if (String.IsNullOrEmpty(id_Input.value) || (String.IsNullOrEmpty(pwd_Input.value)))
        {
            PopUpWindow.SetActive(true);
            PopUpTitle.text = "Complete all input boxs";
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("param_id", id_Input.value);
        form.AddField("param_pwd", pwd_Input.value);

        WWW webRequest = new WWW(CreateUrl,form);
        yield return webRequest;

        
        if (webRequest.text.Contains("complete"))
        {
            // todo : change start game
            //PopUpWindow.SetActive(true);
            //PopUpTitle.text = webRequest.text;
            SceneManager.LoadScene("SelectCharacter");
            InfoManager.Instance.id = id_Input.value;
            InfoManager.Instance.pwd = pwd_Input.value;
        }
        else
        {
            PopUpWindow.SetActive(true);
            PopUpTitle.text = webRequest.text;
            Debug.Log(webRequest.text);
            Debug.Log(webRequest.error);
        }

    }

    public void PopUpOKButtonClicked()
    {
        PopUpWindow.SetActive(false);
    }
}
