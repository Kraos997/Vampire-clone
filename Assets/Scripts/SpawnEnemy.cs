using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public static float _spawnCooldown = 0.5f;
    [SerializeField] private Transform[] _enemyArray;

    private void Start()
    {
        StartCoroutine(SpawnObjectOverTime());
        //SpawnObjects(1.5f);
    }

    IEnumerator SpawnObjectOverTime()
    {
        while (true)
        {
            Vector2 randomPos = new(Random.Range(-Utils.ArenaX, Utils.ArenaX), Random.Range(-Utils.ArenaX, Utils.ArenaX));
            int randomEnemyIndex = Random.Range(0, _enemyArray.Length);

            Instantiate(GameAssets.I.pfVisualWarning, randomPos, Quaternion.identity);
            yield return new WaitForSeconds(_spawnCooldown);
            Instantiate(_enemyArray[randomEnemyIndex], randomPos, Quaternion.identity);
            yield return new WaitForSeconds(_spawnCooldown);
        }
    }

    /*private async void SpawnObjects(float spawnC)
    {
        while (true)
        {
            Vector2 randomPos = new(Random.Range(-arenaX, arenaY), Random.Range(-arenaY, arenaY));

            Instantiate(visualWarning, randomPos, Quaternion.identity);
            await Task.Delay(1000); //wait for visual warning to disapear

            Instantiate(enemyPrefab, randomPos, Quaternion.identity);
            await Task.Delay((int)spawnC * 1000);
        }
    }*/
}
