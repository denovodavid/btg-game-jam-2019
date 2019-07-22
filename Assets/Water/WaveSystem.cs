using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public GameObject wavePrefab;

    public void SpawnWave()
    {
        Instantiate(wavePrefab, transform.position, Quaternion.identity, transform);
    }
}
