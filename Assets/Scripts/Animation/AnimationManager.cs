using UnityEngine;
using System.Collections.Generic;

public abstract class AnimationManager : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    // Map tên animation → tên clip trong Animator
    protected Dictionary<int, string> map = new Dictionary<int, string>();

    protected int currentID = -1;

    protected virtual void Awake()
    {
        //animator = GetComponentInChildren<Animator>();
        InitDatabase();
    }

    // Mỗi class con override hàm này tự build animation map
    protected abstract void InitDatabase();

    public void Play(PlayerAnimID id, bool force = false)
    {
       // Debug.Log("PLAY REQUEST: " + id);
        int key = (int)id; // chuyển enum sang int
        if (!force && currentID == key) return;

        currentID = key;
        if (!map.ContainsKey(key))
        {
           // Debug.LogError("Animation ID not found in map: " + key);
            return;
        }
       // Debug.Log("PLAYING CLIP = " + map[key]);

        animator.Play(map[key], 0);
    }


    public void CrossFade(int id, float duration = 0.1f)
    {
        if (currentID == id) return;

        currentID = id;
        animator.CrossFade(map[id], duration, 0);
    }
}
