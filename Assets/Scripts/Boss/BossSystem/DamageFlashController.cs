using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageFlashController : MonoBehaviour
{
    // --- Cấu Hình ---
    [Tooltip("Thời gian hiệu ứng nháy sáng kéo dài (giống Hollow Knight thì rất ngắn).")]
    public float flashDuration = 0.1f;

    [Tooltip("Cường độ flash (giá trị Float gửi lên Shader). Thường là 1.")]
    public float flashIntensity = 1.0f;

    // --- Biến Nội Bộ ---
    // Tên thuộc tính Float trong Shader Graph của bạn (RẤT QUAN TRỌNG: PHẢI KHỚP TÊN)
    private const string FLASH_AMOUNT_NAME = "_FloatAmount";

    // ID của thuộc tính (để tối ưu hiệu suất, chỉ cần tính 1 lần)
    private int flashAmountPropertyID;

    private Material spriteMaterial;
    [SerializeField]private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // 1. Lấy tham chiếu đến SpriteRenderer
        //spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("DamageFlashController: Không tìm thấy SpriteRenderer trên GameObject này.");
            return;
        }

        // 2. Lấy Material Instance (quan trọng để không ảnh hưởng đến các Sprite khác)
        // Lưu ý: dùng .material để tạo một bản sao Material riêng cho đối tượng này.
        spriteMaterial = spriteRenderer.material;
        // KIỂM TRA 2: Đảm bảo Material tồn tại
        if (spriteMaterial == null)
        {
            Debug.LogError("DamageFlashController: Không có Material được gán cho SpriteRenderer.", this);
            return; // Dừng nếu Material bị thiếu
        }
        else
        {
            Debug.LogError("DamageFlashController: có Material được gán cho SpriteRenderer.", this);
        }

        // 3. Chuyển tên thuộc tính thành ID (Tối ưu)
        flashAmountPropertyID = Shader.PropertyToID(FLASH_AMOUNT_NAME);

        // Đảm bảo Flash Amount ban đầu là 0
        spriteMaterial.SetFloat(flashAmountPropertyID, 0f);
    }

    /// <summary>
    /// Hàm này được gọi khi nhân vật nhận sát thương (từ script HP của Boss/Player).
    /// </summary>
    public void TriggerDamageFlash()
    {
        // Ngăn Coroutine cũ đang chạy dở để tránh nhấp nháy không đúng
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine());
    }

    /// <summary>
    /// Coroutine xử lý quá trình nháy sáng (Bật -> Chờ -> Tắt).
    /// </summary>
    IEnumerator FlashCoroutine()
    {
        // --- BƯỚC 1: BẬT FLASH ---
        // Gửi giá trị 1.0 (hoặc flashIntensity) lên biến _FlashAmount trong Shader
        spriteMaterial.SetFloat(flashAmountPropertyID, flashIntensity);

        // --- BƯỚC 2: CHỜ ---
        yield return new WaitForSeconds(flashDuration);

        // --- BƯỚC 3: TẮT FLASH ---
        // Gửi giá trị 0.0 lên biến _FlashAmount trong Shader
        spriteMaterial.SetFloat(flashAmountPropertyID, 0f);
    }
}