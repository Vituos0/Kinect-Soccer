using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BallSpawnerZone : MonoBehaviour
{


    [SerializeField] private GameObject prefabBall;

    public float spawnTime;

    BoxCollider box;


    private void Start()
    {
        box = GetComponent<BoxCollider>();
        InvokeRepeating(nameof(CheckBoxSpawner), spawnTime, spawnTime);
    }
    void CheckBoxSpawner()
    {
        Bounds b = box.bounds;

        Collider[] hits = Physics.OverlapBox(
            b.center,
            b.extents,
            Quaternion.identity
        );

        foreach (var h in hits)
        {
            if (h.CompareTag("Ball"))
                return; // đã có ball → không spawn
        }

        // không có ball
        SpawnBall();
    }

    void SpawnBall()
    {
        Instantiate(prefabBall, transform.position, Quaternion.identity);
    }


    private void OnDrawGizmos()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        if (!box) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(box.bounds.center, box.bounds.size);
    }
}
