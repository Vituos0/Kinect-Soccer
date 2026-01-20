using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoxSpawnerZone : MonoBehaviour
{

    private BoxCollider spawnZone;
    public GameObject scoreBoxPrefab;

    [SerializeField] private Transform CellParent;



    public int spawnCount = 20;

    public Vector3 minScale = Vector3.one * 0.5f;
    public Vector3 maxScale = Vector3.one * 1.2f;

    public int minScore = 10;
    public int maxScore = 100;

    private List<Transform> spawnedBoxes = new List<Transform>();


    void Start()
    {
        spawnZone = GetComponent<BoxCollider>();
        SpawnAll();
    }

    void SpawnAll()
    {
        Bounds b = spawnZone.bounds;

        int gridSize = Mathf.CeilToInt(Mathf.Sqrt(spawnCount));
        float cellX = b.size.x / gridSize;
        float cellY = b.size.y / gridSize;

        int spawned = 0;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (spawned >= spawnCount)
                    return;

                Vector3 pos = new Vector3(
                    b.min.x + cellX * (x + 0.5f),                   
                    b.min.y + cellY * (y + 0.5f),
                    b.center.z
                );

                // random offset nhỏ để tự nhiên hơn
              //  pos.x += Random.Range(-cellX * 0.3f, cellX * 0.3f);
                //pos.y += Random.Range(-cellY * 0.3f, cellY * 0.3f);

                SpawnOne(pos);
                spawned++;
            }
        }
    }

    void SpawnOne(Vector3 pos)
    {
        // random scale
      //  float s = Random.Range(minScale.x, maxScale.x);
      float  s = 1;
        float radius = s * 0.5f;

        // ⭐ CHECK KHÔNG CHẠM
        pos = ResolveScoreBoxOverlap(pos, radius);

        GameObject box = Instantiate(scoreBoxPrefab, pos, Quaternion.identity,CellParent);
        box.transform.localScale = Vector3.one * s;

        spawnedBoxes.Add(box.transform); // ⭐ lưu lại box đã spawn

        // random score
        int score = Random.Range(minScore, maxScore + 1);
        box.GetComponent<ScoreBox>().Init(score);
    }


    private void OnDrawGizmos()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        if (!box) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(box.bounds.center, box.bounds.size);
    }

    Vector3 ResolveScoreBoxOverlap(Vector3 pos, float radius, int maxTry = 20)
    {
        Bounds b = spawnZone.bounds;

        for (int i = 0; i < maxTry; i++)
        {
            bool overlap = false;

            foreach (var other in spawnedBoxes)
            {
                float otherRadius = other.localScale.x * 0.5f;
                float dist = Vector3.Distance(pos, other.position);

                if (dist < radius + otherRadius)
                {
                    overlap = true;

                    Vector3 dir = (pos - other.position).normalized;
                    if (dir == Vector3.zero)
                        dir = Random.insideUnitSphere;

                    dir.z = 0; // ⭐ ép 2D

                    pos += dir * (radius + otherRadius - dist);

                    // ⭐⭐ CLAMP LẠI TRONG SPAWN ZONE
                    pos.x = Mathf.Clamp(pos.x, b.min.x + radius, b.max.x - radius);
                    pos.y = Mathf.Clamp(pos.y, b.min.y + radius, b.max.y - radius);
                    pos.z = b.center.z;

                    break;
                }
            }

            if (!overlap)
                return pos;
        }

        return pos;
    }



}
