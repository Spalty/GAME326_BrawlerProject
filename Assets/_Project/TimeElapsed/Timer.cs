using UnityEngine;
using TMPro;

public class ElapsedTime: MonoBehaviour
{
    public TMP_Text timerText; //inspector
    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}