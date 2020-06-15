using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string name;
    public string own_id;
    public int level;
    public int max_hp;
    public int cur_hp;
    public int exp;
    public int atk;
    public int luck;
    public int health;
    public int ap;
    public float pose_x;
    public float pose_y;
    public int maze_size;
    public string mazes;
    public string doors;
    public int world_pose_x;
    public int world_pose_y;
    public int in_x;
    public int in_y;
    public int out_x;
    public int out_y;

    private string saveURL = "http://jajugoguma.synology.me/SaveChar.php";

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
       // Debug.Log(name);
    }

    public void loadData(Chracters data)
    {
        name = data.name;
        own_id = data.own_id;
        level = data.level;
        max_hp = data.max_hp;
        cur_hp = data.cur_hp;
        exp = data.exp;
        atk = data.atk;
        luck = data.luck;
        health = data.health;
        ap = data.ap;
        pose_x = float.Parse(data.pose_x);
        pose_y = float.Parse(data.pose_y);
        maze_size = data.maze_size;
        mazes = data.mazes;
        doors = data.doors;
        world_pose_x = data.world_pos_x;
        world_pose_y = data.world_pos_y;
        in_x = data.in_x;
        in_y = data.in_y;
        out_x = data.out_x;
        out_y = data.out_y;
    }



    public void saveData(float posi_x, float posi_y, int max_hp_, int cur_hp_, int exp_, int level_)
    {
        pose_x = posi_x;
        pose_y = posi_y;
        max_hp = max_hp_;
        cur_hp = cur_hp_;
        exp = exp_;
        level = level_;

        StartCoroutine(createCharacter());
    }

    IEnumerator createCharacter()
    {

        WWWForm form = new WWWForm();
        form.AddField("param_name", name);
        form.AddField("param_level", level);
        form.AddField("param_max_hp", max_hp);
        form.AddField("param_cur_hp", cur_hp);
        form.AddField("param_exp", exp);
        form.AddField("param_atk", atk);
        form.AddField("param_luck", luck);
        form.AddField("param_health", health);
        form.AddField("param_ap", ap);
        form.AddField("param_pose_x", pose_x.ToString());
        form.AddField("param_pose_y", pose_y.ToString());
        form.AddField("param_world_pose_x", world_pose_x);
        form.AddField("param_world_pose_y", world_pose_y);

        WWW _www = new WWW(saveURL, form);
        yield return _www;

        Debug.Log(_www.text);
        if (_www.error == null)
        {
            if (_www.text == "save complete")
            {
                Debug.Log("save complete");
            }
            else
            {
                Debug.Log("save Fail" + _www.text);
            }
        }
        else
        {
            Debug.Log("Something worng...");
        }
    }
}
