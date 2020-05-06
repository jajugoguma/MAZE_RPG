using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoSingleton<MonsterManager>
{
    public List<Monster> mosters;

    // Start is called before the first frame update
    public void Init()
    {
        DontDestroyOnLoad(gameObject);
     //   Debug.Log("xxxxx");
    }
}
