using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameClearUI : MonoBehaviour
{
    public Text playTimeText;


    public void Show()
    {
        gameObject.SetActive(true);
        playTimeText.text = $"플레이 타임 : {(int)(GameManager.Instance.playTime/3600)}:{(int)((GameManager.Instance.playTime%3600)/60)}:{(int)((GameManager.Instance.playTime%3600)%60)}";
    }

    public void Main()
    {
        SceneManager.LoadScene(0);
    }

    public void ReStart()
    {
        SceneManager.LoadScene(1);
    }

    public void Rank()
    {
        RankManager.Instance.RankUIOpen();
    }


}
