using System;
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

    public GameObject logoutBtn;


    //Character name 
    public JsonDataClass _jsonData;
    public UILabel[] CharacterName;
    public GameObject PlayerData;

    
    //ID
    public UILabel ID;

    [Header("CreateCharacter")]
    public UIInput name_Input;
    public GameObject namInputGO;
    public GameObject createButton;
    public GameObject cancelButton;
    public GameObject checkBox;

    public MazeGenerator mazeGen;
    private int isHardMode = 1;



    //Andoroid only

    private string jsonURL = "http://jajugoguma.synology.me/LoadChars.php";
    private string createURL = "http://jajugoguma.synology.me/CreateChar.php";
    private string deleteURL = "http://jajugoguma.synology.me/DeleteChar.php";


    // Start is called before the first frame update
    // ID 정보값에 맞춰서 DB에서 케릭터들을 가져와서 스폰해야됨 ID값은 InfoManager.Instance로 접근 가능
    void Start()
    {
        disableCreateObjects();

        ID.text = "ID : " + InfoManager.Instance.id;

        StartCoroutine(getData());

        mazeGen = GameObject.Find("MazeGen").GetComponent<MazeGenerator>();
    }

    IEnumerator getData()
    {
        Debug.Log("Start");

        WWWForm form = new WWWForm();
        form.AddField("param_id", InfoManager.Instance.id);

        WWW _www = new WWW(jsonURL, form);
        yield return _www;

        Debug.Log(_www.text);
        if (_www.error == null)
        {
            loadCharacters(_www.text);
        }
        else
        {
            Debug.Log("Something worng...");
        }
    }

    private void loadCharacters(string _url)
    {
        _jsonData = JsonUtility.FromJson<JsonDataClass>(_url);
        Debug.Log(_jsonData.chracters.Count.ToString());

        int i;
        for (i = 0; i < _jsonData.chracters.Count; i++)
        {
            CharacterName[i].text = _jsonData.chracters[i].name;
            True[i].SetActive(true);
            False[i].SetActive(false);
        }

        for (int j = i; j < 3; j++)
        {
            True[j].SetActive(false);
            False[j].SetActive(true);
        }
    }

    private void disableCreateObjects()
    {
        namInputGO.SetActive(false);
        createButton.SetActive(false);
        cancelButton.SetActive(false);
        checkBox.SetActive(false);
    }

    private void enableCreateObjects()
    {
        namInputGO.SetActive(true);
        createButton.SetActive(true);
        cancelButton.SetActive(true);
        checkBox.SetActive(true);
    }

    private void disableSelectObjects()
    {
        for (int i = 0; i < 3; i++)
            Characters[i].SetActive(false);
        logoutBtn.SetActive(false);
    }

    private void enableSelectObjects()
    {
        for (int i = 0; i < 3; i++)
            Characters[i].SetActive(true);
        logoutBtn.SetActive(true);
    }
  
    public void LogoutButoonClicked()
    {
        Destroy(PlayerData.gameObject);
        SceneManager.LoadScene("Title_v3");
    }

    public void DeleteButtonClicked(GameObject button)
    {
        for (int i = 0; i < 3; i++) {
            if (button == Characters[i])
            {
                StartCoroutine(deleteCharacter(CharacterName[i].text));
            }
        }
    }

    IEnumerator deleteCharacter(string name)
    {
        WWWForm form = new WWWForm();
        form.AddField("param_name", name);

        WWW _www = new WWW(deleteURL, form);
        yield return _www;

        Debug.Log(_www.text);
        if (_www.error == null)
        {
            if (_www.text == "delete complete")
            {
                Debug.Log("delete complete");
            }
            else
            {
                Debug.Log("delete Fail");
            }
        }
        else
        {
            Debug.Log("Something worng...");
        }

        yield return StartCoroutine(getData());
    }


    // DB에 ID 정보와 함께 캐릭터값을 받아와야하는데 어떻게 해야될지..
    public void CreateButtonClicked(GameObject button)
    {

        // 만드는 부분 필요 캐릭터별 닉네임 ??
        if (button == Characters[0] || button == Characters[1] || button == Characters[2])
        {
            enableCreateObjects();
            disableSelectObjects();
        }
        else
            Debug.LogError("Wrong Clicked");
    }

    public void charCreateBtnClicked()
    {
        if (String.IsNullOrEmpty(name_Input.value))
        {
            Debug.Log("input name");
        }
        else
        {
            StartCoroutine(createCharacter());
        }

        disableCreateObjects();
        enableSelectObjects();
    }

    public void charCancelBtnClicked()
    {
        disableCreateObjects();
        enableSelectObjects();
    }

    IEnumerator createCharacter()
    {
        mazeGen.generate();

        WWWForm form = new WWWForm();
        form.AddField("param_id", InfoManager.Instance.id);
        form.AddField("param_name", name_Input.value);
        form.AddField("param_maze_size", mazeGen.maze_size);
        form.AddField("param_mazes", mazeGen.mazes);
        form.AddField("param_doors", mazeGen.doors);
        form.AddField("param_in_x", mazeGen.in_x);
        form.AddField("param_in_y", mazeGen.in_y);
        form.AddField("param_out_x", mazeGen.out_x);
        form.AddField("param_out_y", mazeGen.out_y);

        WWW _www = new WWW(createURL, form);
        yield return _www;

        Debug.Log(_www.text);
        if (_www.error == null)
        {
            if (_www.text == "create complete")
            {
                Debug.Log("create complete");
            }
            else
            {
                Debug.Log("Create Fail");
            }
        }
        else
        {
            Debug.Log("Something worng...");
        }

        yield return StartCoroutine(getData());
    }

    // 게임 시작 캐릭터 정보를 가진상태로 시작하게 만들어야함 text값 사용 하면 될듯
    public void StartButtonClicked(GameObject button)
    {
        for (int i = 0; i < 3; i++)
        {
            if (button == Characters[i])
            {
                PlayerData.GetComponent<PlayerData>().loadData(_jsonData.chracters[i]);
                //SceneManager.LoadScene("GameScene_backup");
                SceneManager.LoadScene("GameScene");
                Debug.Log("Game Start with " + CharacterName[i].text);
            }
        }

    }

    public void CheckHardMode()
    {
        if (isHardMode == 0)
            isHardMode = 1;
        else
            isHardMode = 0;
        Debug.Log(isHardMode);
                   
    }

}
