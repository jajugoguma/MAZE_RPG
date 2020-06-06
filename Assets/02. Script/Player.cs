using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public RectTransform HpBar;
    public Transform player;
    public Vector3 directionVec = Vector3.zero;
    public float attackdamage = 0.10001f;
    public Rigidbody2D rb;
    public Vector3 moveVector;
    public bool flag = true;
    private Inventory inventory;
    public UI_Inventory uiInventory;

    private bool isKeyDwnRight, isKeyDwnLeft, isKeyDwnUp, isKeyDwnDown;



    void Start()
    {
        inventory = new Inventory();
        inventory.player = this;
        uiInventory.SetInventory(inventory);

        isKeyDwnRight = false; isKeyDwnLeft = false; isKeyDwnUp = false; isKeyDwnDown = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
#if UNITY_EDITOR_WIN
        
            GetInput();
#endif


#if UNITY_ANDROID
        if(flag)
            GetInput_joystick();
#endif
        //공격 중일땐 이동 속도 대폭 감소 (기본 속도)
        if (!animator.GetBool("attack"))
        {
            base.Move();
        }

        if(HpBar.localScale.x <= 0.0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    

    //public void attack(Vector3 directionVec)
    public void attack()
    {
        animator.SetFloat("Attack_Horizontal", directionVec.x);
        animator.SetFloat("Attack_Vertical", directionVec.y);

        animator.SetBool("attack", true);

        for (int i = 0; i < MonsterManager.Instance.mosters.Count; i++)
        {
            Vector3 monsterVec = MonsterManager.Instance.mosters[i].transform.position - transform.position;
            float dist = Vector3.Distance(MonsterManager.Instance.mosters[i].transform.position, transform.position);
            monsterVec.Normalize();

            float dot = Vector3.Dot(directionVec, monsterVec);
            Debug.Log(directionVec);
            //Debug.Log(dot);
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
        moveVector.z = 0f;
        moveVector.Normalize();
        Debug.Log(moveVector);
        movement = moveVector;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveVector.x, moveVector.y);
    }

    private void GetInput()
    {
        
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");
        moveVector.z = 0f;

        movement = moveVector;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Shift Down");
            speed = speed * 1.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("Shift Up");
            speed = speed / 1.5f;
        }


        setAttackDirect();


        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            attack();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < MonsterManager.Instance.mosters.Count; i++)
                MonsterManager.Instance.mosters[i].resurrection();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            //animator.SetBool("attack", false);
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

    private void setAttackDirect()
    {
        if (isKeyDwnLeft || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            directionVec = Vector3.left;
            isKeyDwnLeft = true;
        }

        if (isKeyDwnRight || Input.GetKeyDown(KeyCode.RightArrow))
        {
            directionVec = Vector3.right;
            isKeyDwnRight = true;
        }

        if (isKeyDwnUp || Input.GetKeyDown(KeyCode.UpArrow))
        {
            directionVec = Vector3.up;
            isKeyDwnUp = true;
        }

        if (isKeyDwnDown || Input.GetKeyDown(KeyCode.DownArrow))
        {
            directionVec = Vector3.down;
            isKeyDwnDown = true;
        }

        //키는 놓으면 안눌린것으로 판단.
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isKeyDwnLeft = false;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isKeyDwnRight = false;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isKeyDwnUp = false;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isKeyDwnDown = false;
        }
    }

    public void attacked(float attackdamage)
    {
        //캐릭터 맞는 애니메이션 추가자리
        if (HpBar.localScale.x > 0.0f)
            HpBar.localScale = new Vector3(HpBar.localScale.x - attackdamage, 1.0f, 1.0f);
    }

    public void UsePotion()
    {
        Debug.Log("use potion");
        if (HpBar.localScale.x < 0.8f)
            HpBar.localScale = new Vector3(HpBar.localScale.x + 0.2f, 1.0f, 1.0f);
        else
            HpBar.localScale = new Vector3(1.0f, 1.0f, 1.0f);

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

    //공격 애니메이션 종료 확인
    public void setAttackfalse()
    {
        animator.SetBool("attack", false);
    }

    
}
