using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class ScoreBox : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private GameObject hitEffect;
    int scoreBall;
    Text scoreText;


    [Header("Actions")]
    public static Action onHitScoreBoxSound;

    private void Awake()
    {
        scoreText = GetComponentInChildren<Text>();

        Ball.onCollisionWithball += OnHandleEvent;

    }
    private void OnDestroy()
    {
        Ball.onCollisionWithball -= OnHandleEvent;
    }
    public void Init(int score)
    {
        scoreBall = score;
        scoreText.text = scoreBall.ToString();
    }

    private void OnHandleEvent(Ball ball, Collision target)
    {
        if (target.gameObject == gameObject)
         {
            if (ball.lastPlayerTouchball != -1)
            {
                onHitScoreBoxSound?.Invoke();  
               //SoundManager.Instance.PlayHitScore();
               GameManager.Instance.AddScore(ball.lastPlayerTouchball, scoreBall);
                StartCoroutine(DisappearRoutine());

                Debug.Log("effectttttttttttttttttt");
            }
        }
    }

    System.Collections.IEnumerator DisappearRoutine()
    {
        // 2. Ẩn Renderer (hình ảnh) và tắt Collider (va chạm)
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // 3. Chạy hiệu ứng (giả sử hiệu ứng đã được set Play On Awake)

        GameObject effect = Instantiate(hitEffect, transform.position,  Quaternion.identity);


        // 4. Đợi hiệu ứng chạy xong (ví dụ 2 giây)
        yield return new WaitForSeconds(2f);

        // 5. Lúc này mới thực sự xóa toàn bộ Item
        Destroy(gameObject);
    }

}
