using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    // Tham chiếu [SerializeField] để kéo Component PlayerAttack vào Inspector
    [SerializeField] private PlayerAttack playerAttackTarget;

    // --- Hàm lắng nghe Animation Events ---

    // 1. Hàm này được gọi bởi Animation Event
    public void ReceiveEnableHitboxCall()
    {
        // 🟢 DEBUG 1: Kiểm tra xem Animator có gọi hàm này không
        //Debug.Log("[RECEIVER LOG] ReceiveEnableHitboxCall was triggered by Animator.");

        if (playerAttackTarget != null)
        {
            // 2. Chuyển tiếp lời gọi đến PlayerAttack đích
            playerAttackTarget.EnableHitbox();
        }
        else
        {
            // 🔴 DEBUG LỖI: Cảnh báo nếu tham chiếu bị thiếu
            Debug.LogError("[RECEIVER ERROR] playerAttackTarget is NULL. Please assign PlayerAttack component in the Inspector.");
        }
    }

    // 1. Hàm này được gọi bởi Animation Event
    public void ReceiveDisableHitboxCall()
    {
        if (playerAttackTarget != null)
        {
            // 2. Chuyển tiếp lời gọi đến PlayerAttack đích
            playerAttackTarget.DisableHitbox();
        }
    }
}