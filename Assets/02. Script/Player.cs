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
    public float attackdamage;
    public Rigidbody2D rb;
    public Vector3 moveVector;
    public bool flag = true;

    public Inventory inventory;
    public Equipment equipment;
    public WorldItem worldItem;
    public UI_Inventory uiInventory;
    public UI_Equipment uiEquipment;
    public UI_worldItem uiWorldItem;

    public UI_Exp ui_exp;
    public GameObject manager;
    public int level;
    public int levPoint;
    public int maxHp;
    public int currentHp;
    private float uihp;
    public int maxExp;
    public int exp;



    private PlayerData _playerData;

    private float TimeLeft = 10.0f;
    private float nextTime = 0.0f;


   

   

    private bool isKeyDwnRight, isKeyDwnLeft, isKeyDwnUp, isKeyDwnDown;



    void Start()
    {
        _playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        Vector3 initpos = new Vector3(_playerData.pose_x, _playerData.pose_y, 0.0f);
        transform.position = initpos;

        //인벤토리 및 장비창 초기화
        inventory = new Inventory(this);
        equipment = new Equipment(this);
        worldItem = new WorldItem(this);

        level = _playerData.level;
        levPoint = _playerData.ap;
        maxHp = _playerData.max_hp;
        attackdamage = _playerData.atk;
        currentHp = _playerData.cur_hp ;
        exp = _playerData.exp;
        maxExp = level * 50;

        uiInventory.SetInventory(inventory);
        uiEquipment.SetEquipment(equipment);
        uiWorldItem.SetWorldItem(worldItem);

        ui_exp.ExpUIReflash();

        uihp = (float)currentHp / maxHp;

        if (HpBar.localScale.x > 0.0f)
            HpBar.localScale = new Vector3(uihp, 1.0f, 1.0f);

        //인벤토리에 테스트 값넣음
        inventory.TestInsert();
        worldItem.TestInsert();

        isKeyDwnRight = false; isKeyDwnLeft = false; isKeyDwnUp = false; isKeyDwnDown = false;

        //expBar.value = 1;
    }

    // Update is called once per frame
    protected override void Update()
    {


        GetInput();
        

#if UNITY_ANDROID
        if(flag)
            GetInput_joystick();
#endif
        //공격 중일땐 이동 속도 대폭 감소 (기본 속도)
        if (!animator.GetBool("attack"))
        {
#if UNITY_ANDROID
            base.MoveMobile();
#else
            base.Move();
#endif
        }

        if (currentHp <= 0)
        {
            //다시시작을 다른방법으로 해야할듯 정보가 다 끊김

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            MonsterManager.Instance.mosters.Clear();
        }

        if (maxExp <= exp)
        {
            level += 1;
            levPoint += 3;
            exp = 0;
            maxExp = level * 50;
        }

        if (Time.time > nextTime)
        {
            nextTime = Time.time + TimeLeft;
            _playerData.saveData(transform.position.x, transform.position.y, maxHp,currentHp,exp);
           
            
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

        if (currentHp <= 0)
        {
            currentHp = 0;
            return;
        }

        currentHp -= (int)attackdamage;
        uihp = (float)currentHp / maxHp;
      
        if (HpBar.localScale.x > 0.0f)
            HpBar.localScale = new Vector3(uihp, 1.0f, 1.0f);
    }

    public void UsePotion()
    {
        Debug.Log("use potion");
        if (currentHp + 20 > maxHp)
            currentHp = maxHp;
        else
            currentHp += 20;

        uihp = currentHp / maxHp;
        HpBar.localScale = new Vector3(uihp, 1.0f, 1.0f);
     }

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("character arrive at end point");

        if (collision.gameObject.tag == "goal")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("key down space");
                
                manager.GetComponent<ManagerController>().change_map(collision.gameObject.name);
            }
        }
    }

    //공격 애니메이션 종료 확인
    public void setAttackfalse()
    {
        animator.SetBool("attack", false);
    }

    
}
