using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Actions")]
    public static Action<Ball> onCollisionWithBall;

    [Header("Settings")]
    private bool isDestroyed = false;

    private Coroutine lifeCoroutine;

    void Start()
    {
        // Bắt đầu đếm thời gian sống 10s ngay khi spawn
        lifeCoroutine = StartCoroutine(TimeLife());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDestroyed) return;

        if (collision.collider.TryGetComponent<Wall>(out Wall wall))
        {
            onCollisionWithBall?.Invoke(this);
            DestroyBall();
        }
    }

    IEnumerator TimeLife()
    {
        yield return new WaitForSeconds(10f);
        DestroyBall();
    }

    private void DestroyBall()
    {
        if (isDestroyed) return;

        isDestroyed = true;

        if (lifeCoroutine != null)
            StopCoroutine(lifeCoroutine);

        Destroy(gameObject);
    }
}
