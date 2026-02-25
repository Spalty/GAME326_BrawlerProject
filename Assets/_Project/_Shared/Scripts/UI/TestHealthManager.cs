using System;
using UnityEngine;
using UnityEngine.UI;
using Brawler.Core;
using NaughtyAttributes;

public class TestHealthManager : MonoBehaviour
{
    public static event Action<int> OnHealthUpdated;

    [SerializeField] private int playerIndex;

    [Header("Debug")]
    public bool useDebug;
    [ShowIf("useDebug")]
    [Button]
    public void HitPlayer()
    {
        OnHealthUpdated?.Invoke(playerIndex);
    }
}
 
