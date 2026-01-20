using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoxSpawnerBeeZone : MonoBehaviour
{

    private BoxCollider spawnZone;
    public GameObject scoreBoxPrefab;

    [SerializeField] private Transform CellParent;



    public int spawnCount = 20;

    //public Vector3 minScale = Vector3.one * 0.5f;
    //public Vector3 maxScale = Vector3.one * 1.2f;

    [SerializeField] public float s;

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

        float cellSize = 1.2f;               // KÍCH THƯỚC MỖI SCORE BOX
        float xSpacing = cellSize;
        float ySpacing = cellSize * 0.866f;  // ⭐ khoảng cách tổ ong

        int row = 0;

        for (float y = b.min.y + cellSize / 2; y < b.max.y; y += ySpacing)
        {
            bool offsetRow = row % 2 == 1;
            float startX = b.min.x + cellSize / 2;

            if (offsetRow)
                startX += xSpacing / 2; // ⭐ so le

            for (float x = startX; x < b.max.x; x += xSpacing)
            {
                if (spawnedBoxes.Count >= spawnCount)
                    return;

                Vector3 pos = new Vector3(x, y, b.center.z);

                SpawnOne(pos);
            }

            row++;
        }
    }


    void SpawnOne(Vector3 pos)
    {
        GameObject box = Instantiate(scoreBoxPrefab, pos, Quaternion.identity, CellParent);

        box.transform.localScale = Vector3.one * s ; // ✅ scale cố định

        spawnedBoxes.Add(box.transform);

        if (box.TryGetComponent(out ScoreBox sb))
        {
            int score = Random.Range(minScore, maxScore + 1);
            sb.Init(score);
        }
    }

}
