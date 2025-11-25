using UnityEngine;
using UnityEngine.InputSystem; // Để hỗ trợ InputAction nếu cần

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 5f;
    private float jumpForce = 7f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 2f;
    [SerializeField] private LayerMask groundLayer;
    private int facingDirection = 1;

    
    

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        
    }

    
    public void Move(float dir)
    { 
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocityY);
        this.Flip(dir);
    }

    public void Jump()
    {
        if (this.IsGrounded())
        {
         rb.AddForce(new Vector3(0,jumpForce,0),ForceMode2D.Impulse);
        }
        
    }

    /// <summary>
    /// Variable Height: Thả nút -> nhảy thấp (dodge linh hoạt)
    /// Gọi từ PlayerInput Jump Canceled
    /// </summary>
   
    public bool IsGrounded()
    {
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        
        return grounded;
    }

    public void Flip(float dir)
    {
        if (dir > 0 && facingDirection == -1 || dir < 0 && facingDirection == 1)
        {
            facingDirection *= -1;
            Vector3 currentScale = transform.parent.localScale;
            currentScale.x *= -1;
            transform.parent.localScale = currentScale;
        }
    }

    public void Stop()
    {
        rb.linearVelocity = Vector2.zero;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}