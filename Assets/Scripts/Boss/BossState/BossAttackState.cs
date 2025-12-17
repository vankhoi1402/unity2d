using UnityEngine;

public class BossAttackState : BossBaseState
{
    private float _attackCooldownTimer;
    private const float ATTACK_COOLDOWN = 1.5f; // Tấn công mỗi 1.5 giây

    public BossAttackState(BossController controller, StateMachine<BossController> sm)
        : base(controller, sm)
    {
    }

    public override void Enter()
    {
       // Debug.Log("<color=red>ATTACK State:</color> Bắt đầu tấn công.");
        controller._bossAim.CrossFade((int)BossAnimID.Attack); // Chơi animation Attack
        _attackCooldownTimer = 0f;
        controller._attack.EnableHitbox();
        controller.IsTransitionLocked = true;
    }

    public override void Update()
    {
        _attackCooldownTimer += Time.deltaTime;
        if (_attackCooldownTimer > ATTACK_COOLDOWN)
        {
            controller.IsTransitionLocked = false;
            sm.Transition((int)BossStateID.Idle);  
        }
            
    }

    public override void FixUpdate() { }

    public override void Exit()
    {
        //Debug.Log("<color=red>ATTACK State:</color> Kết thúc tấn công.");
        controller._attack.DisableHitbox();
    }
}