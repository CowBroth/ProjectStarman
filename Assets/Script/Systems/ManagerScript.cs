using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    public static ManagerScript instance;
    public Stats playerStats;
    public Stats enemyStats;
    
    public void BattleScene(Stats enemyInstance)
    {
        enemyStats = enemyInstance;
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
}