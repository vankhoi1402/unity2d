using UnityEngine;

public class BossLook : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _bossTransform;
    [SerializeField] private float _lookDistance = 10f;
    public float _distanceToPlayer { get; private set; } 
    
    private bool _isLookingAtPlayer = false;

    // Hàm này sẽ được gọi mỗi khung hình để đảm bảo Boss liên tục kiểm tra
    void Update()
    {
        // 1. Liên tục gọi hàm kiểm tra
        CheckPlayerDistance(); 
    }

    void CheckPlayerDistance()
    {
        if (_playerTransform == null || _bossTransform == null)
        {
            // Tránh lỗi nếu chưa gán Transform
            _isLookingAtPlayer = false;
            return;
        }
        
        // SỬ DỤNG Vector3.Distance để tính khoảng cách 3D chính xác hơn
         _distanceToPlayer = Vector3.Distance(_bossTransform.position, _playerTransform.position);
        
        // 2. Cập nhật trạng thái
        if (_distanceToPlayer <= _lookDistance)
        {
            // Debug Log: Boss thấy Player trong tầm!
          //  Debug.Log($"<color=green>Boss: Đã phát hiện Player ở {_distanceToPlayer:F2}m (Tầm: {_lookDistance}m)</color>");
            _isLookingAtPlayer = true;
        }
        else
        {
            if (_isLookingAtPlayer)
            {
                 // Chỉ log khi Player vừa rời khỏi tầm nhìn
                // Debug.Log($"<color=yellow>Boss: Mất dấu Player, khoảng cách hiện tại: {_distanceToPlayer:F2}m</color>");
            }
            _isLookingAtPlayer = false;
        }
    }

    // Hàm public để các script khác có thể đọc trạng thái
    public bool GetIsLookingAtPlayer()
    {
        return _isLookingAtPlayer;
    }
    public float GetDistanceToPlayer()
    {
        return _distanceToPlayer;
    }
    public  Vector3 GetPlayerPosition()
    {
        return _playerTransform.position;
    }
    public Vector3 GetBossPosition()
    {
        return _bossTransform.position;
    }
    public bool HasPlayerTransform()
    {
        return _playerTransform != null;
    }
}