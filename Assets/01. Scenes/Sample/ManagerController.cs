using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManagerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public PlayerData data;

    public GameObject[] maps;
    public GameObject[] maps_10;
    public GameObject[] maps_15;
    public GameObject[] doors_ten;
    public GameObject[] doors_fifteen;
    private GameObject[] doors;

    public GameObject[] door_walls_ten;
    public GameObject[] door_walls_fifteen;
    private GameObject[] door_walls;

    public GameObject[] rest_doors;
    public GameObject rest_room;

    public Timer timer;

    private GameObject now_map;
    public Image image;
    public int[,] world_map_ten;
    public int[,] random_map;
    private int[,] world_doors;
    public float world_position_l, world_position_r;
    private const int TEN = 10;
    private const int FIFTEEN = 15;
    public int map_size;
    private Vector3 left_door_position, right_door_position, top_door_position, bottom_door_position,
        rest_left_door_position
        , rest_right_door_position, rest_top_door_position, rest_bottom_door_position;

    [SerializeField]
    private GameObject key;

    public UILabel description;

    private string[] ranks;


    private string rankURL = "http://jajugoguma.synology.me/Ranking.php";

    void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        data = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        init_world_map();

        //maps = GameObject.FindGameObjectsWithTag("map");
        Debug.Log("maps length: " + maps.Length);
#if UNITY_ANDROID
        image.gameObject.SetActive(false);
#else
        StartCoroutine("Fade");
#endif

    }

    void set_random_map(string r_str)
    {
        string[] token = r_str.Split(' ');
        if (map_size == TEN)
        {
            random_map = new int[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    random_map[i, j] = int.Parse(token[i * 10 + j]);
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
                    random_map[i, j] = int.Parse(token[i * 15 + j]);
                }
            }
        }
    }

    void close_all_door()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(false);
        }
    }

    void close_all_rest_door()
    {
        for (int i = 0; i < rest_doors.Length; i++)
        {
            rest_doors[i].SetActive(false);
        }
    }

    void set_doors()
    {
        if (map_size == TEN)
        {
            left_door_position = GameObject.Find("goal_left").transform.position;
            right_door_position = GameObject.Find("goal_right").transform.position;
            top_door_position = GameObject.Find("goal_top").transform.position;
            bottom_door_position = GameObject.Find("goal_bottom").transform.position;
            doors = doors_ten;
        }
        else
        {
            left_door_position = GameObject.Find("goal_left_15").transform.position;
            right_door_position = GameObject.Find("goal_right_15").transform.position;
            top_door_position = GameObject.Find("goal_top_15").transform.position;
            bottom_door_position = GameObject.Find("goal_bottom_15").transform.position;
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
    private void set_doors_array(string str, int size)
    {
        string[] result = str.Split(' ');
        world_doors = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                world_doors[i, j] = int.Parse(result[i * 10 + j]);
            }
        }
    }
    private void off_active(int size)
    {
        if (size == TEN)
        {
            for (int i = 0; i < 4; i++)
            {
                door_walls_fifteen[i].SetActive(false);
                doors_fifteen[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                door_walls_ten[i].SetActive(false);
                doors_ten[i].SetActive(false);
            }
        }
    }
    private void select_maps(int size)
    {
        if (size == TEN)
        {
            maps = maps_10;

        }
        else
        {
            maps = maps_15;
        }
    }

    void init_world_map()
    {

        map_size = data.maze_size;
        off_active(map_size);
        world_position_l = data.world_pose_y;
        world_position_r = data.world_pose_x;
        Debug.Log("l:" + world_position_l + " " + " r:" + world_position_r);
        set_doors_array(data.doors, map_size);
        select_maps(map_size);
        set_random_map(data.mazes);

        set_doors();
        set_walls();

        rest_left_door_position = GameObject.Find("rest_goal_left").transform.position;
        rest_right_door_position = GameObject.Find("rest_goal_right").transform.position;
        rest_top_door_position = GameObject.Find("rest_goal_top").transform.position;
        rest_bottom_door_position = GameObject.Find("rest_goal_bottom").transform.position;

        close_all_rest_door();
        close_all_door();

        //now_map =maps[random_map[0, 0]];
        now_map = maps[random_map[(int)world_position_l, (int)world_position_r]];
        now_map.SetActive(true);
        key.SetActive(true);
        /*
        for (int i = 0; i < MonsterManager.Instance.mosters.Count; i++)
        {
            MonsterManager.Instance.mosters[i].gameObject.SetActive(true);
        }
        */
        //MonsterManager.Instance.Able();
        select_door();

        if (world_position_r % 1 != 0)
        {
            select_rest_door("goal_left");
            MonsterManager.Instance.Disable();
            key.SetActive(false);
        }
        else if (world_position_l % 1 != 0)
        {
            select_rest_door("goal_bottom");
            MonsterManager.Instance.Disable();
            key.SetActive(false);
        }
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
        int now = world_doors[(int)world_position_l, (int)world_position_r];

        const int Up = 2, Down = 3, Right = 5, Left = 7;


        Debug.Log("select_door call! now door: " + now);

        if (now % Up == 0)
        {
            doors[0].SetActive(true);
            door_walls[0].SetActive(false);
        }

        if (now % Down == 0)
        {
            doors[1].SetActive(true);
            door_walls[1].SetActive(false);
        }

        if (now % Right == 0)
        {
            doors[3].SetActive(true);
            door_walls[3].SetActive(false);
        }

        if (now % Left == 0)
        {
            doors[2].SetActive(true);
            door_walls[2].SetActive(false);
        }
    }

    public void select_rest_door(string name)
    {
        if (name == "goal_left" || name == "goal_right" || name == "goal_left_15" || name == "goal_right_15")
        {
            rest_doors[3].SetActive(true);
            rest_doors[2].SetActive(true);
        }
        else if (name == "goal_bottom" || name == "goal_top" || name == "goal_bottom_15" || name == "goal_top_15")
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
        key.SetActive(true);
        MonsterManager.Instance.Able();

        rest_room.SetActive(false);
        close_all_rest_door();
        select_door();
        Debug.Log("L:" + world_position_l + " " + "R:" + world_position_r);
    }

    private void set_player_position(string name)
    {
        if (name == "goal_left" || name == "goal_left_15")
        {
            player.transform.position = rest_right_door_position;
            world_position_r -= 0.5f;
            select_rest_door(name);
            ranking((int)(world_position_r + 0.5), (int)world_position_l);
        }
        else if (name == "goal_right" || name == "goal_right_15")
        {
            player.transform.position = rest_left_door_position;
            world_position_r += 0.5f;
            select_rest_door(name);
            ranking((int)(world_position_r - 0.5), (int)world_position_l);
        }
        else if (name == "goal_top" || name == "goal_top_15")
        {
            player.transform.position = rest_bottom_door_position;
            world_position_l -= 0.5f;
            select_rest_door(name);
            ranking((int)world_position_r, (int)(world_position_l + 0.5));
        }
        else if (name == "goal_bottom" || name == "goal_bottom_15")
        {
            player.transform.position = rest_top_door_position;
            world_position_l += 0.5f;
            select_rest_door(name);
            ranking((int)world_position_r, (int)(world_position_l - 0.5));
        }
        else if (name == "rest_goal_left")
        {
            player.transform.position = right_door_position;
            world_position_r -= 0.5f;
            select_map();
            timer.setTimer();
        }
        else if (name == "rest_goal_right")
        {

            player.transform.position = left_door_position;
            world_position_r += 0.5f;
            select_map();
            timer.setTimer();
        }
        else if (name == "rest_goal_top")
        {
            player.transform.position = bottom_door_position;
            world_position_l -= 0.5f;
            select_map();
            timer.setTimer();
        }
        else if (name == "rest_goal_bottom")
        {
            player.transform.position = top_door_position;
            world_position_l += 0.5f;
            select_map();
            timer.setTimer();
        }

        data.world_pose_x = world_position_r;
        data.world_pose_y = world_position_l;

        data.saveData(data);

    }


    public void change_map(string name)
    {
        Debug.Log("function change_map call");
        set_player_position(name);
#if UNITY_ANDROID
       image.gameObject.SetActive(false); 
#else
        StartCoroutine("Fade");
#endif    
    }

    public void ranking(int world_pose_x, int world_pose_y)
    {
        data.printRanking = true;
        int duration = timer.getDuration();
        if (duration > 20)
        {
            StartCoroutine(setRank(duration, world_pose_y, world_pose_y));
        }
        else
        {
            data.printRanking = false;
        }
    }

    IEnumerator setRank(int dur, int world_pose_x, int world_pose_y)
    {
        WWWForm form = new WWWForm();
        form.AddField("param_maze_size", data.maze_size);
        form.AddField("param_maze_index", random_map[world_pose_x, world_pose_y]);
        form.AddField("param_name", data.name);
        form.AddField("param_duration", dur);

        WWW _www = new WWW(rankURL, form);
        yield return _www;

        Debug.Log(_www.text);

        if (_www.error == null)
        {
            ranks = _www.text.Split(';');
        }
        else
        {
            Debug.Log("Something worng...");
        }

        StartCoroutine(printRank());
    }

    IEnumerator printRank()
    {
        description.text = "Ranking of previous maze";
        yield return new WaitForSeconds(2);
        for (int i = 0; i < ranks.Length - 1; i++)
        {
            string s;
            switch(i)
            {
                case 0:
                    s = "1st : ";
                    break;
                case 1:
                    s = "2nd : ";
                    break;
                case 2:
                    s = "3rd : ";
                    break;
                default:
                    s = (i + 1).ToString() + "th : ";
                    break;
            }
            description.text = s + " : " + ranks[i] + "s";
            yield return new WaitForSeconds(2);

            if (i >= 4) break;
        }
        description.text = "";
        data.printRanking = false;
    }
}

