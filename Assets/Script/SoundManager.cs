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

        audioComponent.playOnAwake = true;

        Debug.Log(gameObject);

        /*// Debug Example
        if (null == audioComponent)
            Debug.LogError("null");
        else
            Debug.Log("정상");

        audioComponent.playOnAwake = true;
        */

        
    }

    // Update is called once per frame
    void Update()
    {
        //SceneManager.GetSceneAt(
    }

    Vector3 playerPos;
    Vector3 EnemyPos;

    void Temp()
    {
        Vector3 vec = (EnemyPos - playerPos);
        float dis = Vector3.Distance(EnemyPos, playerPos);

        vec.Normalize();

        float v1 = Vector3.Magnitude(vec);

        float te = vec.x * vec.y;
    }
}
