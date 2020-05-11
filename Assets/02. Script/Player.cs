﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityStandardAssets.CrossPlatformInput;

public class Player : Character
{
    private bool isAttack = false;
    public RectTransform HpBar;
    public Transform player;
    public Vector3 directionVec = Vector3.zero;
    public float attackdamage = 0.10001f;
    public Rigidbody2D rb;
    public Vector2 moveVector;


    void start()
    {
        HpBar = GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    protected override void Update()
    {
#if UNITY_EDITOR_WIN
        GetInput();
#endif


#if UNITY_ANDROID
        GetInput_joystick();
#endif

        if (!isAttack)
        {
            base.Move();
        }
    }

    public void attack(Vector3 directionVec)
    {
        animator.SetBool("attack", true);



        for (int i = 0; i < MonsterManager.Instance.mosters.Count; i++)
        {
            Vector3 monsterVec = MonsterManager.Instance.mosters[i].transform.position - transform.position;
            float dist = Vector3.Distance(MonsterManager.Instance.mosters[i].transform.position, transform.position);
            monsterVec.Normalize();

            float dot = Vector3.Dot(directionVec, monsterVec);
            Debug.Log(directionVec);
            Debug.Log(dot);
            if (dot > 0.5 && dist < 1)
            {
                AttackMoster(MonsterManager.Instance.mosters[i]);
            }

        }
    }


    private void AttackMoster(Monster monster)
    {
        Debug.Log("attack");
        // 나중에 맞는 애니메이션 및 채력 관련한 func 추가
        monster.attacked(attackdamage);

    }


    private void GetInput_joystick()
    {
        moveVector.x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        moveVector.y = CrossPlatformInputManager.GetAxisRaw("Vertical");


        if(moveVector.x != 0 && moveVector.y != 0)
        {
            directionVec = moveVector;
        }
        direction = moveVector;

        

       // if (CrossPlatformInputManager.GetButtonUp("Jump"))
     //  {
      //      Debug.Log("ket Up");
      //  }
        
       // Debug.Log(moveVector);

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveVector.x, moveVector.y);
    }

    private void GetInput()
    {
        
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

        direction = moveVector;
       // Debug.Log(animator.GetBool("attack"));
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Shift Down");
            speed = speed * (float)2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("Shift Up");
            speed = speed / (float)2;
        }
       

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            directionVec = Vector3.left;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            directionVec = Vector3.right;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            directionVec = Vector3.up;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            directionVec = Vector3.down;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            attack(directionVec);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < MonsterManager.Instance.mosters.Count; i++)
                MonsterManager.Instance.mosters[i].resurrection();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("attack", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (HpBar.localScale.x > 0.0f)
            {
                HpBar.localScale = new Vector3(HpBar.localScale.x - 0.1f, 1.0f, 1.0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (HpBar.localScale.x < 1.0f)
            {
                HpBar.localScale = new Vector3(HpBar.localScale.x + 0.1f, 1.0f, 1.0f);
            }
        }

    }

    public void attacked(float attackdamage)
    {
        //캐릭터 맞는 애니메이션 추가자리
        if (HpBar.localScale.x > 0.0f)
            HpBar.localScale = new Vector3(HpBar.localScale.x - attackdamage, 1.0f, 1.0f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("character arrive at end point");

        if (collision.gameObject.tag == "goal")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("key down space");
                //manager.GetComponent<ManagerController>().change_map();
            }
        }
    }

    
}
