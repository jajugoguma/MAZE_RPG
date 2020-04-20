using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    // Update is called once per frame
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
}
