using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class MazeGenerator : MonoBehaviour
{
    [DllImport("maze_generator")]
    public static extern int maze_gen(int[] list, int size);

    void Start()
    {
        generate();
    }

    void generate()
    {
        int[] maze = new int[100];
        int size = 0;
        size = maze_gen(maze, 10);

        //미로가 정상적으로 만들어졌는지 테스트
        string s = "";

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                s = s + maze[i * 10 + j].ToString();
            }
            s = s + "\n";
        }

        Debug.Log(s);
        //########################################
    }
}
