using UnityEngine;

[CreateAssetMenu(fileName = "FighterData", menuName = "Scriptable Objects/FighterData")]
public class FighterData : ScriptableObject
{
    [Header("Fighter Properties")]
    [SerializeField] private float maxHealth = 1000f;
    
    [Header("Fighter Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float dashSpeed = 8f;

    #region Getter / Setter Properties
    public float MaxHealth => maxHealth;
    public float WalkSpeed => walkSpeed;
    public float DashSpeed => dashSpeed;
    public float JumpForce => jumpForce;
    #endregion
}
