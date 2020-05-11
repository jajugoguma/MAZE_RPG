using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    public Vector2 direction;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("attack", false);
        animator.SetBool("move", false);
    }

    protected virtual void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if(direction.x != 0 || direction.y != 0)
        {
            AnimateMovement();
        }
        else
        {
            animator.SetBool("move", false);
        }
    }

    public void AnimateMovement()
    {
        animator.SetBool("move", true);

        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }
}
