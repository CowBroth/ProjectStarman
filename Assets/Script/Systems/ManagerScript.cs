using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    public static ManagerScript instance;
    public Stats playerStats;
    public Stats enemyStats;
    public GameObject prefab;
    
    public void BattleScene(Stats enemyInstance, GameObject prefabInstance)
    {
        enemyStats = enemyInstance;
        prefab = prefabInstance;
        SceneManager.LoadScene(1);
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