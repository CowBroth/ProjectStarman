using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    void Start()
    {
        ctrl_interact = InputSystem.actions.FindAction("Interact");
        state = BattleState.START;
        StartCoroutine(BattleSetup());
    }

    IEnumerator BattleSetup()
    {
        Instantiate(enemyPrefab);
        playerStats = ManagerScript.instance.playerStats;
        enemyStats = ManagerScript.instance.enemyStats;
        playerHealth = playerStats.health;
        enemyHealth = enemyStats.health;

        enemyDialogue = enemyPrefab.GetComponent<BattleDialogue>();
        enemyPrefab.GetComponent<BattleDialogue>().TriggerDialogue(0);
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

    IEnumerator PlayerAttack()
    {
        state = BattleState.ACTION;
        yield return new WaitForSeconds(2f);

        int damageValue = DamageCalculation(playerStats, enemyStats);
        print("DMG: " + damageValue + " HP: " + enemyHealth);
        yield return new WaitForSeconds(1f);
        //check enemy dead
        if (enemyHealth <= 0)
        {
            state = BattleState.WON;
        }
        else
        {
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
        yield return new WaitForSeconds(3f);
        state = BattleState.PLAYERTURN;
    }

    public int DamageCalculation(Stats attacker, Stats defender)
    {
        int output = 0;

        //base dmg
        float baseDMG = attacker.attack - (defender.defense / 2);
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