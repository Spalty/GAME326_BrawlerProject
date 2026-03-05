using UnityEngine;

[CreateAssetMenu(fileName = "FighterData", menuName = "Scriptable Objects/FighterData")]
public class FighterData : ScriptableObject
{
    [Header("Fighter Properties")]
    [SerializeField] private float maxHealth = 1000f;
    
    [Header("Fighter Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float dashSpeed = 8f;
    [SerializeField] private float verticalJumpForce = 10f;
    [SerializeField] private float horizontalJumpForce = 5f;
    [SerializeField] private int maxJumpCount = 2;

    #region Getter / Setter Properties
    public float MaxHealth => maxHealth;
    public float WalkSpeed => walkSpeed;
    public float DashSpeed => dashSpeed;
    public float VerticalJumpForce => verticalJumpForce;
    public float HorizontalJumpForce => horizontalJumpForce;
    public int MaxJumpCount => maxJumpCount;
    #endregion
}
