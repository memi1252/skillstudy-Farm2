using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankAddUI : MonoBehaviour
{
    public InputField nameInputField;
    public Text playTimeText;
    public GameObject errorText;


    private void OnEnable()
    {
        nameInputField.text = "";
        errorText.SetActive(false);
        playTimeText.text = $"{(int)(GameManager.Instance.playTime / 3600):D2}:{(int)((GameManager.Instance.playTime % 3600) / 60):D2}:{(int)((GameManager.Instance.playTime % 3600) % 60):D2}";
    }


    public void Sumit()
    {
        if(nameInputField.text.Length < 3)
        {
            StartCoroutine(TextError());
        }
        else
        {
            RankManager.Instance.AddRank(nameInputField.text, GameManager.Instance.playTime);
            gameObject.SetActive(false);
        }
    }

    IEnumerator TextError()
    {
        errorText.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        errorText.SetActive(false);
    }
}
