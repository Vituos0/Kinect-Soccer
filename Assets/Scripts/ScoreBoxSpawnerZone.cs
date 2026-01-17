using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoxSpawnerZone : MonoBehaviour
{

    private BoxCollider spawnZone;
    public GameObject scoreBoxPrefab;

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
                pos.x += Random.Range(-cellX * 0.3f, cellX * 0.3f);
                pos.y += Random.Range(-cellY * 0.3f, cellY * 0.3f);

                SpawnOne(pos);
                spawned++;
            }
        }
    }

    void SpawnOne(Vector3 pos)
    {
        // random scale
        float s = Random.Range(minScale.x, maxScale.x);
        float radius = s * 0.5f;

        // ⭐ CHECK KHÔNG CHẠM
        pos = ResolveScoreBoxOverlap(pos, radius);

        GameObject box = Instantiate(scoreBoxPrefab, pos, Quaternion.identity);
        box.transform.localScale = Vector3.one * s;

        spawnedBoxes.Add(box.transform); // ⭐ lưu lại box đã spawn

        // random score
        int score = Random.Range(minScore, maxScore + 1);
        box.GetComponent<ScoreBox>().Init(score);
    }


    void OnDrawGizmos()
    {
        if (!spawnZone) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnZone.bounds.center, spawnZone.bounds.size);
    }

    Vector3 ResolveScoreBoxOverlap(Vector3 pos, float radius, int maxTry = 20)
    {
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

                    // đẩy box mới ra xa box cũ
                    Vector3 dir = (pos - other.position).normalized;
                    if (dir == Vector3.zero)
                        dir = Random.insideUnitSphere;

                    pos += dir * (radius + otherRadius - dist);
                    break;
                }
            }

            if (!overlap)
                return pos;
        }

        return pos; // nếu vẫn chạm thì trả vị trí cuối
    }

}
