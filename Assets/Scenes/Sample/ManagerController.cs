using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManagerController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] maps;
    public GameObject player;
    public Image image;

    void Start()
    {
        //maps = GameObject.FindGameObjectsWithTag("map");
        Debug.Log("maps length: " + maps.Length);
        
    }


    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.0095f) 
        {
            Color c = image.material.color;
            c.a = f;
            image.material.color = c;
            yield return null;
        }
    }

    /*
     Active true인 map을 false로 변환후
     maps배열에 있는 맵을 하나 가져와서 true로 변환
    */
    public void change_map()
    {
        Debug.Log("function change_map call");
        maps[0].SetActive(false);
        player.transform.position = new Vector3(-0.18f, -9.14f, 0);
        maps[1].SetActive(true);
        StartCoroutine("Fade");
    }
}
