using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private float _moveSpeed = 5f;
    private Rigidbody2D _rigidbody2D;
    private BossController _bossController;
    void Awake()
    {
        _rigidbody2D = GetComponentInParent<Rigidbody2D>();
        _bossController = GetComponent<BossController>();
    }
    public void Move()
    {
        // Kiểm tra an toàn
        if (!_bossController._detector.HasPlayerTransform() || _rigidbody2D == null) return;

        // 1. Tính toán Hướng đi (chỉ xét trục X, bỏ qua Y)
        Vector2 targetPosition = _bossController._detector.GetPlayerPosition();
        Vector2 bossPosition = _bossController._detector.GetBossPosition();
        
        // Hướng cần di chuyển (chỉ X)
        float directionX = Mathf.Sign(targetPosition.x - bossPosition.x);
        Vector2 moveDirection = new Vector2(directionX, 0).normalized;
        
        // 2. Tính toán Vận tốc mục tiêu (Target Velocity)
        float targetVelocityX = moveDirection.x * _moveSpeed;

        // 3. Làm mượt Vận tốc (Sử dụng Lerp)
        // Lưu ý: Nếu dùng FixedUpdate (như trong RunState), nên dùng Physics Time.
        float smoothFactor = 0.15f; 
        
        float smoothVelocityX = Mathf.Lerp(_rigidbody2D.linearVelocityX, targetVelocityX, smoothFactor);

        // 4. Áp dụng Vận tốc vào Rigidbody
        _rigidbody2D.linearVelocity = new Vector2(smoothVelocityX,_rigidbody2D.linearVelocityY);
        
        // 5. Lật Boss (Flip)
        Flip(-moveDirection);
        

        
    }
    public void Flip(Vector2 dir)
    {
       // Flip
        if (dir.x != 0)
        {
            float flip = dir.x > 0 ? 1 : -1;
            transform.parent.localScale = new Vector3(flip, 1, 1);
        }
    }
    public void StopMove()
    {
        _rigidbody2D.linearVelocity = new Vector2(0, _rigidbody2D.linearVelocityY);
    }

}
