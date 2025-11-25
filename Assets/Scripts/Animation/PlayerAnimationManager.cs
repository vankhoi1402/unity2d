using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimID
{
    Idle ,
    Run ,
    Attack,
    Jump
}

public class PlayerAnimationManager : AnimationManager
{
    [SerializeField] private List<PlayerAnimPair> animations;

    protected override void InitDatabase()
    {
        map.Clear();
        foreach (var a in animations)
            map[(int)a.id] = a.clip;
    }
}

[System.Serializable]
public struct PlayerAnimPair
{
    public PlayerAnimID id;
    public string clip;
}
