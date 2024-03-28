using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static List<GameObject> EnemyList;

    public static float ArenaX = 19.5f;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one GameManager instance");
        }
        Instance = this;
    }
    private void Start()
    {
        GameManager.EnemyList?.Clear();
    }
}
