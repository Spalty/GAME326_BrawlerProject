using UnityEngine;
using Brawler.Core;
using NaughtyAttributes;

public class FighterGM : Singleton<FighterGM>
{
    [Expandable]
    [Header("---Match Configs---")]
    [SerializeField] private MatchConfig matchConfig;

    [Header("---Test---")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [Space(10)]
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;

    protected override void Awake()
    {
        base.Awake();

        //Instantiate(player1, spawnPoint1.position, spawnPoint1.rotation);
        //Instantiate(player2, spawnPoint2.position, spawnPoint2.rotation);
    }
}
