using UnityEngine;
using TMPro;

public class RoundCountdown : MonoBehaviour
{
    private TextMeshProUGUI _countdownText;

    private void Awake()
    {
        _countdownText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void UpdateCountdownDisplay()
    {
        //Round n
        //Wait...
        //Then 3, 2, 1
        //Wait...
        //Fight!
    }
}
