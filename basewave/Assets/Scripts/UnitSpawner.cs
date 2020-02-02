using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject FriendlyPrefab;
    public int countPerSide = 10;

    private void Start()
    {
        Application.targetFrameRate = 2000;
        QualitySettings.vSyncCount = 0;
        for (int i = 0; i < countPerSide; ++i)
        {

            int xPos = Random.RandomRange(1, 5);
            int yPos = Random.RandomRange(-5, 5);
            Instantiate(enemyPrefab, new Vector3(xPos, yPos, 0.0f), Quaternion.identity);
        }
        for (int i = 0; i < countPerSide; ++i)
        {

            int xPos = Random.RandomRange(-1, -5);
            int yPos = Random.RandomRange(-5, 5);
            Instantiate(FriendlyPrefab, new Vector3(xPos, yPos, 0.0f), Quaternion.identity);
        }
    }
}