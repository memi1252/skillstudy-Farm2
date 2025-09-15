using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform target;


    private void Start()
    {
        target = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if(target != null)
        {
            transform.LookAt(transform.position + target.rotation * Vector3.forward, target.rotation * Vector3.up);
        }
    }
}
