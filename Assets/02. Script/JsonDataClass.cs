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
    public double pose_x;
    public double pose_y;
}
