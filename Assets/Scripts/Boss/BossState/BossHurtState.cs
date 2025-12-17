using UnityEngine;

public class BossHurtState : BossBaseState
{
    public BossHurtState(BossController controller, StateMachine<BossController> sm)
          : base(controller, sm)
    {
        // Không cần viết lại this.controller = controller;
        // Không cần viết lại this.sm = sm;
    }
    public override void Enter()
    {


    }
    public override void Update() { }
    public override void FixUpdate() { }
    public override void Exit() { }
}
