using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{


    [Header("Actions")]
    public static Action onCollisionWithball;



    [Header("Setting")]
    private bool isCollied;
    private bool canbeDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollied)
        {   
            StartCoroutine(TimeLife());
        }
        StartCoroutine(TimeLife());
        
    }

    private void ManageCollison(Collision collision)
    {
        isCollied = true;
        if(collision.collider.TryGetComponent<Wall>(out Wall wall))
        {
            onCollisionWithball?.Invoke();
            canbeDestroyed = true;
        }
    }

    IEnumerator TimeLife()
    {
        yield return new WaitForSeconds(10f);
        DeleteTheBall();
    }
    public void DeleteTheBall()
    {
        Destroy(gameObject);
    }


}
