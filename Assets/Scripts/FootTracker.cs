using UnityEngine;

public class FootTracker : MonoBehaviour
{
    public int playerIndex = 0; // Người chơi đầu tiên
    public int jointType = 18;  // Khớp AnkleRight (Cổ chân phải)  
    private Rigidbody rb;
    public long UserId { get; private set; }   

    void Start()
    {
       // Lấy Rigidbody để xử lý va chạm vật lý 
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Chân di chuyển theo Kinect, không bị vật lý đẩy ngược lại
        }
    }

    void FixedUpdate()
    {
        KinectManager manager = KinectManager.Instance;
        if (manager && manager.IsInitialized())
        {
            long userId = manager.GetUserIdByIndex(playerIndex);
            if (userId != 0) 
            {
               // Lấy vị trí cổ chân phải từ Kinect  
                Vector3 footPos = manager.GetJointPosition(userId, jointType);

                // Di chuyển Rigidbody để tạo lực sút vào bóng
                if (rb != null)
                {
                    rb.MovePosition(footPos);
                }
                else
                {
                    transform.position = footPos;
                }
            }
        }
    }
}