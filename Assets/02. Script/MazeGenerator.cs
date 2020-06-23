using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Runtime.InteropServices;

public class MazeGenerator : MonoBehaviour
{
    //Door opened cases (Self view)
    public const int NO = 0, RIGHT = 1, DOWN = 2, DOWNRIGHT = 3;
    public const int Up = 2, Down = 3, Right = 5, Left = 7;

#if UNITY_ANDROID
            [DllImport ("mazeGen")]
            public static extern int maze_gen(int[] list, int size);

#else
    [DllImport("maze_generator")]
    public static extern int maze_gen(int[] list, int size);
#endif
    public string mazes;
    public string doors;
    public int maze_size;
    public int in_x;
    public int in_y;
    public int out_x;
    public int out_y;

    void Start()
    {
        generate();
    }

    public void generate()
    {
        //추후 미로 크기 선택 토글 추가로 변경 가능하게 수정
        //maze_size = 10;

        //maze의 값  =>  NO : 0    R : 1    D : 2    RL : 3

        //maze door의 값 => Up : 2    Down : 3    Right : 5   Left : 7    의 곱

        int[] maze = new int[maze_size * maze_size];

        int size = 0;
        size = maze_gen(maze, maze_size);

        mazes = "";

        System.Random r = new System.Random();

        for (int i = 0; i < maze_size * maze_size; i++)
        {
            int tmp = r.Next(0, maze_size - 1);
            mazes = mazes + tmp.ToString() + ' ';
        }

        in_x = 0;
        in_y = 0;

        out_x = maze_size - 1;
        out_y = maze_size - 1;


        //door open cases

        //int[] door = new int[maze_size * maze_size];
        int[] door = Enumerable.Repeat<int>(1, maze_size * maze_size).ToArray<int>();

        for (int i = 0; i < maze_size; i++)
        {
            for (int j = 0; j < maze_size; j++) 
            {
                int index = maze_size * i + j;

                switch (maze[index])
                {
                    case NO:
                        break;
                    case RIGHT:
                        door[index] *= Right;
                        door[index + 1] *= Left;
                        break;
                    case DOWN:
                        door[index] *= Down;
                        door[index + maze_size] *= Up;
                        break;
                    case DOWNRIGHT:
                        door[index] *= Right;
                        door[index] *= Down;
                        door[index + 1] *= Left;
                        door[index + maze_size] *= Up;
                        break;
                }
            }
        }

        door[maze_size * maze_size - 1] *= Right;

        doors = "";
         
        for (int i = 0; i < maze_size * maze_size; i++)
        {
            doors = doors + door[i].ToString() + ' ';
        }

        Debug.Log("doors");

        

        //미로가 정상적으로 만들어졌는지 테스트
        /*
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                s = s + maze[i * 10 + j].ToString();
            }
            s = s + "\n";
        }*/

        /* 문자열로 된 미로를 숫자로 변경
        string[] s2 = s.Split(' ');
        mazes = new int[s2.Length];
        for (int i = 0; i < s2.Length; i++)
        {
            Int32.TryParse(s2[i], out mazes[i]);
        }

        s.Remove(0);

        for (int i = 0; i < s2.Length; i++)
        {
            s = s + mazes[i].ToString();
        }
        Debug.Log(s);
        */
        //########################################
    }
}
