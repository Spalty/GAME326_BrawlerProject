using UnityEngine;
using Brawler.Combat;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "FighterData", menuName = "Scriptable Objects/FighterData")]
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
    [Expandable][SerializeField] private AttackData lightAtk;
    [Expandable][SerializeField] private AttackData mediumAtk;
    [Expandable][SerializeField] private AttackData heavyAtk;
    [Space(10)]
    [Expandable][SerializeField] private AttackData jLightAtk;
    [Expandable][SerializeField] private AttackData jMediumAtk;
    [Expandable][SerializeField] private AttackData jHeavyAtk;
    [Space(10)]
    [Expandable][SerializeField] private AttackData crLightAtk;
    [Expandable][SerializeField] private AttackData crMediumAtk;
    [Expandable][SerializeField] private AttackData crHeavyAtk;

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
    public AttackData LightAtk => lightAtk;
    public AttackData MediumAtk => mediumAtk;
    public AttackData HeavyAtk => heavyAtk;

    public AttackData JLightAtk => jLightAtk;
    public AttackData JMediumAtk => jMediumAtk;
    public AttackData JHeavyAtk => jHeavyAtk;

    public AttackData CRLightAtk => crLightAtk;
    public AttackData CRMediumAtk => crMediumAtk;
    public AttackData CRHeavyAtk => crHeavyAtk;
    #endregion
}
