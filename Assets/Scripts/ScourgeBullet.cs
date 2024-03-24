using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScourgeBullet : SlowBullet
{
    private void Start()
    {
        StartCoroutine(Couroutine());
    }

    IEnumerator Couroutine()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.3f);
            Instantiate(GameAssets.I.pfMiniScourgeBullet, transform.position, Quaternion.identity);
        }
    }
}
