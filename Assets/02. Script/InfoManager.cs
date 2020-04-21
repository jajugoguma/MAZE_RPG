using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoSingleton<InfoManager>
{

    public string id = null;
    public string pwd = null;

    // Start is called before the first frame update
    public void Init()
    {
       DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
