using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerAttack3State : PlayerBaseState
{
     private float duration = 0.6f;
   // private float timeCombo = 0.25f;
    private float timeAttack;
    
    public PlayerAttack3State(PlayerController controller, StateMachine<PlayerController> sm)
         : base(controller, sm)
    {
        // Không cần viết lại this.controller = controller;
        // Không cần viết lại this.sm = sm;
    }
    public override void Enter()
    {
        timeAttack = 0f;                      // 🟢 RESET TIMER
        controller.SetQueueAttack(false);  
        controller._database.CrossFade((int)PlayerAnimID.Attack3);
        
        //Debug.Log("Attack State 3");
      
        

    }
   
   
    public override void Update()
    {
        timeAttack += Time.deltaTime;
        if (timeAttack >= duration)
        {
            
                sm.Transition((int)PlayerStateID.Idle);    // Kết thúc combo
            
        }

       
    }
    public override void FixUpdate() { }
    public override void Exit()
    {
        controller._playerAttack.DisableHitbox();

    }
}
