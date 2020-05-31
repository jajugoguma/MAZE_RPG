using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScene : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
        SoundManager.Instance.Init();
        InfoManager.Instance.Init();
        MonsterManager.Instance.Init();
        SceneManager.LoadScene("Title_v3");

#if UNITY_EDITOR_WIN
        Debug.Log("unity");
#endif
#if UNITY_ANDROID
        Debug.Log("android");
#endif


    }

  
}
