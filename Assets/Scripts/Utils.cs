using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utils
{
    public static int ArenaX = 20;
    public static Enemy GetClosest(Vector2 position, float maxRange)
    {
        Enemy closest = null;
        if (Enemy.EnemyList != null)
        {
            foreach (Enemy enemy in Enemy.EnemyList)
            {
                if (Vector2.Distance(position, enemy.GetPosition()) <= maxRange)
                {
                    if (closest == null)
                    {
                        closest = enemy;
                    }
                    else
                    {
                        if (Vector2.Distance(position, enemy.GetPosition()) < Vector2.Distance(position, closest.GetPosition()))
                        {
                            closest = enemy;
                        }
                    }
                }
            }
        }
        return closest;
    }

}
