using UnityEngine;

[RequireComponent(typeof(BossAnimationManager))]
[RequireComponent(typeof(BossStateMachine))]
public class BossController : MonoBehaviour
{
    public BossAnimationManager _bossAim { get; private set; }
    public BossStateMachine _stateMachine { get; private set;  }
    public BossMovement _movement { get; private set; }
    public BossLook _detector { get; private set; }

    public BossAttack _attack { get; private set; }
    public DamageFlashController _dameFlash { get; private set; }
    // Trong BossController.cs

    public bool IsTransitionLocked { get; set; } = false; // Cờ khóa chuyển đổi
    public float AttackRange = 3.0f;
    void Awake()
    {
        // Dependency Injection “thủ công” – cực gọn, không cần framework
        _bossAim = GetComponent<BossAnimationManager>();
        _stateMachine= GetComponent<BossStateMachine>();  
        _movement = GetComponent<BossMovement>();
        _detector = GetComponent<BossLook>();
        _attack=GetComponent<BossAttack>();
        _dameFlash = GetComponent<DamageFlashController>();
    }
    void Update()
    {
        _stateMachine.Update();
    }
    void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

}
