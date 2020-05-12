using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManagerController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] maps;
    public GameObject player;
    public GameObject[] doors;
    public Image image;

    

    void Start()
    {
        //maps = GameObject.FindGameObjectsWithTag("map");
        Debug.Log("maps length: " + maps.Length);
        StartCoroutine("Fade");
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
     * 문을 어떻게 할지
     */
    public void select_door()
    {
        for(int i=0;i<doors.Length;i++)
        {
            doors[i].SetActive(false);
        }

    }

    /*
     * 어떤 맵을 선택할지
     */

    public void select_map()
    {
        
    }

    private void set_player_position(string name)
    {
        if (name == "goal_left")
        {
            player.transform.position = new Vector3(33.8f, -12.5f, 0);
            doors[3].SetActive(true);
        }
        else if (name == "goal_right")
        {
            player.transform.position = new Vector3(5.5f, -13.5f, 0);
            doors[2].SetActive(true);
        }
        else if (name == "goal_top")
        {
            player.transform.position = new Vector3(20.5f, -26.5f, 0);
            doors[1].SetActive(true);
        }
        else
        {
            player.transform.position = new Vector3(18.5f, 1f, 0);
            doors[0].SetActive(true);
        }
    }

    /*
     Active true인 map을 false로 변환후
     maps배열에 있는 맵을 하나 가져와서 true로 변환
    */
    public void change_map(string name)
    {
        Debug.Log("function change_map call");
        maps[0].SetActive(false);
        set_player_position(name);
        maps[2].SetActive(true);
        StartCoroutine("Fade");
        
    }
}
