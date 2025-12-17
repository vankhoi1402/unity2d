// File: IDamageable.cs
using UnityEngine;

// Interface này xác định rằng bất kỳ lớp nào triển khai nó đều có khả năng nhận sát thương.
public interface IDamageable
{
    /// <summary>
    /// Áp dụng sát thương lên đối tượng.
    /// Hàm này được gọi bởi kẻ tấn công (Attacker).
    /// </summary>
    /// <param name="damageAmount">Lượng sát thương cơ bản được tính từ kẻ tấn công.</param>
    void TakeDamage(float damageAmount);

    // Tùy chọn: Thêm các thuộc tính hoặc hàm khác nếu cần, ví dụ:
    // float CurrentHealth { get; }
    // bool IsVulnerable { get; }
}