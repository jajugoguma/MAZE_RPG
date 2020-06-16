using System;
using System.Collections.Generic;

[Serializable]
public class JsonDataClass
{
    public List<Chracters> chracters;
}

[Serializable]
public class Chracters
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
    public string pose_x;
    public string pose_y;
    public int maze_size;
    public string mazes;
    public string doors;
    public int world_pos_x;
    public int world_pos_y;
    public int in_x;
    public int in_y;
    public int out_x;
    public int out_y;
    public string inven;
    public string equip;
}
