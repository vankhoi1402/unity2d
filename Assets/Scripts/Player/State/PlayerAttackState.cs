using System.Collections;
using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
   // private bool canCombo = false;
    private float duration = 1f;
   // private float timeCombo = 0.25f;
    private float timeAttack;

    
    public PlayerAttackState(PlayerController controller, StateMachine<PlayerController> sm)
          : base(controller, sm)
    {
        // Không cần viết lại this.controller = controller;
        // Không cần viết lại this.sm = sm;
    }
    public override void Enter()
    {
        timeAttack = 0f;                      // 🟢 RESET TIMER
        controller.SetQueueAttack(false);  

        controller._database.CrossFade((int)PlayerAnimID.Attack);
        controller._playerAttack.SetAttack1();
        
        //Debug.Log("Attack State");
        
        

    }
   
    
    public override void Update() {
        timeAttack += Time.deltaTime;
        if (timeAttack >= duration)
        {
           
            if (controller.GetQueueAttack())
            {
                controller.SetQueueAttack(false);
                sm.Transition((int)PlayerStateID.Attack2);  // Combo next hit
            }
            else
            {
                sm.Transition((int)PlayerStateID.Idle);    // Kết thúc combo
            }
        }

    }
    public override void FixUpdate() { }
    public override void Exit() {
        controller._playerAttack.DisableHitbox();
      
    }
}
