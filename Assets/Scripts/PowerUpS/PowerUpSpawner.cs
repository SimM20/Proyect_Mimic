using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private Transform scorePaddle;
    [Range(0f, 1f)]
    [SerializeField] private float spawnChance = 0.25f;

    private void OnEnable() { TopPaddle.OnHit += TrySpawnPowerUp; }
    private void OnDisable() { TopPaddle.OnHit -= TrySpawnPowerUp; }

    public void TrySpawnPowerUp()
    {
        if (Random.value <= spawnChance)
        {
            if (powerUpPrefabs.Length == 0) return;

            int index = Random.Range(0, powerUpPrefabs.Length);
            Instantiate(powerUpPrefabs[index], scorePaddle.position, Quaternion.identity);
        }
    }
}
