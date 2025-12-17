using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private GameObject hitbox;   // Drag AttackHitbox vào đây



    // Animation Event gọi
    public void EnableHitbox()
    {
        hitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        hitbox.SetActive(false);
    }
}
