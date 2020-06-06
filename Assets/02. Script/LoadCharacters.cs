using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadCharacters : MonoBehaviour
{
    string jsonURL = "http://jajugoguma.synology.me/LoadChars.php";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getData());
    }

    IEnumerator getData()
    {
        Debug.Log("Start");
        WWW _www = new WWW(jsonURL);
        yield return _www;

        Debug.Log(_www.text);
        if (_www.error == null)
        {
            processJsonData(_www.text);
        }
        else
        {
            Debug.Log("Something worng...");
        }
    }

    private void processJsonData(string _url)
    {
        JsonDataClass _jsonData = JsonUtility.FromJson < JsonDataClass >(_url);
        Debug.Log(_jsonData.chracters[0].name);
        Debug.Log(_jsonData.chracters[1].name);
    }
}
