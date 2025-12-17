using UnityEngine;
using System;

// Giả định PlayerStateID là một enum có sẵn


public class PlayerStateMachine : MonoBehaviour
{
    private PlayerController controller;
    private StateMachine<PlayerController> sm;
   

    // --- Unity Lifecycle Methods ---

    void Awake()
    {
        // 1. Lấy tham chiếu đến PlayerController. 
        controller = GetComponent<PlayerController>();

        // 2. Khởi tạo State Machine
        sm = new StateMachine<PlayerController>(controller);

        // 3. Add States và Transition Rules
        sm.AddState((int)PlayerStateID.Idle, new PlayerIdleState(controller, sm));
        sm.AddState((int)PlayerStateID.Run, new PlayerRunState(controller, sm));
        sm.AddState((int)PlayerStateID.Jump, new PlayerJumpState(controller, sm)); // Thêm State cho Jump và Attack
        sm.AddState((int)PlayerStateID.Attack, new PlayerAttackState(controller, sm));
        sm.AddState((int)PlayerStateID.Attack2, new PlayerAttack2State(controller, sm));
        sm.AddState((int)PlayerStateID.Attack3, new PlayerAttack3State(controller, sm));

        // Cấu hình trạng thái ban đầu (ví dụ: Idle)
        sm.SetInitialState((int)PlayerStateID.Idle);
    }

    void OnEnable()
    {
        // Đăng ký các hàm xử lý sự kiện
        InputEventBus.OnMove += HandleMove;
        InputEventBus.OnJump += HandleJump;
        InputEventBus.OnAttack += HandleAttack;
        Debug.Log("OnEnable");
    }

    void OnDisable()
    {
        // Hủy đăng ký các hàm xử lý sự kiện (RẤT QUAN TRỌNG)
        InputEventBus.OnMove -= HandleMove;
        InputEventBus.OnJump -= HandleJump;
        InputEventBus.OnAttack -= HandleAttack;
        Debug.Log("OnDisable");
    }

    public void Update()
    {
        // Gọi Update cho State hiện tại (đây là nơi logic của State sẽ chạy)
        sm.Update();
    }
    public void FixedUpdate()
    {
        sm.FixUpdate();
    }

    // --- Event Handlers (Logic chuyển trạng thái) ---

    private void HandleMove(float dir)
    {
        controller.SetMoveInput(dir);
        if (sm.CurrentStateID == (int)PlayerStateID.Jump) return;
        // Logic di chuyển: Nếu có input di chuyển (dir != 0) VÀ đang ở trạng thái Idle, 
        // thì chuyển sang Run. 
        if (Mathf.Abs(dir) > 0.01f && sm.CurrentStateID == (int)PlayerStateID.Idle)
        {
            sm.Transition((int)PlayerStateID.Run);
        }
        // Logic dừng: Nếu KHÔNG CÓ input di chuyển (dir == 0) VÀ đang ở trạng thái Run,
        // thì chuyển sang Idle.
        else if (Mathf.Abs(dir) <= 0.01f && sm.CurrentStateID == (int)PlayerStateID.Run)
        {
            sm.Transition((int)PlayerStateID.Idle);
        }

    }

    private void HandleJump()
    {
        // Khi nhận sự kiện Jump, chuyển sang trạng thái Jump 
        // (Bạn có thể thêm điều kiện: vd: chỉ nhảy khi đang ở Idle/Run)
        if (sm.CurrentStateID == (int)PlayerStateID.Idle || sm.CurrentStateID == (int)PlayerStateID.Run)
        {
            sm.Transition((int)PlayerStateID.Jump);
        }
    }

    private void HandleAttack()
    {
        // Nếu đang Idle/Run → vào Attack lần đầu
        if (sm.CurrentStateID == (int)PlayerStateID.Idle ||
            sm.CurrentStateID == (int)PlayerStateID.Run)
        {
            sm.Transition((int)PlayerStateID.Attack);
        }
        // 👇👇 Cần thêm Attack2, Attack3 vào đây 👇👇
        else if (sm.CurrentStateID == (int)PlayerStateID.Attack ||
                 sm.CurrentStateID == (int)PlayerStateID.Attack2 || // THÊM DÒNG NÀY
                 sm.CurrentStateID == (int)PlayerStateID.Attack3)   // VÀ DÒNG NÀY (nếu có Attack 3)
        {
            controller.SetQueueAttack(true); // Cờ để combo
        }
    }


}