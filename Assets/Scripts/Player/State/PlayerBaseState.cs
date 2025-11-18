// Triển khai IState và chứa các tham chiếu Context/Owner
public abstract class PlayerBaseState : IState
{
    protected PlayerController controller;
    protected StateMachine<PlayerController> sm;

    protected PlayerBaseState(PlayerController controller, StateMachine<PlayerController> sm)
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