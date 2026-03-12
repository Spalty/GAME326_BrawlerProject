using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "FighterData", menuName = "Game Data/FighterData")]
public class FighterData : ScriptableObject
{
    [Header("Fighter Properties")]
    [SerializeField] private float maxHealth = 1000f;
    
    [Header("Fighter Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float dashSpeed = 8f;
    [SerializeField] private int maxJumpCount = 2;
    [SerializeField] private float verticalJumpForce = 10f;
    [SerializeField] private float horizontalJumpForce = 5f;
    [SerializeField] private int maxAirDashCount = 1;
    [SerializeField] private float airDashSpeed = 15f;
    
    [Space(10)]
    
    [Header("---Attack Data---")]
    [Expandable][SerializeField] private HitboxData lightAtk;
    [Expandable][SerializeField] private HitboxData mediumAtk;
    [Expandable][SerializeField] private HitboxData heavyAtk;
    [Space(10)]
    [Expandable][SerializeField] private HitboxData jLightAtk;
    [Expandable][SerializeField] private HitboxData jMediumAtk;
    [Expandable][SerializeField] private HitboxData jHeavyAtk;
    [Space(10)]
    [Expandable][SerializeField] private HitboxData crLightAtk;
    [Expandable][SerializeField] private HitboxData crMediumAtk;
    [Expandable][SerializeField] private HitboxData crHeavyAtk;

    #region Getter / Setter Properties
    //Fighter Properties
    public float MaxHealth => maxHealth;
    public float WalkSpeed => walkSpeed;
    public float DashSpeed => dashSpeed;
    public float VerticalJumpForce => verticalJumpForce;
    public float HorizontalJumpForce => horizontalJumpForce;
    public int MaxJumpCount => maxJumpCount;
    public int MaxAirDashCount => maxAirDashCount;
    public float AirDashSpeed => airDashSpeed;

    //Attack Data
    public HitboxData LightAtk => lightAtk;
    public HitboxData MediumAtk => mediumAtk;
    public HitboxData HeavyAtk => heavyAtk;

    public HitboxData JLightAtk => jLightAtk;
    public HitboxData JMediumAtk => jMediumAtk;
    public HitboxData JHeavyAtk => jHeavyAtk;

    public HitboxData CRLightAtk => crLightAtk;
    public HitboxData CRMediumAtk => crMediumAtk;
    public HitboxData CRHeavyAtk => crHeavyAtk;
    #endregion
}
