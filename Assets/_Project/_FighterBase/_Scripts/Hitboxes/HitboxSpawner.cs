using UnityEngine;
using Brawler.Combat;

public class HitboxSpawner : MonoBehaviour
{
    public GameObject hitboxPrefab; // Assign in inspector
    private AttackData currentAttackData; // Assign in inspector
    public FighterController owner; // Assign the fighter that spawns this hitbox
    

    public void SpawnHitbox()
    {
        if (hitboxPrefab == null || currentAttackData == null || owner == null)
        {
            Debug.LogError("HitboxSpawner is missing references.");
            return;
        }

        // Instantiate the hitbox prefab
        GameObject hitboxObj = Instantiate(hitboxPrefab, transform.position + (Vector3)currentAttackData.hitboxOffset, Quaternion.identity);
        
        Hitbox hitbox = hitboxObj.GetComponent<Hitbox>();
        
        //hitbox.Data = currentAttackData; // Assign the data asset
    } 

    public void DestoyHitbox(GameObject hitboxObj)
    {
        Destroy(hitboxObj);
    }      
}
