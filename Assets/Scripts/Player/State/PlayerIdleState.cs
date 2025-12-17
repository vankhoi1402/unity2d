using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController controller, StateMachine<PlayerController> sm)
          : base(controller, sm)
    {
        // Không cần viết lại this.controller = controller;
        // Không cần viết lại this.sm = sm;
    }
   public override void Enter() {
        controller._database.CrossFade((int)PlayerAnimID.Idle,0.25f);
        //Debug.Log("State idle");
    

    }
   public override void Update() { }
    public override void FixUpdate() { }
    public override void Exit() { }
}
