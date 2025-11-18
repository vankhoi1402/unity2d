using UnityEngine;

public class Movement : MonoBehaviour
{
	private Rigidbody2D rb;
	private float speed = 5f;
    private int facingDirection = 1;

    private void Awake()
	{
		rb = GetComponentInParent<Rigidbody2D>();
	}
	private void OnEnable()
	{

	}
	private void OnDisable()
	{
	}

	public void Move(float dir)
	{
		rb.linearVelocity = new Vector2(dir*speed,rb.linearVelocity.y);
        if (dir != 0) // Chỉ lật khi nhân vật thực sự di chuyển
        {
            Flip(dir);
        }

    }
    public void Flip(float dir)
    {
        // Kiểm tra xem hướng di chuyển có ngược với hướng hiện tại không
        if (dir > 0 && facingDirection == -1 || dir < 0 && facingDirection == 1)
        {
            // Đảo ngược hướng đối diện
            facingDirection *= -1;

            // Lấy scale cục bộ hiện tại
            Vector3 currentScale = transform.parent.localScale;

            // Đảo ngược trục X của scale để lật đối tượng
            currentScale.x *= -1;

            // Áp dụng scale mới
            transform.parent.localScale = currentScale;
        }
    }
    public void Stop()
	{
		rb.linearVelocity = Vector2.zero;
	}
}
