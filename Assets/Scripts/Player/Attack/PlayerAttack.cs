using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour
{
    // Cần Component HealthPlayer (thường nằm trên GameObject cha) để lấy chỉ số công
    private HealthPlayer playerHealth;

    // GameObject chứa Component HitboxPlayer (được thiết lập trong Inspector)
    // Đây là Hitbox thực tế đại diện cho vùng tấn công
    [SerializeField] private GameObject hitboxObject;

    // Tham chiếu đến Component HitboxPlayer (để đăng ký Event)
    private HitboxPlayer hitboxComponent;

    // Biến lưu trữ hệ số sát thương hiện tại
    private float currentDamageMultiplier = 1.0f;

    [Header("Hệ số Sát thương Combo")]
    [SerializeField] private float attack1Multiplier = 1.0f;
    [SerializeField] private float attack2Multiplier = 1.3f;
    [SerializeField] private float attack3Multiplier = 1.7f;

    void Awake()
    {
        // Thường HealthPlayer nằm trên đối tượng gốc (Parent)
        playerHealth = GetComponentInParent<HealthPlayer>();

        if (hitboxObject != null)
        {
            // Lấy Component HitboxPlayer từ GameObject đã Drag
            hitboxComponent = hitboxObject.GetComponent<HitboxPlayer>();

            // Đảm bảo Hitbox bắt đầu ở trạng thái tắt
            hitboxObject.SetActive(false);
        }

        if (playerHealth == null)
        {
            Debug.LogError("PlayerAttack không tìm thấy HealthPlayer Component.");
        }
    }

    // --- CÁC HÀM SET DAMAGE (Được gọi từ State Machine) ---
    // Các hàm này thiết lập hệ số sát thương cho đòn đánh sắp tới.

    public void SetAttack1() { currentDamageMultiplier = attack1Multiplier; }
    public void SetAttack2() { currentDamageMultiplier = attack2Multiplier; }
    public void SetAttack3() { currentDamageMultiplier = attack3Multiplier; }

    // --- CÁC HÀM QUẢN LÝ HITBOX (Được gọi từ AnimationEventReceiver) ---
    // Đây là hàm được Animation Event gọi gián tiếp.

    public void EnableHitbox()
    {
        if (hitboxComponent != null && playerHealth != null)
        {

            hitboxObject.SetActive(true);
            hitboxComponent.OnHit += HandleHitOnTarget;

        }
    }

    public void DisableHitbox()
    {
        if (hitboxComponent != null)
        {
            // 1. HỦY ĐĂNG KÝ: Ngừng lắng nghe để chuẩn bị cho đòn đánh tiếp theo
            hitboxComponent.OnHit -= HandleHitOnTarget;

            // 2. Vô hiệu hóa Collider
            
            hitboxObject.SetActive(false);
        }
    }

    // --- LOGIC XỬ LÝ GÂY SÁT THƯƠNG (Listener cho Hitbox Event) ---

    private void HandleHitOnTarget(IDamageable target)
    {
        // 2. Lấy chỉ số công cơ bản
        float baseDamage = playerHealth.GetBaseAttackPower();

        // 3. Tính toán SÁT THƯƠNG CUỐI CÙNG
        float finalDamage = baseDamage * currentDamageMultiplier;

        // 4. GỌI HÀM TRỪ MÁU TRÊN MỤC TIÊU (Sử dụng tính đa hình của Interface)
        target.TakeDamage(finalDamage);

        Debug.Log($"Player gây {finalDamage} sát thương (Hệ số x{currentDamageMultiplier})!");
    }
}