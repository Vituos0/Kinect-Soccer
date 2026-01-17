using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [Header("Actions")]
    public static Action< Ball, Collision> onCollisionWithball;

    [Header("Setting")]
    [SerializeField] private float lifeTime = 10f;
    private bool isColliedWithPlayer;
    private Coroutine lifeTimecoroutine;


    [Header("Data")]
    public int lastPlayerTouchball;


    // Start is called before the first frame update
    void Start()
    {
        lifeTimecoroutine = StartCoroutine(TimeLife());
    }
    //=============================XU LY VA CHAM==========================
    private void OnCollisionEnter(Collision collision)  
    {
        ManageCollison(collision);
    }

    /*
     Logic xu ly nhu sau: 
    1. Khi ball va cham voi doi tuong khac, se kiem tra doi tuong do co component Collision khong
    2.neu co thi goi collider cua doi tuong do 
    3. sau do se goi su kien onCollisionWithball, truyen doi tuong ball(this) va doi tuong bi va cham(TargetCollider) vao
    4. Cac doi tuong lang nghe su kien nay co the xu ly logic rieng cua minh trong ham xu ly su kien
     */
    private void ManageCollison(Collision collision)
    {   
        onCollisionWithball?.Invoke(this, collision);
         
    }
    /*=============Thoi gian ton tai cua ball trong scene===================*/
    IEnumerator TimeLife()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        Debug.Log("Ball has been destroyed");
    }

}
