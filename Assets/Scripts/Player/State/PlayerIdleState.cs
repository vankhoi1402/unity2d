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
       controller._database.Play(PlayerAnimID.Idle);
        Debug.Log("State idle");
    

    }
   public override void Update() { }
    public override void FixUpdate() { }
    public override void Exit() { }
}
