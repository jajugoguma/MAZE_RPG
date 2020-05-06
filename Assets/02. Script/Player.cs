using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Player : Character
{
    private bool isAttack = false;
    public RectTransform HpBar;
    public Transform player;
    public Vector3 directionVec = Vector3.zero;


    void start()
    {
        HpBar = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();
        

        if (!isAttack)
        {
            base.Move();
        }
    }

    private void AttackMoster(Monster monster)
    {
        Debug.Log("attack");
        // 나중에 맞는 애니메이션 및 채력 관련한 func 추가
        monster.attackedfunc();

    }

    private void GetInput()
    {
        Vector2 moveVector;
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

        direction = moveVector;
        Debug.Log(animator.GetBool("attack"));
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
            animator.SetBool("attack", true);
            
            for (int i = 0; i < MonsterManager.Instance.mosters.Count; i++)
            {
                Vector3 monsterVec = MonsterManager.Instance.mosters[i].transform.position - transform.position;
                float dist = Vector3.Distance(MonsterManager.Instance.mosters[i].transform.position, transform.position);
                monsterVec.Normalize();

                float dot = Vector3.Dot(directionVec, monsterVec);
                Debug.Log(directionVec);
                if( dot > 0.5 && dist <1)
                {                   
                    AttackMoster(MonsterManager.Instance.mosters[i]);
                }

            }
           
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
