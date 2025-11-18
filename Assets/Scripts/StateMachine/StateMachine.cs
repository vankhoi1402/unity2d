using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : MonoBehaviour
{
    public IState currentState;
    private Dictionary<int, IState> states;
    public T owner;

    // Thuộc tính mới: Lưu trữ ID của trạng thái hiện tại
    public int CurrentStateID { get; private set; } = -1;

    public StateMachine(T owner)
    {
        this.owner = owner;
        states = new Dictionary<int, IState>();
    }

    public void AddState(int id, IState state)
    {
        states[id] = state;
    }

    // Phương thức mới: Thiết lập trạng thái ban đầu
    public void SetInitialState(int id)
    {
        if (states.TryGetValue(id, out IState initialState))
        {
            currentState = initialState;
            CurrentStateID = id;
            currentState.Enter();
        }
        else
        {
            Debug.LogError($"State with ID {id} not found for initial state.");
        }
    }

    // Đổi tên từ SwitchState thành Transition (đồng bộ với PlayerStateMachine)
    public void Transition(int id)
    {
        // 1. Kiểm tra trạng thái kế tiếp có tồn tại không
        if (!states.TryGetValue(id, out IState next))
        {
            Debug.LogError($"State with ID {id} not found.");
            return;
        }

        // 2. Không chuyển nếu trạng thái không thay đổi
        if (next == currentState) return;

        // 3. Thoát khỏi trạng thái hiện tại (nếu có)
        currentState?.Exit();

        // 4. Cập nhật trạng thái và ID
        currentState = next;
        CurrentStateID = id;

        // 5. Vào trạng thái mới
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }
    public void FixUpdate()
    {
        currentState?.FixUpdate();
    }
}