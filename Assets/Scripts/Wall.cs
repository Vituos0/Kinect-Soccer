using UnityEngine;

public class Wall : MonoBehaviour
{
    void Awake()
    {
        Ball.onCollisionWithBall += HandleBallCollision;
    }

    private void OnDestroy()
    {
        Ball.onCollisionWithBall -= HandleBallCollision;
    }

    private void HandleBallCollision(Ball ball)
    {
        Debug.Log("GOAL!");
        // xử lý score, effect, sound ở đây
    }
}
