using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform player;
    public Transform RayCam;
    public Animator animator;
    public RectTransform HpBar;


    int MoveSpeed = 3;
    int MinDist = 1;
    int MaxDist = 5;

    Vector2 moveVec = Vector2.zero;

    public Vector2[] dir = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        HpBar = GetComponentInChildren<RectTransform>();
        MonsterManager.Instance.mosters.Add(this);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(transform.position, player.position) <= MaxDist)
            if(Vector3.Distance(transform.position,player.position) >= MinDist)
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

            if(HpBar.localScale.x <= 0.0f)
            {
                gameObject.SetActive(false);
            }
    }

    void Ray(Vector3 vec)
    {
        RaycastHit2D hit = Physics2D.Raycast(RayCam.position, vec, MaxDist);
        if (hit.transform.name.Equals("wall"))
        {
            animator.SetBool("move", false);
            //Debug.Log("Detect Wall");
        }
        else if (hit.transform.name.Equals("Player"))
        {
            transform.position += vec * MoveSpeed * Time.deltaTime;

            animator.SetBool("move", true);

            animator.SetFloat("x", vec.x);
            animator.SetFloat("y", vec.y);
        }
        else
        {
            //Debug.LogError("error");
        }
       // Debug.Log(hit.transform.name);


    }

    public void attackedfunc()
    {
        if (HpBar.localScale.x > 0.0f)
        {
            HpBar.localScale = new Vector3(HpBar.localScale.x - 0.100001f, 0.3f, 1.0f);
        }
       

    }

    public void resurrection()
    {
        if (gameObject.activeInHierarchy == false)
        {
            HpBar.localScale = new Vector3(0.3f, 0.3f, 1.0f);
            gameObject.SetActive(true);
        }
       
    }
}
