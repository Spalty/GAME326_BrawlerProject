using UnityEngine;
using Brawler.Core;
using NaughtyAttributes;

public class FighterGM : Singleton<FighterGM>
{
    [Expandable]
    [Header("---Match Configs---")]
    [SerializeField] private MatchConfig matchConfig;

    [Header("---Test---")]
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    [Space(10)]
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;

    protected override void Awake()
    {
        base.Awake();

        InitializePlayers();
    }

    private void InitializePlayers()
    {
        GameObject player1 = Instantiate(player1Prefab, spawnPoint1.position, spawnPoint1.rotation);
        GameObject player2 =  Instantiate(player2Prefab, spawnPoint2.position, spawnPoint2.rotation);

        PlayerStateMachine player1SM = player1.GetComponent<PlayerStateMachine>();
        PlayerStateMachine player2SM = player2.GetComponent<PlayerStateMachine>();

        player1SM.Opponent = player2.transform;
        player2SM.Opponent = player1.transform;
    }
}
