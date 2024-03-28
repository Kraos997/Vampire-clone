using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public static SpawnEnemy Instance { get; private set; }

    [field: SerializeField] public float SpawnCooldown { get; private set; } = 0.5f;
    [SerializeField] private Transform[] _enemyArray;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one SpawnEnemy instance");
        }
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(SpawnObjectOverTime());
    }

    IEnumerator SpawnObjectOverTime()
    {
        while (true)
        {
            float arenaX = GameManager.ArenaX;
            Vector2 randomPos = new(Random.Range(-arenaX, arenaX), Random.Range(-GameManager.ArenaX, GameManager.ArenaX));

            int randomEnemyIndex = Random.Range(0, _enemyArray.Length);

            Instantiate(GameAssets.I.pfVisualWarning, randomPos, Quaternion.identity);
            yield return new WaitForSeconds(SpawnCooldown);
            Instantiate(_enemyArray[randomEnemyIndex], randomPos, Quaternion.identity);
            yield return new WaitForSeconds(SpawnCooldown);
        }
    }
}
