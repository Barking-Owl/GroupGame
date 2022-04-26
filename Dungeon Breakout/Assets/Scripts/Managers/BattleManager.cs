/**** 
 * Created by: Andrew Nguyen
 * Date Created: 11 April, 2022
 * 
 * Last Edited by: Camp Steiner
 * Last Edited: 25 April, 2022
 * 
 * Description: Manages battles. Communicates with Gamemanager, UI, Player, and Enemies.
 * 
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    GameManager gm; //reference to game manager
    
    //New battlemanager with player then enemy, make a battle manager for these two enemies to fight. Get their stats, and have them fight.
    public enum battleState { PlayerTurn, EnemyTurn };

    public battleState State;

    //Variables//

    //Before battle is instantiated, these things should be done [IN GAME MANAGER]

    //These need the playerCharacter script to interact with Game Manager
    // -Set the reference to playerRef in Game Manager
    // -Set reference to enemyRef in Game Manager
    // -Get the index of the dungeon right before the battle starts in Game Manager. After the battle is won by the player, load that.
    // -The enemy should be destroyed by getting the reference in GameManager.

    private int enemyAction;

    [Header("SET DYNAMICALLY")]
    public GameObject playerRef;
    public GameObject enemyRef;
    //References to Player and Enemy's stats.
    public CharlieArcher CAStats;
    public EnemyScript enemyStats;
    public bool guardFlag; //Did the player guard on the previous turn?
    public bool guardFlagEnemy; //Is the enemy guard on the prev turn?
    public bool chargeFlag; //Did the enemy perform a charge attack before hand?
    public Text statusTextbox; //What are we doing?

    [Header("SET IN INSPECTOR")]
    //These transforms are empty gameobjects. The background will be a part of the canvas, but the two gameobjects won't move.
    //Spheres are only there for reference
    public Transform playerPos;
    public Transform enemyPos;
    public GameObject explosionGood;
    public GameObject explosionBad;
    

    private void Awake()
    {
        gm = GameManager.GM;
        //playerRef = gm.playerRef;
        //enemyRef = gm.enemyRef;
    } //end Awake

    public void InitializeUnits()
    {
        Debug.Log("Initializing units");
        GameObject player = Instantiate(playerRef, playerPos);
        CAStats = player.GetComponent<CharlieArcher>();
        CharlieArcher.isDisabled = true;
        player.transform.localScale += new Vector3(3, 3, 3);

        GameObject enemy = Instantiate(enemyRef, enemyPos);
        enemyStats = enemy.GetComponent<EnemyScript>();

        enemy.transform.localScale += new Vector3(3, 3, 3);
    } //end InitializeUnit()

    // Start is called before the first frame update
    void Start()
    {
        //The battle will start always with the player's turn
        State = battleState.PlayerTurn;

        //Set up player and enemy
        InitializeUnits();
    } //end Start()

    #region playerMoves
    //Player attack. These and other options are only available if it is the player's turn. Otherwise they will be HIDDEN.
    //May need use of update in a battle HUD script.
    public void playerAttack()
    {
        //Instantiate a new explosion, an explosion will suffice for attack animation
        Debug.Log("Attack connected");
        Instantiate(explosionGood, enemyPos);

        //The explosion will have an event at the end of it that will tell itself in its script to destroy itself.

        //Then at the end...
        enemyStats.hitpoints -= CAStats.attack*enemyStats.defense;

        //Now it is the enemy's turn
        switchTurn();
    } //end playerAttack()

    public void playerGuard()
    {
        //Nullify once player's turn is back.
        //The next attack, if they choose to attack, will do a quarter of the damage.
        CAStats.defense = 0.25f;

        switchTurn();
        guardFlag = true;
    } //end playerGuard()

    public void playerHeal()
    {
        //Subtract a healing potion and only do this if there is a healing potion. 
        if (CAStats.potions > 0)
        {
            CAStats.hitpoints += 34;
            //Make sure there is a check that if it goes over 100 it goes back to 100.
            if (CAStats.hitpoints > 100)
            {
                CAStats.hitpoints = 100;
                Debug.Log("Healed with enough health to overheal. Resetting health back to 100.");
            }
            CAStats.potions--;
            switchTurn();
        }
        else //If there isn't, keep it on player's turn. If there is, go to enemy's turn and heal player.
        {
            Debug.Log("Attempted to heal without potion"); 
            return;
        }
    } //end playerHeal()
    #endregion playerMoves

    #region enemyMoves
    public void enemyAttack()
    {
        //Instantiate a new explosion, an explosion will suffice for attack animation
        Instantiate(explosionBad, playerPos);

        //The explosion will have an event at the end of it that will tell itself in its script to destroy itself.

        //Then at the end...
        CAStats.hitpoints -= enemyStats.attack*CAStats.defense;

        //Now it is the player's turn
        switchTurn();
    } //end enemyAttack()

    public void enemyChargeAttack()
    {
        //The enemy will spend a turn charging an attack. They will always do the strong attack on their next turn, so set a flag for that.
        Instantiate(explosionBad, enemyPos);
        Debug.Log("Enemy is charging a strong move");

        chargeFlag = true;
        switchTurn();
    } //end enemyChargeAttack()

    public void enemyStrongAttack()
    {
        //Instantiate a new explosion, an explosion will suffice for attack animation
        Instantiate(explosionBad, playerPos);

        //The explosion will have an event at the end of it that will tell itself in its script to destroy itself.

        //Then at the end...
        CAStats.hitpoints -= (enemyStats.attack*2)*CAStats.defense;
        chargeFlag = false;
        //Now it is the player's turn
        switchTurn();
    } //end enemyStrongAttack()

    public void enemyGuard()
    {
        enemyStats.defense = 0.5f;
        switchTurn();
        guardFlagEnemy = true;
    } //end enemyGuard()

    #endregion enemyMoves
    public void switchTurn()
    {
        if (State == battleState.PlayerTurn)
        {
            State = battleState.EnemyTurn;
        }
        else
        {
            State = battleState.PlayerTurn;
        }
    } //end switchTurn()

    public void enemyTurn()
    {
        statusTextbox.text = "Enemy's turn."; 
        if (chargeFlag == true)
        {
            statusTextbox.text = "The enemy is attacking!"; 
            enemyStrongAttack();
        }
        else
        {
            //Decide what to do
            enemyAction = Random.Range(1, 100);

            //Roughly 80% they will attack
            if (enemyAction > 20)
            {
                statusTextbox.text = "The enemy is attacking!";
                enemyAttack();
            }
            //10% chance they'll do a strong attack
            else if (enemyAction < 20 && enemyAction > 10)
            {
                statusTextbox.text = "The enemy is charging up a strong attack, they will attack with double the force as normal"; 
                enemyChargeAttack();
            }
            //10% they guard
            else
            {
                statusTextbox.text = "The enemy is guarding. Now is a good time to heal...";
                enemyGuard();
            }
        }
    } //end enemyTurn()

    public void GameOver()
    {
        gm.GameOver();
    } //end GameOver()

    public void BattleWon()
    {
        gm.tutorialStage++;
        SceneManager.LoadScene(gm.lastScene);
    } //end BattleWon()

    // Update is called once per frame
    void Update()
    {
        //Set up a turn system, exit the battle if the enemy or player's HP reaches 0
        //Is it the enemy's turn?
        if (State == battleState.EnemyTurn)
        {
            enemyTurn();
        }

        //Health Check; Previous level should be logged by GameManager or the Battle Manager right before the player attacks an enemy and enters the battle screen
        if (CAStats.hitpoints <= 0)
        {
            statusTextbox.text = "Oh no..."; 
            Debug.Log("Game Over! Go to game over screen!");
            GameOver();
        }
        if (enemyStats.hitpoints <= 0)
        {
            statusTextbox.text = "Right on!"; 
            Debug.Log("We won! Right on! Go back to the dungeon");
            Debug.Log("Drops should be notified on the HUD to the player");
            BattleWon();

        }
        
    }
}
