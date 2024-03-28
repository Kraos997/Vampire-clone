using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpriteScript : MonoBehaviour
{
    public void Awake()
    {
        Destroy(gameObject, SpawnEnemy.Instance.SpawnCooldown);
    }
}
