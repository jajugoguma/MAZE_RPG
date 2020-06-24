using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    PlayerData _playerData;
    Timer _timer;

    string[] rank;

    private string rankURL = "http://jajugoguma.synology.me/Ranking.php";

    // Start is called before the first frame update
    void Start()
    {
        _playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        _timer = GameObject.Find("Timer").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ranking()
    {
        StartCoroutine(connServer());
    }

    IEnumerator connServer()
    {
        int duration = _timer.getDuration();

        WWWForm form = new WWWForm();
        form.AddField("param_maze_index", _playerData.mazes[_playerData.maze_size * (int)_playerData.world_pose_x + (int)_playerData.world_pose_y]);
        form.AddField("param_name", _playerData.name);
        form.AddField("param_duration", duration);

        WWW _www = new WWW(rankURL, form);
        yield return _www;

        Debug.Log(_www.text);
        if (_www.error == null)
        {
            rank = _www.text.Split('\n');

            if (rank[0] == _playerData.name + ' ' + duration)
            {
                //1위 축하 등등
                Debug.Log("1st!!");
            }
        }
        else
        {
            Debug.Log("Something worng...");
        }
    }
}
