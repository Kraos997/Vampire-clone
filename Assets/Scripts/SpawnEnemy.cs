using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private float _spawnCooldown = 0.001f;
    [SerializeField] private float _arenaX = 19.5f;
    [SerializeField] private float _arenaY = 19.5f;

    private void Start()
    {
        StartCoroutine(SpawnObjectOverTime());
        //SpawnObjects(1.5f);
    }

    IEnumerator SpawnObjectOverTime()
    {
        while (true)
        {
            Vector2 randomPos = new(Random.Range(-_arenaX, _arenaY), Random.Range(-_arenaY, _arenaY));

            Instantiate(GameAssets.I.pfVisualWarning, randomPos, Quaternion.identity);
            yield return new WaitForSeconds(1);
            Instantiate(GameAssets.I.pfEnemy1, randomPos, Quaternion.identity);
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
