using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private bool isAttack = false;
    public RectTransform HpBar;

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

    private void GetInput()
    {
        Vector2 moveVector;
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

        direction = moveVector;

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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool("attack", true);
            SetAttackTure();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("attack", false);
            SetAttackFalse();
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

    public void SetAttackTure()
    {
        isAttack = true;
    }

    public void SetAttackFalse()
    {
        isAttack = false;
        Debug.Log(isAttack);
    }
}
