using UnityEngine;

public class EnemyBattle : MonoBehaviour
{
    public Stats stats;

    private void Start()
    {
        stats = ManagerScript.instance.enemyStats;
    }
}
