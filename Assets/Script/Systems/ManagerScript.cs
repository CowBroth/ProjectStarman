using System;
using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    public static ManagerScript instance;
    public Stats playerStats;
    public Stats enemyStats;
    public GameObject prefab;
    public Vector2 position;
    public GameObject toDestroy;
    public GameObject toKill;

    
    public void BattleScene(Stats enemyInstance, GameObject prefabInstance, GameObject destroyInstance, Transform posInstance)
    {
        enemyStats = enemyInstance;
        prefab = prefabInstance;
        position = posInstance.position;
        toDestroy = destroyInstance;
        SceneManager.LoadScene(2);
    }

    public void OverworldScene()
    {
        SceneManager.LoadScene(1);
        //toDestroy.SetActive(false);
        //kill.SetActive(false);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LevelUp()
    {
        playerStats.health += 3;
        playerStats.attack += 1;
        playerStats.defense += 1;
        playerStats.luck += 1;
    }
}