using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    
    public GameObject Character1;
    public GameObject Character2;
    public GameObject Character3;

    //Character True or False Controll
    public GameObject True1;
    public GameObject True2;
    public GameObject True3;

    public GameObject False1;
    public GameObject False2;
    public GameObject False3;

    //Character name 
    public UILabel Character1name;
    public UILabel Character2name;
    public UILabel Character3name;

    //ID
    public UILabel ID;



    // Start is called before the first frame update
    // ID 정보값에 맞춰서 DB에서 케릭터들을 가져와서 스폰해야됨 ID값은 InfoManager.Instance로 접근 가능
    void Start()
    {
        ID.text = "ID : "+InfoManager.Instance.id;

    }

    // Update is called once per frame
    void Update()
    {
    
    }


  
    public void LogoutButoonClicked()
    {
        SceneManager.LoadScene("Title_v2");
    }

    public void DeleteButtonClicked(GameObject button)
    {
        if (button == Character1)
        {
            True1.SetActive(false);
            False1.SetActive(true);
        }
        else if (button == Character2)
        {
            True2.SetActive(false);
            False2.SetActive(true);
        }
        else if (button == Character3)
        {
            True3.SetActive(false);
            False3.SetActive(true);
        }
        else
            Debug.LogError("Wrong Clicked");
    }


    // DB에 ID 정보와 함께 캐릭터값을 받아와야하는데 어떻게 해야될지..
    public void CreateButtonClicked(GameObject button)
    {

        // 만드는 부분 필요 캐릭터별 닉네임 ??
        if (button == Character1)
        {
            True1.SetActive(true);
            False1.SetActive(false);
        }
        else if (button == Character2)
        {
            True2.SetActive(true);
            False2.SetActive(false);
        }
        else if (button == Character3)
        {
            True3.SetActive(true);
            False3.SetActive(false);
        }
        else
            Debug.LogError("Wrong Clicked");
    }

    // 게임 시작 캐릭터 정보를 가진상태로 시작하게 만들어야함 text값 사용 하면 될듯
    public void StartButtonClicked(GameObject button)
    {
        if (button == Character1)
        {
            Debug.Log("Game Start with " + Character1name.text);
        }

        else if (button == Character2)
        {
            Debug.Log("Game Start with " + Character2name.text);

        }
        else if (button == Character3)
        {
            Debug.Log("Game Start with " + Character3name.text);

        }

        else
        {
            Debug.LogError("Wrong Clicked");
        }

    }
}
