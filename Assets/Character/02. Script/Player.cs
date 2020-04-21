using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    // Update is called once per frame

    public GameObject manager;
    protected override void Update()
    {
        GetInput();

        base.Move();
    }

    private void GetInput()
    {
        Vector2 moveVector;

#if UNITY_EDITOR

        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

#elif UNITY_ANDOROID


#endif

        direction = moveVector;
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("character arrive at end point");

        if(collision.gameObject.tag == "goal")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("key down space");
                manager.GetComponent<ManagerController>().change_map();
            }
        }
    }
}
