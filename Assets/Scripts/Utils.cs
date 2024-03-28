using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utils
{
    public static GameObject GetClosest(Vector2 position, float maxRange)
    {
        GameObject closest = null;
        if (GameManager.EnemyList != null)
        {
            foreach (GameObject enemy in GameManager.EnemyList)
            {
                if (Vector2.Distance(position, enemy.transform.position) <= maxRange)
                {
                    if (closest == null)
                    {
                        closest = enemy;
                    }
                    else
                    {
                        if (Vector2.Distance(position, enemy.transform.position) < Vector2.Distance(position, closest.transform.position))
                        {
                            closest = enemy;
                        }
                    }
                }
            }
        }
        return closest;
    }

    public static void ManageEnemyList(GameObject thisGameObject)
    {
        if (GameManager.EnemyList == null)
        {
            GameManager.EnemyList = new List<GameObject>
            {
                thisGameObject
            };
        }
        else
        {
            GameManager.EnemyList.Add(thisGameObject);
        }
    }
}
