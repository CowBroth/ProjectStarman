using UnityEngine;

public class TestSceneChangeVariable : MonoBehaviour
{
    public Stats player_stats;

    private void Start()
    {
        player_stats = ManagerScript.instance.playerStats;
    }
}
