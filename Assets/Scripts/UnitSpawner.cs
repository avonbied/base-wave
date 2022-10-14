using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject[] EntityPrefabs;
    public int[] StartSpawnRatio;
    public int[] SpawnRuntimeRatio;

    public int StartSpawnUnitCount;

    public float SpawnRuntimeTime;
    public int SpawnRuntimeUnitCount;

    public GameObject[] SpawnZoneObjects;
    private Rect[] SpawnZoneRects;

    public Vector3 EntityTarget;
    private float spawnCounter = 0.0f;

    private int lastUnitIndex = 0;

    public int TotalWaveUnitCount;

    private int currentWaveSpawnedCount = 0;

    public int CurrentOnScreenCount = 0;

    private float IncreaseTimer1 = 15;
    private float IncreaseTimer2 = 60;

    public int TotalUnitSpawnPerCap = 15;
    public float FinalMiniumSpawnRateTime = 0.1f;

    private void Start()
    {
        SpawnZoneRects = new Rect[SpawnZoneObjects.Length];
        ConvertSpawnZones();
        spawnCounter = SpawnRuntimeTime;
        Global.EnemyHPMultiplier = 1;
        Global.FriendlyDamageMultiplier = 1;
        DoSpawn(SpawnZoneRects, EntityPrefabs, StartSpawnRatio, StartSpawnUnitCount);
    }

    void ConvertSpawnZones()
    {
        for (int i = 0; i < SpawnZoneObjects.Length; ++i)
        {
            var go = SpawnZoneObjects[i];

            var trans = go.transform;

            var t1 = trans.position - (trans.localScale / 2);
            var t2 = trans.position + (trans.localScale / 2);

            SpawnZoneRects[i] = new Rect(t1, t2 - t1);
        }
    }

    private void Update()
    {
        spawnCounter -= Time.deltaTime;
        IncreaseTimer1 -= Time.deltaTime;
        IncreaseTimer2 -= Time.deltaTime;

        if (IncreaseTimer1 <= 0)
        {
            IncreaseTimer1 = 15;
            Global.EnemyHPMultiplier *= 1.01f;
            if (SpawnRuntimeUnitCount < TotalUnitSpawnPerCap)
            {
                SpawnRuntimeUnitCount += 1;
            }
        }
        if (IncreaseTimer2 <= 0)
        {
            IncreaseTimer2 = 60;
            if (SpawnRuntimeTime > FinalMiniumSpawnRateTime)
            {
                SpawnRuntimeTime *= 0.95f;
            }
            else
            {
                SpawnRuntimeTime = FinalMiniumSpawnRateTime;
            }
        }

        if (spawnCounter <= 0)
        {
            spawnCounter = SpawnRuntimeTime;
            lastUnitIndex = DoSpawn(SpawnZoneRects, EntityPrefabs, SpawnRuntimeRatio, SpawnRuntimeUnitCount, lastUnitIndex);
        }
    }

    int GetTotalRatioUnitCount(int[] ratios)
    {
        int count = 0;
        foreach (var item in ratios)
        {
            count += item;
        }
        return count;
    }

    // Returns unit count location within the spawn ratios list.
    int DoSpawn(Rect[] spawnZones, GameObject[] prefabs, int[] ratios, int count, int startUnitRatioCount = 0)
    {
        int totalRatioUnitCount = GetTotalRatioUnitCount(ratios);
        int spawnedUnits = totalRatioUnitCount - Mathf.Clamp(startUnitRatioCount, 0, totalRatioUnitCount);
        int spawnZone = 0;
        for (int i = 0; i < ratios.Length; ++i)
        {
            int spawnCount = ratios[i];
            for (int ec = 0; ec < spawnCount; ++ec, ++spawnedUnits)
            {
                // Iterate until we resume the original spawning unit index location
                if (spawnedUnits < startUnitRatioCount)
                    continue;

                if (currentWaveSpawnedCount >= TotalWaveUnitCount)
                {
                    return 0;
                }

                // TODO - Set rect
                spawnZone = spawnedUnits % spawnZones.Length;
                SpawnUnit(spawnZones[spawnZone], prefabs[i]);
            }
        }
        return spawnedUnits - startUnitRatioCount;
    }

    void SpawnUnit(Rect mapSpawnZone, GameObject entityPrefab)
    {
        float xPos = Random.Range(mapSpawnZone.xMin, mapSpawnZone.xMax);
        float yPos = Random.Range(mapSpawnZone.yMin, mapSpawnZone.yMax);
        Instantiate(entityPrefab, new Vector3(xPos, yPos, 0.0f), Quaternion.identity);
        currentWaveSpawnedCount++;
        CurrentOnScreenCount++;

    }

    // public void SetSpawn
}