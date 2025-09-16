using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageSlot : MonoBehaviour
{
    private Text text;
    private Animator animator;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Invoke("Hide", 5);
    }

    public Text Set()
    {
        return text;
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
        Invoke("Hide2", 1.5f);
    }

    public void Hide2()
    {
        Destroy(gameObject);
    }
}
