using System;
using UnityEngine;

public static class InputEventBus
{
    public static Action<float> OnMove;
    public static Action OnJump;
    public static Action OnAttack;

    public static void Move(float dir) => OnMove?.Invoke(dir);
    public static void Jump() => OnJump?.Invoke();
    public static void Attack() => OnAttack?.Invoke();
}

