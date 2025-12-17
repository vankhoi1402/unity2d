using UnityEngine;

public class BossIdleState : BossBaseState
{
    private float IdleTime = 1f;
    private float TimeIdle;

    public BossIdleState(BossController controller, StateMachine<BossController> sm)
        : base(controller, sm)
    {
    }

    public override void Enter()
    {
       // Debug.Log("<color=cyan>IDLE State:</color> Bắt đầu đứng yên.");
        controller._bossAim.CrossFade((int)BossAnimID.Idle); // Chơi animation Idle
        TimeIdle = 0;
        controller.IsTransitionLocked = true;

    }

    public override void Update()
    {
        TimeIdle += Time.deltaTime;
        if (TimeIdle > IdleTime) {
            controller.IsTransitionLocked = false;
            sm.Transition((int)BossStateID.Run);
        
        }
    }

    public override void FixUpdate() { }
    
    public override void Exit()
    {
        //Debug.Log("<color=cyan>IDLE State:</color> Thoát trạng thái Idle.");
    }
}