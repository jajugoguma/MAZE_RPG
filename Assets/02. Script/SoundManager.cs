using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoSingleton<SoundManager>
{
    [Header("Sound Clip")]
    public AudioClip _tilte_bgm;
    public AudioClip _signup_bgm;

    public AudioSource audioComponent;


    // Start is called before the first frame update
    public void Init()
    {
        DontDestroyOnLoad(gameObject);
        audioComponent = gameObject.GetComponent<AudioSource>();

        audioComponent.time = 0;
        
        //audioComponent.playOnAwake = true;
        audioComponent.Play();

        Debug.Log(gameObject);
        /*
        // Debug Example
        if (null == audioComponent)
            Debug.LogError("null");
        else
            Debug.Log("정상");

        audioComponent.playOnAwake = true;
        */

        
    }


}
