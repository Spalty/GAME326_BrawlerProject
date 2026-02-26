using UnityEngine;

[CreateAssetMenu(fileName = "FighterData", menuName = "Scriptable Objects/FighterData")]
public class FighterData : ScriptableObject
{
    [Header("Fighter Properties")]
    public float maxHealth = 1000f;
    
    [Header("Fighter Movement")]
    public float walkSpeed = 5f;
    public float jumpForce = 10f;
    public float dashSpeed = 8f;

}
