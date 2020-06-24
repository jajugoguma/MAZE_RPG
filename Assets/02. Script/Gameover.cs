using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    public PlayerData _playerData;
    public UILabel uptime;


    // Start is called before the first frame update
    void Start()
    {
        _playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();

        uptime.text = "Play Time : " + (_playerData.play_time / 3600).ToString() + "h " + ((_playerData.play_time % 3600) / 60).ToString() + "m " + (_playerData.play_time % 60).ToString() + "s";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
