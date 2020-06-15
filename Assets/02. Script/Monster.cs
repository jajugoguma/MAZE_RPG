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

    public float maxHp;
    public float currentHp;
    private float uihp;



    int MoveSpeed = 3;
    float MinDist = 1.0f;
    int MaxDist = 5;
    float attackdamage = 10f;

    Vector2 moveVec = Vector2.zero;



    void Start()
    {
        animator = GetComponent<Animator>();
        HpBar = GetComponentInChildren<RectTransform>();
        maxHp = 100f;
        currentHp = maxHp;
        MonsterManager.Instance.mosters.Add(this);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 vec = player.transform.position - transform.position;
        vec.Normalize();
        moveVec = vec;


        if (Vector3.Distance(transform.position, player.transform.position) <= MaxDist)
        {
            if (Vector3.Distance(transform.position, player.transform.position) >= MinDist)
            {
                Ray(moveVec);
                Debug.Log("chasing");
            }
        }
        else
        {
            //Idle 애니메이션으로 전환
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Magnitude", -0.1f);
        }
           
        if (Vector3.Distance(transform.position, player.transform.position) <= MinDist)
        {
            if (Time.time > lastAttacked + attackDelay)
            {
                animator.SetFloat("Attack_Horizontal", moveVec.x);
                animator.SetFloat("Attack_Vertical", moveVec.y);

                animator.SetBool("attack", true);

                player.attacked(attackdamage);
                lastAttacked = Time.time;
            }

        }

        if (currentHp <= 0)
        {
            //TODO : 죽음 애니메이션 추가
            gameObject.SetActive(false);
            player.exp += 20;
            player.ui_exp.ExpUIReflash();
            int prob = (int)Random.Range(0, 10);
            if (prob == 0)
            {
                player.worldItem.AddItem(new Item { itemType = Item.ItemType.Potion, pos = transform.localPosition });
            }
            else if (prob == 1)
            {
                player.worldItem.AddItem(new Item { itemType = Item.ItemType.Armor, pos = transform.localPosition });
            }
            else if (prob == 2)
            {
                player.worldItem.AddItem(new Item { itemType = Item.ItemType.Helmet, pos = transform.localPosition });
            }
            else if (prob == 3)
            {
                player.worldItem.AddItem(new Item { itemType = Item.ItemType.Sword, pos = transform.localPosition });
            }
            Debug.Log(prob);

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
            animator.SetFloat("Magnitude", -0.1f);
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
        if (currentHp <= 0)
        {
            currentHp = 0;
            return;
        }


        currentHp -= attackdamage;

        uihp = currentHp / maxHp;
        
        if (HpBar.localScale.x > 0.0f)
        {
            HpBar.localScale = new Vector3(uihp / 3 , 0.3f, 1.0f);
        }


    }

    public void resurrection()
    {
        if (gameObject.activeInHierarchy == false)
        {
            currentHp = maxHp;
            uihp = currentHp / maxHp;
            HpBar.localScale = new Vector3(uihp / 3 , 0.3f, 1.0f);
            gameObject.SetActive(true);
        }

    }

    //공격 애니매이션의 종료 확인
    public void setAttackfalse()
    {
        animator.SetBool("attack", false);
    }
}
