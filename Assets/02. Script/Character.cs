using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public Vector3 movement;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("attack", false);
        animator.SetBool("move", false);
        Debug.Log("animator");
    }

    protected virtual void Update()
    {
        Move();
    }

    public void Move()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + movement * speed * Time.deltaTime;
    }
}
