using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    private BossController _bossController;
    private StateMachine<BossController> sm;
   

    // --- Unity Lifecycle Methods ---

    void Awake()
    {
        // 1. Lấy tham chiếu đến PlayerController. 
        _bossController = GetComponent<BossController>();

        // 2. Khởi tạo State Machine
        sm = new StateMachine<BossController>(_bossController);

        // 3. Add States và Transition Rules
        sm.AddState((int)BossStateID.Idle, new BossIdleState(_bossController, sm));
        sm.AddState((int)BossStateID.Run, new BossRunState(_bossController, sm));
        sm.AddState((int)BossStateID.Jump, new BossJumpState(_bossController, sm)); // Thêm State cho Jump và Attack
        sm.AddState((int)BossStateID.Attack, new BossAttackState(_bossController, sm));
        
        // Cấu hình trạng thái ban đầu (ví dụ: Idle)
        sm.SetInitialState((int)BossStateID.Idle);
    }
    public void Update()
    {
        this.CheckGlobalTransitions();
        sm.Update();
    }
    public void FixedUpdate()
    {
        sm.FixUpdate();
    }
    private void CheckGlobalTransitions()
    {
        // 1. Lấy dữ liệu phát hiện
        bool playerSeen = _bossController._detector.GetIsLookingAtPlayer();
        float distance = _bossController._detector._distanceToPlayer; // Giả định bạn thêm biến này vào BossLook

        int currentStateID = sm.GetCurrentStateID();
        bool canAttack = distance < _bossController.AttackRange;

        // 2. RULES (Luật chuyển đổi)
        // 🥇 ƯU TIÊN TUYỆT ĐỐI: KHÓA CHUYỂN ĐỔI
        if (_bossController.IsTransitionLocked)
        {
            // Nếu Boss đang chạy animation không thể ngắt quãng, thì KHÔNG LÀM GÌ CẢ.
            return;
        }
        // RULE 1: Nếu thấy Player (Bất kể đang ở trạng thái nào khác Idle/Run/Attack)
        if (playerSeen && currentStateID != (int)BossStateID.Attack)
        {
            // Nếu đủ gần tầm đánh, chuyển sang Attack
            if (distance <= _bossController.AttackRange)
            {
                sm.Transition((int)BossStateID.Attack);
            }
           
            // Nếu xa tầm đánh, chuyển sang Run/Chase
            else if (currentStateID != (int)BossStateID.Run)
            {
                sm.Transition((int)BossStateID.Run);
            }
            return;
        }

        // RULE 2: Nếu mất dấu Player (Đang ở trạng thái chiến đấu)
        if (!canAttack && (currentStateID == (int)BossStateID.Attack || currentStateID == (int)BossStateID.Run))
        {
            sm.Transition((int)BossStateID.Idle);
            return;
        }

        // RULE 3: Chuyển từ Run sang Attack (Nếu đang đuổi mà vào tầm đánh)
        if (currentStateID == (int)BossStateID.Run && playerSeen && distance <= _bossController.AttackRange)
        {
            sm.Transition((int)BossStateID.Attack);
            return;
        }
    }


}
