using UnityEngine;
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(PlayerAnimationManager))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(PlayerStateMachine))]
public class PlayerController : MonoBehaviour
{
    public  PlayerAnimationManager _database { get; private set; }
    public Movement _movement { get; private set; }
    public PlayerStateMachine _stateMachine { get; private set; }
    
public float MoveInput { get; private set; } = 0f; // Giữ nguyên bảo vệ

// Phương thức công khai để cập nhật giá trị

    private void Awake()
    {
        // Dependency Injection “thủ công” – cực gọn, không cần framework
        _database = GetComponent<PlayerAnimationManager>();
        _movement = GetComponent<Movement>();
        _stateMachine= GetComponent<PlayerStateMachine>();    
    }
    private void Update()
    {
        _stateMachine.Update();
    }
    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }
    public void SetMoveInput(float value)
    {
        MoveInput = value;
    }


}
