using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerController controller, StateMachine<PlayerController> sm)
         : base(controller, sm)
    {
        // Không cần viết lại this.controller = controller;
        // Không cần viết lại this.sm = sm;
    }
    public override void Enter()
    {
        controller._database.Play(PlayerAnimID.Run);
        Debug.Log("Run " );


    }
    public override void Update() { }
    public override void FixUpdate() {
        controller._movement.Move(controller.MoveInput);
        Debug.Log("soso");
    
    }
    public override void Exit()
    {
        controller._movement.Stop();
    }
}
