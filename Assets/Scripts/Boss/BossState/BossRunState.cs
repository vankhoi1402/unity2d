using UnityEngine;

public class BossRunState : BossBaseState
{
    // Giả định: Boss sẽ không đuổi quá lâu mà không nghỉ
    
   

    public BossRunState(BossController controller, StateMachine<BossController> sm)
        : base(controller, sm)
    {
    }

    public override void Enter()
    {
        //Debug.Log("<color=blue>RUN State:</color> Bắt đầu truy đuổi.");
        controller._bossAim.CrossFade((int)BossAnimID.Run); // Chơi animation Run
        
    }

    public override void Update()
    {
        
    }

    public override void FixUpdate()
    {
        
        // HÀNH ĐỘNG CƠ BẢN: Di chuyển Boss về phía Player
        controller._movement.Move();
    }

    public override void Exit()
    {
        //Debug.Log("<color=blue>RUN State:</color> Kết thúc truy đuổi.");
        controller._movement.StopMove();
    }
}