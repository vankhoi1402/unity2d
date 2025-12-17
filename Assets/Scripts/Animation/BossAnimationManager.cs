using System.Collections.Generic;
using UnityEngine;

public enum BossAnimID
{
    Idle ,
    Run ,
    Attack,
    Jump,
    Hurt,
    Attack2,
    Attack3
}

public class BossAnimationManager : AnimationManager
{
    [SerializeField] private List<BossAnimPair> animations;

    protected override void InitDatabase()
    {
        map.Clear();
        foreach (var a in animations)
            map[(int)a.id] = a.clip;
    }
}

[System.Serializable]
public struct BossAnimPair
{
    public BossAnimID id;
    public string clip;
}
