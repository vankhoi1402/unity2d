using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private float enterTime;
    public PlayerJumpState(PlayerController controller, StateMachine<PlayerController> sm)
        : base(controller, sm)
    { }

    public override void Enter()
    {
        // Bật animation
        controller._database.Play((int)PlayerAnimID.Jump);

        // Thực hiện nhảy
        controller._movement.Jump();
        Debug.Log(controller._movement.IsGrounded());
        enterTime = Time.time;



        // Debug
       // Debug.Log("➡ Enter Jump");
    }

    public override void Update()
    {
        
        controller._movement.Move(controller.MoveInput);
        if (Time.time - enterTime < 0.1f)
            return;
        if (controller._movement.IsGrounded())
        {
            Debug.Log(controller._movement.IsGrounded());
            if (Mathf.Abs(controller.MoveInput) > 0.01f)
                sm.Transition((int)PlayerStateID.Run);
            else
                sm.Transition((int)PlayerStateID.Idle);
        }
    }

    public override void FixUpdate()
    {
        // KHÔNG ĐƯỢC nhảy trong FixedUpdate.
        // FixUpdate chỉ dùng để áp dụng Move vật lý.
//Debug.Log("ground: " + controller._movement.IsGrounded());
    }

    public override void Exit()
    {
        
       // Debug.Log("⬅ Exit Jump");
        
    }
}
