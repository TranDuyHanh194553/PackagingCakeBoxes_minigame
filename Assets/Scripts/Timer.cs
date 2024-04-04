using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float remainTime = 45;
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        if (remainTime > 0)
        {
            remainTime -= Time.deltaTime;
        }
        else
        {
            remainTime = 0;
            gameManager.GameOver();
        }        
        int minutes = Mathf.FloorToInt(remainTime / 60);
        int seconds = Mathf.FloorToInt(remainTime % 60);
        timerText.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }
}
