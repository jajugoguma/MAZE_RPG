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

    public void Disable()
    {
       for (int i = 0; i<mosters.Count; i++)
        {
            mosters[i].gameObject.SetActive(false);
        }
    }

    public void Able()
    {
        for (int i = 0; i < mosters.Count; i++)
        {
            Debug.Log("able");
            mosters[i].gameObject.SetActive(true);
            mosters[i].transform.position = mosters[i].initpos;
        }
    }
}
