using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerAttack2State : PlayerBaseState
{
    //private bool canCombo = false;
    private float duration = 0.6f;
    // private float timeCombo = 0.25f;
    private float timeAttack;

    public PlayerAttack2State(PlayerController controller, StateMachine<PlayerController> sm)
         : base(controller, sm)
    {
        // Không cần viết lại this.controller = controller;
        // Không cần viết lại this.sm = sm;
    }
    public override void Enter()
    {

        controller.SetQueueAttack(false);


        timeAttack = 0f;
        controller._database.CrossFade((int)PlayerAnimID.Attack2,0.3f);
        controller._playerAttack.EnableHitbox();

    }

    public override void Update()
    {
        timeAttack += Time.deltaTime;




        if (timeAttack >= duration)
        {
            if (controller.GetQueueAttack())
            {

                controller.SetQueueAttack(false);
                sm.Transition((int)PlayerStateID.Attack3);
            }
            else
            {

                sm.Transition((int)PlayerStateID.Idle);
            }
        }
    }
    public override void FixUpdate() { }
    public override void Exit()
    {
        controller._playerAttack.DisableHitbox();

    }
}
