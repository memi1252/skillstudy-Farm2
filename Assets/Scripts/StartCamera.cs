using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    public GameObject Ui;
    private void Start()
    {
        Invoke("Start2", 5);
    }

    private void Start2()
    {
        Ui.SetActive(true);
        GameManager.Instance.dontMove = false;
        Destroy(gameObject);
    }
}
