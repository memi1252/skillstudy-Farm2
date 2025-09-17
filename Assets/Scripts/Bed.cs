using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{
    public GameObject TimeSetUI;
    public Text TimeText;
    public Slider TimeSlider;
    private bool on;
    public GameObject timeView;
    public Text timeViewText;
    public Text timeViewText2;
    private bool view = false;




    private void Update()
    {
        if (on)
        {
            int value = (int)TimeSlider.value;
            TimeText.text = $"{value}시간";
        }
    }


    public void OnUI()
    {
        TimeSetUI.SetActive(true);
        on = true;
    }

    public void Yes()
    {
        view = true;
        timeView.SetActive(true);
        GameManager.Instance.sleep = true;
        sleep1();
        StartCoroutine(sleep());
    }

    IEnumerator sleep()
    {
        for (int i = 0; i <= TimeSlider.value; i++)
        {
            timeViewText.text = $"{GameManager.Instance.hour}시";
            GameManager.Instance.inGameTime += 3600;
            yield return new WaitForSecondsRealtime(1);
        }
        view = false;
        on = false;
        timeView.SetActive(false);
        TimeSetUI.SetActive(false);
        GameManager.Instance.sleep = false;
    }

    public void No()
    {
        TimeSlider.value = 1;
        TimeSetUI.SetActive(false);
    }

    private void sleep1()
    {
        timeViewText2.text = "수면중.";
        if (view)
        {
            Invoke("sleep2", 1);
        }
    }

    private void sleep2()
    {
        timeViewText2.text = "수면중..";
        if (view)
        {
            Invoke("sleep3", 1);
        }
    }

    private void sleep3()
    {
        timeViewText2.text = "수면중...";
        if (view)
        {
            Invoke("sleep1", 1);
        }
    }
}
