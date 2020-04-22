using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Monster : Character
{
    public Transform player;
    public Transform RayCam;

    int MoveSpeed = 4;
    int MinDist = 2;
    int MaxDist = 10;

    Vector2 moveVec = Vector2.zero;

    public Vector2[] dir = null;

    // Update is called once per frame
    protected override void Update()
    {
        
        if (Vector3.Distance(transform.position, player.position) <= MaxDist)
        {   
            Vector3 vec = player.position - transform.position;
            vec.Normalize();
            moveVec = vec;
            
            float absx = System.Math.Abs(moveVec.x);
            float absy = System.Math.Abs(moveVec.y);
            
            if (absx > absy)
            {
                if (moveVec.x > 0)
                    RayCam.transform.position = (Vector2)transform.position + dir[1];
                else if (moveVec.x < 0)
                    RayCam.transform.position = (Vector2)transform.position + dir[0];

            }
            else if (absy > absx)
            {
                if (moveVec.y > 0)
                    RayCam.transform.position = (Vector2)transform.position + dir[2];
                else if (moveVec.y < 0)
                    RayCam.transform.position = (Vector2)transform.position + dir[3];
            }
            Ray(moveVec);
            
            
            Debug.Log("chasing");
        }
    }

    void Ray(Vector3 vec)
    {
        RaycastHit2D hit = Physics2D.Raycast(RayCam.position, vec, MaxDist);
        if (hit.transform.name.Equals("wall"))
        {
            Debug.Log("Detect Wall");
        }
        else if (hit.transform.name.Equals("Player"))
        {
           transform.position += vec * MoveSpeed * Time.deltaTime;
        }
        else
        {
            Debug.LogError("error");
        }
       // Debug.Log(hit.transform.name);


    }
}
