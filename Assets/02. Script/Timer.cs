using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Timer : MonoBehaviour
{
    private Stopwatch sw;
    private long millisecond;
    private int second;
    private int pre_second;

    // Start is called before the first frame update
    void Start()
    {
        setTimer();
    }

    // Update is called once per frame
    void Update()
    {
        millisecond = sw.ElapsedMilliseconds;
        second = (int)(millisecond / 1000);

        //매 프레임 밀리세컨드 단위 표시
        //Debug.Log("StopWatch : " + millisecond.ToString() + "ms");
        
        //매초 표시
        if (second != pre_second)
        {
            pre_second = second;
            Debug.Log("StopWatch : " + second.ToString() + "s");
        }
    }

    public void setTimer()
    {
        sw = new Stopwatch();
        pre_second = 0;
        sw.Start();
    }

    public int getDuration()
    {
        return second;
    }
}
