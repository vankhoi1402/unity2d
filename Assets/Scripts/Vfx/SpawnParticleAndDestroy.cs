using UnityEngine;

public class SpawnParticleAndDestroy : MonoBehaviour
{
    [Header("Particle Prefab")]
    [SerializeField] private ParticleSystem particlePrefab;

    /// <summary>
    /// Spawn particle tại vị trí chỉ định và tự hủy sau khi chạy xong
    /// </summary>
    public void Spawn(Vector3 position, Quaternion rotation)
    {
        if (particlePrefab == null)
        {
            Debug.LogWarning("Particle Prefab is NULL!");
            return;
        }

        // Sinh particle
        ParticleSystem ps = Instantiate(particlePrefab, position, rotation);

        // Tính thời gian sống tối đa
        float lifeTime = ps.main.duration + ps.main.startLifetime.constantMax;

        // Tự hủy GameObject
        Destroy(ps.gameObject, lifeTime);
    }
}
