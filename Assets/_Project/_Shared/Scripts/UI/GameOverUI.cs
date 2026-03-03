using UnityEngine;
using TMPro;
using Brawler.Core;

public class GameOverUI: MonoBehaviour
{
    public TextMeshProUGUI winnerText;

    void Start()
    {
        winnerText.text = GameManager.winnerMessage;
    }
}