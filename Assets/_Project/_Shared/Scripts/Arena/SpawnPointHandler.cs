using Brawler.Arena;
using Brawler.Core;
using UnityEngine;

public class SpawnPointHandler : MonoBehaviour
{
    [Header("---Spawn Points---")]
    [SerializeField] private SpawnPoint[] spawnPoints;
    public SpawnPoint[] SpawnPoints => spawnPoints;

    private void Awake()
    {
        ServiceLocator.Register(this);
    }
}
