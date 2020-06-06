using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Player player;
    public Animator animator;
    public RectTransform HpBar;
    public float attackDelay = 2f;
    float lastAttacked = -9999f;


    int MoveSpeed = 3;
    float MinDist = 1.0f;
    int MaxDist = 5;
    float attackdamage = 0.1000001f;

    Vector2 moveVec = Vector2.zero;



    void Start()
    {
        animator = GetComponent<Animator>();
        HpBar = GetComponentInChildren<RectTransform>();
        MonsterManager.Instance.mosters.Add(this);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 vec = player.transform.position - transform.position;
        vec.Normalize();
        moveVec = vec;


        if (Vector3.Distance(transform.position, player.transform.position) <= MaxDist)
            if (Vector3.Distance(transform.position, player.transform.position) >= MinDist)
            {
                Ray(moveVec);
                Debug.Log("chasing");
            }
        if ((Vector3.Distance(transform.position, player.transform.position)) <= MinDist)
        {
            if (Time.time > lastAttacked + attackDelay)
            {
                animator.SetFloat("Attack_Horizontal", moveVec.x);
                animator.SetFloat("Attack_Vertical", moveVec.y);

                animator.SetBool("attack", true);

                //몬스터가 캐릭터를 공격 몬스터 공격 애니메이션 추가 자리
                player.attacked(attackdamage);
                lastAttacked = Time.time;
            }

        }

        if (HpBar.localScale.x <= 0.0f)
        {
            gameObject.SetActive(false);
        }
    }

    void Ray(Vector3 vec)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vec, MaxDist);
        Debug.Log(hit.transform.name);
        if (hit.transform.name.Equals("wall"))
        {
            //animator.SetBool("move", false);
            //Debug.Log("Detect Wall");
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Magnitude", -0.001f);
        }
        else if (hit.transform.name.Equals("Player"))
        {
            transform.position += vec * MoveSpeed * Time.deltaTime;
            //animator.SetBool("move", true);
            animator.SetFloat("Horizontal", vec.x);
            animator.SetFloat("Vertical", vec.y);
            animator.SetFloat("Magnitude", vec.magnitude);
        }
        else
        {
            //Debug.LogError("error");
        }
        // Debug.Log(hit.transform.name);


    }

    public void attacked(float attackdamage)
    {
        if (HpBar.localScale.x > 0.0f)
        {
            HpBar.localScale = new Vector3(HpBar.localScale.x - attackdamage, 0.3f, 1.0f);
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

    //공격 애니매이션의 종료 확인
    public void setAttackfalse()
    {
        animator.SetBool("attack", false);
    }
}
