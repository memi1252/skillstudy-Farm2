using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public bool inHouse = false;
    public Transform pos;

    public GameObject doorUI;
    public Text doorText;
    private Transform target;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.transform;
            doorUI.SetActive(true);
            if (inHouse)
            {
                //집 나가기
                doorText.text = "집을 나가겠습니까?";
                
            }
            else
            {
                //집 들어가기
                doorText.text = "집에 들어가겠습니까?";
            }
        }
    }

    public void Yes()
    {
        target.transform.position = pos.position;
        target.transform.rotation = pos.rotation;
        if (inHouse)
        {
            GameManager.Instance.player.Camera.SetActive(true);
        }
        else
        {
            GameManager.Instance.player.Camera.SetActive(false);
        }
        GetComponent<AudioSource>().Play();
        doorUI.SetActive(false);
    }

    public void No()
    {
        doorUI.SetActive (false);
    }
}
