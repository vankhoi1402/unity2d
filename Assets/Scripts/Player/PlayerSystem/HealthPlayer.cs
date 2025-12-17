using UnityEngine;
using System;

// Đảm bảo bạn đã tạo file IDamageable.cs
public class HealthPlayer : MonoBehaviour, IDamageable
{
    // --- Events Cục bộ ---
    // Thông báo cho UI (float currentHealth, float maxHealth)
    public event Action<float, float> OnHealthChanged;
    public event Action OnTakeDamage; // Thông báo cho VFX, State Machine
    public event Action OnDeath;

    [Header("Chỉ số Player")]
    [SerializeField] private float maxHealth = 1000f;
    [SerializeField] private float defenseRating = 50f; // Khả năng phòng thủ
    [SerializeField] private float baseAttackPower = 100f; // Chỉ số công cơ bản

    private float currentHealth;
    public bool IsAlive => currentHealth > 0;

    void Awake()
    {
        currentHealth = maxHealth;
        // Thông báo trạng thái ban đầu khi game bắt đầu
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    // --- Phương thức Bắt buộc của IDamageable ---
    public void TakeDamage(float incomingDamage)
    {
        if (!IsAlive) return;

        // 1. Tính toán sát thương thực nhận (sau khi trừ giáp/phòng thủ)
        // Sát thương thực nhận luôn >= 1 để đảm bảo luôn nhận sát thương
        float damageTaken = Mathf.Max(1f, incomingDamage - defenseRating);

        currentHealth -= damageTaken;
        currentHealth = Mathf.Max(0, currentHealth); // Đảm bảo máu không âm

        Debug.Log($"Player bị tấn công: Nhận {damageTaken} sát thương thực.");

        // 2. PHÁT EVENTS Cục bộ
        OnTakeDamage?.Invoke(); // Kích hoạt State Hurt, VFX, SFX
        OnHealthChanged?.Invoke(currentHealth, maxHealth); // Cập nhật thanh máu UI

        // 3. Kiểm tra chết
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Debug.Log("Player đã bị đánh bại!");
        // Thêm logic Game Over, hủy vật lý, v.v.
    }

    // --- Getters cho các Component khác ---
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetBaseAttackPower()
    {
        // Lớp PlayerAttack sẽ gọi hàm này để lấy chỉ số công
        return baseAttackPower;
    }
}