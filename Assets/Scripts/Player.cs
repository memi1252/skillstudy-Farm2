using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidbody;

    public float moveSpeed =4;
    public GameObject Camera;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CameraMove();
    }

    private void LateUpdate()
    {
        Move();
    }

    public void Move()
    {
        if(GameManager.Instance.dontMove || GameManager.Instance.clear)
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = transform.forward * v;
        dir.Normalize();
        rigidbody.velocity =dir * moveSpeed;
        float move = Mathf.Lerp(h, v, 1f);

        transform.eulerAngles += Vector3.up * h * moveSpeed *10 * Time.deltaTime;
        
        animator.SetFloat("Move", move);
        
    }

    public void CameraMove()
    {
        if (GameManager.Instance.dontMove || GameManager.Instance.clear)
        {
            return;
        }
        if (Input.GetMouseButton(1))
        {
            float xMouse = Input.GetAxis("Mouse X");
            transform.eulerAngles += Vector3.up * xMouse;
        }
    }
}
