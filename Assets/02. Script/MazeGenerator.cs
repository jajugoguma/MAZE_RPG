using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class MazeGenerator : MonoBehaviour
{
    //Door opened cases (Self view)
    public const int NO = 0, RIGHT = 1, DOWN = 2, DOWNRIGHT = 3;

    //Door opened cases (Full direction)
    public const int YET = -1, U = 0, D = 1, R = 2, L = 3, UD = 4, UR = 5, UL = 6, UDR = 7,
     UDL = 8, URL = 9, UDRL = 10, DR = 11, DL = 12, DRL = 13, RL = 14;

    [DllImport("maze_generator")]
    public static extern int maze_gen(int[] list, int size);
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
        int[] maze = new int[maze_size * maze_size];

        int size = 0;
        size = maze_gen(maze, maze_size);


        //mazes는 추후 미로의 인덱스 값으로 변경
        mazes = "";

        System.Random r = new System.Random();

        for (int i = 0; i < maze_size * maze_size; i++)
        {
            maze[i] = r.Next(0, 9);
            mazes = mazes + maze[i].ToString() + ' ';
        }


        //입구의 위치 랜덤하게 생성
        if (r.Next(0,1) == 0)
        {
            if (r.Next(0, 1) == 0) 
            {
                in_x = 0;            
            } 
            else 
            {
                in_x = 9;
            }
            in_y = r.Next(0, 9);
        }
        else
        {
            if (r.Next(0, 1) == 0)
            {
                in_y = 0;
            }
            else
            {
                in_y = 9;
            }
            in_y = r.Next(0, 9);
        }
        //출구의 위치 랜덤하게 생성
         if (r.Next(0,1) == 0)
        {
            if (r.Next(0, 1) == 0) 
            {
                out_x = 0;            
            } 
            else 
            {
                out_x = 9;
            }
            out_y = r.Next(0, 9);
        }
        else
        {
            if (r.Next(0, 1) == 0)
            {
                out_y = 0;
            }
            else
            {
                out_y = 9;
            }
            out_y = r.Next(0, 9);
        }
               

        //door open cases
        
        int[] door = new int[maze_size * maze_size];

        for (int i = 0; i < maze_size; i++)
        {
            for (int j = 0; j < maze_size; j++) 
            {
                int index = maze_size * i + j;
                //Check self view
                switch(maze[index])
                {
                    case NO:
                        door[index] = YET;
                        break;
                    case RIGHT:
                        door[index] = R;
                        break;
                    case DOWN:
                        door[index] = D;
                        break;
                    case DOWNRIGHT:
                        door[index] = DR;
                        break;
                }

                //왼쪽 값 검사 (오른쪽이 열렸는지만 검사)
                if (j != 0 && maze[index - 1] == R) {
                    switch(door[index])
                    {
                        case YET:
                            door[index] = L;
                            break;
                        case R:
                            door[index] = RL;
                            break;
                        case D:
                            door[index] = DL;
                            break;
                        case DR:
                            door[index] = DRL;
                            break;
                    }
                }

                //위쪽 값 검사 (아래가 열렸는지만 검사)
                if (i != 0 && maze[index - (maze_size * i)] == D) 
                {
                    switch(door[index])
                    {
                        case YET:
                            door[index] = U;
                            break;
                        case R:
                            door[index] = UR;
                            break;
                        case D:
                            door[index] = UD;
                            break;
                        case DR:
                            door[index] = UDR;
                            break;
                        case L:
                            door[index] = UL;
                            break;
                        case RL:
                            door[index] = URL;
                            break;
                        case DL:
                            door[index] = UDL;
                            break;
                        case DRL:
                            door[index] = UDRL;
                            break;
                    }
                }
            }
        }

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
