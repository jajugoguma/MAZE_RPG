using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManagerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject[] maps;
    
    public GameObject[] doors_ten;
    public GameObject[] doors_fifteen;
    private GameObject[] doors;

    public GameObject[] door_walls_ten;
    public GameObject[] door_walls_fifteen;
    private GameObject[] door_walls;

    public GameObject[] rest_doors;
    public GameObject rest_room;

    private GameObject now_map;
    public Image image;
    public int[,] world_map_ten;
    public int[,] random_map;
    public double world_position_l, world_position_r;
    private const int TEN = 10;
    private const int FIFTEEN = 15;
    public int map_size;
    private Vector3 left_door_position, right_door_position, top_door_position, bottom_door_position,
        rest_left_door_position
        , rest_right_door_position, rest_top_door_position, rest_bottom_door_position;

    void Start()
    {
        init_world_map();        
        //maps = GameObject.FindGameObjectsWithTag("map");
        Debug.Log("maps length: " + maps.Length);
        StartCoroutine("Fade");        
    }

    void set_random_map()
    {
        if(map_size == TEN)
        {
            random_map = new int[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    random_map[i, j] = j;
                }
            }
        }
        else
        {
            random_map = new int[15, 15];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    random_map[i, j] = j;
                }
            }
        }
    }

    void close_all_door()
    {
        for(int i=0;i<doors.Length;i++)
        {
            doors[i].SetActive(false);
        }
    }

    void close_all_rest_door()
    {
        for(int i=0;i<rest_doors.Length;i++)
        {
            rest_doors[i].SetActive(false);
        }
    }

    void set_doors()
    {
        if(map_size == TEN)
        {
            left_door_position = GameObject.Find("goal_left").transform.position;
            right_door_position = GameObject.Find("goal_right").transform.position;
            top_door_position = GameObject.Find("goal_top").transform.position;
            bottom_door_position = GameObject.Find("goal_bottom").transform.position;
            doors = doors_ten;
        }
        else
        {
            /*
             * 15X15 
             */
            doors = doors_fifteen;
        }
    }

    private void set_walls()
    {
        if (map_size == TEN)
        {
            door_walls = door_walls_ten;
        }
        else
        {
            door_walls = door_walls_fifteen;
        }
    }

    void init_world_map()
    {
        map_size = TEN; // 매개변수로 전달 받아야함
        world_position_l = 0;
        world_position_r = 0;
        set_random_map();

        world_map_ten = new int[10, 10] {
        { 1, 1, 1, 3, 0, 2, 2, 2, 1, 2 },
        { 3,1,1,2,3,2,3,1,0,2},
        {2,1,3,0,2,1,2,1,2,2 },
        {0,3,1,1,0,1,1,1,0,2 },
        {3,1,1,2,1,2,2,1,3,0 },
        {1,3,2,1,1,1,2,1,3,0 },
        {2,0,3,3,1,2,1,3,1,2 },
        {3,1,0,2,1,3,2,3,0,0 },
        {2,2,2,2,1,2,0,2,3,0 },
        {0,1,1,1,0,0,1,1,1,0 } };

        // left_door = GameObject.Find("goal_left").transform.position;
        // right_door = GameObject.Find("goal_right").transform.position;
        // top_door = GameObject.Find("goal_top").transform.position;
        // bottom_door = GameObject.Find("goal_bottom").transform.position;
        set_doors();
        set_walls();

        rest_left_door_position = GameObject.Find("rest_goal_left").transform.position;
        rest_right_door_position = GameObject.Find("rest_goal_right").transform.position;
        rest_top_door_position = GameObject.Find("rest_goal_top").transform.position;
        rest_bottom_door_position = GameObject.Find("rest_goal_bottom").transform.position;

        close_all_rest_door();
        close_all_door();
        now_map =maps[random_map[0, 0]];
        now_map.SetActive(true);
        select_door();
    }




    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.0045f) 
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
        close_all_rest_door();
        activeAllWalls();

        int now = world_map_ten[(int)world_position_l, (int)world_position_r];
        if(now ==3)
        {
            doors[1].SetActive(true);
            doors[3].SetActive(true);

            door_walls[1].SetActive(false);
            door_walls[3].SetActive(false);
        }
        else if(now==1)
        {
            doors[3].SetActive(true);

            door_walls[3].SetActive(false);
        }
        else if(now==2)
        {
            doors[1].SetActive(true);

            door_walls[1].SetActive(false);
        }

        if(world_position_r>0)
        {
            int l = world_map_ten[(int)world_position_l, (int)world_position_r - 1];
            if(l==1 || l==3)
            {
                doors[2].SetActive(true);

                door_walls[2].SetActive(false);
            }
        }
        if(world_position_l>0)
        {
            int r = world_map_ten[(int)world_position_l - 1, (int)world_position_r];
            if(r==2 || r==3)
            {
                doors[0].SetActive(true);

                door_walls[0].SetActive(false);
            }
        }
    }

    public void select_rest_door(string name)
    {
        if(name == "goal_left" || name == "goal_right")
        {
            rest_doors[3].SetActive(true);
            rest_doors[2].SetActive(true);
        }
        else
        {
            rest_doors[0].SetActive(true);
            rest_doors[1].SetActive(true);
        }
        rest_room.SetActive(true);
        now_map.SetActive(false);
        close_all_door();
    }

    private void activeAllWalls()
    {
        for (int i = 0; i < 4; i++)
            door_walls[i].SetActive(true);
    }

    /*
     * 어떤 맵을 선택할지
     */

    public void select_map()
    {
        //maps[random_map[(int)world_postion_l, (int)world_position_r]].SetActive(true);
        now_map = maps[random_map[(int)world_position_l, (int)world_position_r]];
        now_map.SetActive(true);
        rest_room.SetActive(false);
        close_all_rest_door();
        select_door();
        Debug.Log("L:" + world_position_l + " " + "R:" + world_position_r);
    }

    private void set_player_position(string name)
    {
   
        if (name == "goal_left")
        {
            player.transform.position = rest_right_door_position;      
            world_position_r -= 0.5;
            select_rest_door(name);
        }
        else if (name == "goal_right")
        {
            player.transform.position = rest_left_door_position;
            world_position_r += 0.5;
            select_rest_door(name);
        }
        else if (name == "goal_top")
        {
            player.transform.position = rest_bottom_door_position;
            world_position_l -= 0.5;
            select_rest_door(name);
        }
        else if(name == "goal_bottom")
        {
            player.transform.position = rest_top_door_position;
            world_position_l += 0.5;
            
            select_rest_door(name);
        }
        else if(name == "rest_goal_left")
        {
            player.transform.position = right_door_position;
            world_position_r -= 0.5;
            select_map();
        }
        else if(name == "rest_goal_right")
        {
            
            player.transform.position = left_door_position;
            world_position_r += 0.5;
            select_map();
        }
        else if(name == "rest_goal_top")
        {
            player.transform.position = bottom_door_position;
            world_position_l -= 0.5;
            select_map();
        }
        else if(name == "rest_goal_bottom")
        {
            player.transform.position = top_door_position;
            world_position_l += 0.5;
            select_map();
        }
    }

   
    public void change_map(string name)
    {
        Debug.Log("function change_map call");
        set_player_position(name);        
        StartCoroutine("Fade");        
    }
}
