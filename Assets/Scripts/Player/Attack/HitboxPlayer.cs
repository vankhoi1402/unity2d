using System;
using System.Collections.Generic;
using UnityEngine;

public class HitboxPlayer : MonoBehaviour
{
    public event Action<IDamageable> OnHit;
    [SerializeField] private SpawnParticleAndDestroy hitVFXSpawner;
    [SerializeField] private Transform VFXSpawner;




    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[HitboxPlayer] Va chạm với: {other.gameObject.name}");

        
        var target = other.GetComponent<IDamageable>();

        if (target != null)
        {
            hitVFXSpawner.Spawn(VFXSpawner.position,VFXSpawner.rotation);
            OnHit?.Invoke(target);
        }
        
    }
    
}
