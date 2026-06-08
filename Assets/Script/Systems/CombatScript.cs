using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    ACTION,
    WON,
    LOST
}

public class CombatScript : MonoBehaviour
{
    private InputAction ctrl_interact;

    [Header("Health")]
    [SerializeField] float playerHealth;
    [SerializeField] float enemyHealth;

    [Header("Stats")]
    [SerializeField] Stats playerStats;
    [SerializeField] Stats enemyStats;
    BattleDialogue enemyDialogue;

    [Header("State")]
    public BattleState state;

    public GameObject enemyPrefab;
    [SerializeField] GameObject menuBox;
    [SerializeField] GameObject selfHPBox;
    [SerializeField] GameObject enemHPBox;

    void Start()
    {
        ctrl_interact = InputSystem.actions.FindAction("Interact");
        state = BattleState.START;
        StartCoroutine(BattleSetup());
    }

    IEnumerator BattleSetup()
    {
        enemyPrefab = ManagerScript.instance.prefab;
        Instantiate(enemyPrefab);
        playerStats = ManagerScript.instance.playerStats;
        enemyStats = ManagerScript.instance.enemyStats;
        playerHealth = playerStats.health;
        enemyHealth = enemyStats.health;

        enemyDialogue = enemyPrefab.GetComponent<BattleDialogue>();
        enemyPrefab.GetComponent<BattleDialogue>().TriggerDialogue(0);
        selfHPBox.GetComponentInChildren<Text>().text = "Ewan's HP: "+ "\n" + Math.Round(playerHealth);
        enemHPBox.GetComponentInChildren<Text>().text = "Enemy HP: " + "\n" + Math.Round(enemyHealth);
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        //PlayerTurn();

    }

    void Update()
    {
        menuBox.SetActive(BattleState.PLAYERTURN == state);
       
        /*if (DialogueManager.active == true)
        {
            if (ctrl_interact.triggered)
            {
                FindFirstObjectByType<DialogueManager>().Invoke("DisplayNextSentence", 1.5f);
                EnemyTurn();
            }
            return;
        }*/
    }

    IEnumerator PlayerAttack(int a = 0)
    {
        
        state = BattleState.ACTION;
        yield return new WaitForSeconds(2f);


        enemyHealth -= DamageCalculation(playerStats, a += playerStats.attack, enemyStats);
        print("Enem HP: " + enemyHealth);
        yield return new WaitForSeconds(1f);
        //check enemy dead
        if (enemyHealth <= 0)
        {
            FindFirstObjectByType<AudioManager>().Stop("OST");
            FindFirstObjectByType<AudioManager>().Play("Victory");
            state = BattleState.WON;
            enemyPrefab.GetComponent<BattleDialogue>().TriggerDialogue(5);
            yield return new WaitForSeconds(3f);
            ManagerScript.instance.LevelUp();
            ManagerScript.instance.OverworldScene();
        }
        else
        {
            enemHPBox.GetComponentInChildren<Text>().text = "Enemy HP: " + "\n" + Math.Round(enemyHealth); 
            EnemyTurn();
        }
        
        //state = BattleState.PLAYERTURN;
        //statechange
    }

    public void PlayerTurn(int turn)
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        gameObject.GetComponent<BattleDialogue>().TriggerDialogue(turn);
        if (turn == 0)
        {
            StartCoroutine(PlayerAttack());
        }
        if (turn == 1)
        {
            StartCoroutine(PlayerAttack(4));
        }
        Debug.Log("ello");
    }

    void EnemyTurn()
    {
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyAttack());
    }

    IEnumerator EnemyAttack()
    {
        int enemyTurn = UnityEngine.Random.Range(1, 3);
        enemyPrefab.GetComponent<BattleDialogue>().TriggerDialogue(enemyTurn);
        playerHealth -= EnemyDamageCalculation(enemyStats, enemyTurn, playerStats);
        if (playerHealth <= 0)
        {
            FindFirstObjectByType<AudioManager>().Stop("OST");
            FindFirstObjectByType<AudioManager>().Play("Loss");
            state = BattleState.LOST;
            enemyPrefab.GetComponent<BattleDialogue>().TriggerDialogue(4);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(0);
        }
        yield return new WaitForSeconds(3f);
        FindFirstObjectByType<AudioManager>().Play("DamageLow");
        selfHPBox.GetComponentInChildren<Text>().text = "Ewan's HP: " + "\n" + Math.Round(playerHealth);
        state = BattleState.PLAYERTURN;
    }

    public int DamageCalculation(Stats attacker, float attack, Stats defender)
    {
        int output = 0;

        //base dmg
        float baseDMG = attack - (defender.defense / 2);
        int variation = UnityEngine.Random.Range(-3, 3);
        baseDMG += variation;

        //crit check
        bool isCritical = UnityEngine.Random.value < (attacker.luck / 10);
        if (isCritical)
        {
            baseDMG = Mathf.Round(baseDMG * 1.5f);
            FindFirstObjectByType<AudioManager>().Play("DamageHigh");
        }
        else
        {
            FindFirstObjectByType<AudioManager>().Play("DamageLow");
        }
        //output
        output = Convert.ToInt32(baseDMG);
        return output;
    }
    public int EnemyDamageCalculation(Stats attacker, int move, Stats defender)
    {
        int output = 0;
        int dmgIncrease = 0;

        if (move == 2)
        {
            dmgIncrease = 3;
        }
        if (move == 3)
        {
            dmgIncrease -= attacker.attack;
        }
        //base dmg
        float baseDMG = attacker.attack + dmgIncrease - (defender.defense / 2);
        int variation = UnityEngine.Random.Range(-3, 3);
        baseDMG += variation;

        //crit check
        bool isCritical = UnityEngine.Random.value < (attacker.luck / 10);
        if (isCritical)
        {
            baseDMG = Mathf.Round(baseDMG * 1.5f);
        }
        //output
        output = Convert.ToInt32(baseDMG);
        return output;
    }
}