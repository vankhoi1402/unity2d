using UnityEngine;

public abstract class BossBaseState : IState
{
    
    protected BossController controller;
    protected StateMachine<BossController> sm;

    protected BossBaseState(BossController controller, StateMachine<BossController> sm)
    {
        this.controller = controller;
        this.sm = sm;
    }

    // Bắt buộc các lớp con phải ghi đè các phương thức này
    public abstract void Enter();
    public abstract void Update();
    public abstract void FixUpdate();
    public abstract void Exit();
}
