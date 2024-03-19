using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utils
{
    public static int ArenaX = 20;
    
    public static int GetSpatialGroupStatic(float xPos, float yPos)
    {
        float adjustedX = xPos + ArenaX / 2;
        float adjustedY = yPos + ArenaX / 2;

        int xIndex = (int)(adjustedX / ArenaX);
        int yIndex = (int)(adjustedY / ArenaX);

        return xIndex + yIndex * ArenaX;
    }
    

    public static int GetSpatialGroupDynamic(float xPos, float yPos, float mapWidth, float mapHeight, int totalPartitions)
    {
        int cellsPerRow = (int)Mathf.Sqrt(totalPartitions);
        int cellsPerColumn = cellsPerRow;

        float cellWidth = mapWidth / cellsPerRow;
        float cellHeight = mapHeight / cellsPerColumn;

        float adjustedX = xPos + (mapWidth / 2);
        float adjustedY = yPos + (mapHeight / 2);

        int xIndex = (int)(adjustedX / cellWidth);
        int yIndex = (int)(adjustedY / cellHeight);

        xIndex = Mathf.Clamp(xIndex, 0, cellsPerRow - 1);
        yIndex = Mathf.Clamp(yIndex, 0, cellsPerColumn - 1);

        return xIndex + yIndex * cellsPerColumn;
    }

    public static List<int> GetExpandedSpatialGroups(int spatialGroup, int radius = 1)
    {
        List<int> expandedSpatialGroups = new List<int>();

        int widthRange = 20;
        int heightRange = 20;
        int numberOfPartitions = 400;

        for ( int dx = -radius; dx <= radius; dx++)
        {
            for( int dy = -radius; dy <= radius; dy++)
            {
                int newGroup = spatialGroup + dx + dy * widthRange;

                bool isWithinWidth = newGroup % widthRange >= 0 && newGroup % widthRange < widthRange;
                bool isWithinHeight = newGroup / widthRange >= 0 && newGroup / widthRange < heightRange;
                bool isWithinBounds = isWithinWidth && isWithinHeight;

                bool isWithinPartitions = newGroup >= 0 && newGroup < numberOfPartitions;

                if(isWithinBounds && isWithinPartitions)
                {
                    expandedSpatialGroups.Add(newGroup);
                }
            }
        }
        return expandedSpatialGroups.Distinct().ToList();
    }

    public static List<Enemy> GetAllEnemiesInSpatialGroups(List<int> spatialGroups)
    {
        List<Enemy> enemies = new List<Enemy>();

        foreach ( int spatialGroup in spatialGroups )
        {
            enemies.AddRange(Enemy.EnemyList);
        }
        return enemies;
    }

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
