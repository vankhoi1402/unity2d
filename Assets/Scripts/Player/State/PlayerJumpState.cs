using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerController controller, StateMachine<PlayerController> sm)
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
