using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayer : SampleCharacter
{
    private bool isAttack = false;
    public RectTransform HpBar;
    public GameObject manager;
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("key down space");
                manager.GetComponent<ManagerController>().change_map();
            }
        }
    }
}
