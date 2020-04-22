using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    
   
    //Character True or False Controll
    public GameObject[] Characters;
    public GameObject[] True;
    public GameObject[] False;
   
    //Character name 
    public UILabel[] CharacterName;

    
    
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
        if (button == Characters[0])
        {
            True[0].SetActive(false);
            False[0].SetActive(true);
        }
        else if (button == Characters[1])
        {
            True[1].SetActive(false);
            False[1].SetActive(true);
        }
        else if (button == Characters[2])
        {
            True[2].SetActive(false);
            False[2].SetActive(true);
        }
        else
            Debug.LogError("Wrong Clicked");
    }


    // DB에 ID 정보와 함께 캐릭터값을 받아와야하는데 어떻게 해야될지..
    public void CreateButtonClicked(GameObject button)
    {

        // 만드는 부분 필요 캐릭터별 닉네임 ??
        if (button == Characters[0])
        {
            True[0].SetActive(true);
            False[0].SetActive(false);
        }
        else if (button == Characters[1])
        {
            True[1].SetActive(true);
            False[1].SetActive(false);
        }
        else if (button == Characters[2])
        {
            True[2].SetActive(true);
            False[2].SetActive(false);
        }
        else
            Debug.LogError("Wrong Clicked");
    }

    // 게임 시작 캐릭터 정보를 가진상태로 시작하게 만들어야함 text값 사용 하면 될듯
    public void StartButtonClicked(GameObject button)
    {
        if (button == Characters[0])
        {
            SceneManager.LoadScene("map_testbed");
            Debug.Log("Game Start with " + CharacterName[0].text);
        }

        else if (button == Characters[1])
        {
            Debug.Log("Game Start with " + CharacterName[1].text);

        }
        else if (button == Characters[2])
        {
            Debug.Log("Game Start with " + CharacterName[2].text);

        }

        else
        {
            Debug.LogError("Wrong Clicked");
        }

    }
}
