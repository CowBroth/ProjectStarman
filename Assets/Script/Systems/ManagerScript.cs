using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    public static ManagerScript instance;
    public Stats playerStats;

    public GameObject playerObject;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BattleScene()
    {
        playerStats = playerObject.GetComponent<TestMoveScript>().stats;
        SceneManager.LoadScene(1);
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
