/**** 
 * Created by: Andrew Nguyen
 * Date Created: 11 April, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: 22 April, 2022
 * 
 * Description: Manages battles. Communicates with Inventory, UI, Player, and Enemies.
 * 
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        playerRef = gm.playerRef;
        enemyRef = gm.enemyRef;
    } //end Awake

    private void InitializeUnit(GameObject s, Transform t)
    {
        Instantiate(s, t);
        if (s == playerRef)
        {
            CAStats = s.GetComponent<CharlieArcher>();
        } //is a player
        else
        {
            enemyStats = s.GetComponent<EnemyScript>();
        } //must be an enemy
    } //end InitializeUnit()

    // Start is called before the first frame update
    void Start()
    {
        //The battle will start always with the player's turn
        State = battleState.PlayerTurn;

        //Set up player and enemy

        //Instantiate player at this location
        InitializeUnit(playerRef, playerPos);

        //Instantiate enemy at the other side
        InitializeUnit(enemyRef, enemyPos);
    } //end Start()

    #region playerMoves
    //Player attack. These and other options are only available if it is the player's turn. Otherwise they will be HIDDEN.
    //May need use of update in a battle HUD script.
    public void playerAttack()
    {
        //Instantiate a new explosion, an explosion will suffice for attack animation
        Instantiate(explosionGood, enemyPos);

        //The explosion will have an event at the end of it that will tell itself in its script to destroy itself.

        //Then at the end...
        enemyStats.hitpoints -= CAStats.attack;

        //Now it is the enemy's turn
        Invoke("switchTurn", 2.0f);
    } //end playerAttack()

    public void playerGuard()
    {
        //Nullify once player's turn is back.
        //The next attack, if they choose to attack, will do a quarter of the damage.
        CAStats.defense = 0.25f;

        Invoke("switchTurn", 2.0f);
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
            Invoke("switchTurn", 2.0f);
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
        CAStats.hitpoints -= enemyStats.attack;

        //Now it is the player's turn
        Invoke("switchTurn", 2.0f);
    } //end enemyAttack()

    public void enemyChargeAttack()
    {
        //The enemy will spend a turn charging an attack. They will always do the strong attack on their next turn, so set a flag for that.
        Instantiate(explosionBad, enemyPos);
        Debug.Log("Enemy is charging a strong move");

        Invoke("switchTurn", 2.0f);
    } //end enemyChargeAttack()

    public void enemyStrongAttack()
    {
        //Instantiate a new explosion, an explosion will suffice for attack animation
        Instantiate(explosionBad, playerPos);

        //The explosion will have an event at the end of it that will tell itself in its script to destroy itself.

        //Then at the end...
        CAStats.hitpoints -= enemyStats.attack*2;

        //Now it is the player's turn
        Invoke("switchTurn", 2.0f);
    } //end enemyStrongAttack()

    public void enemyGuard()
    {
        enemyStats.defense = 0.5f;
        Invoke("switchTurn", 2.0f);
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
        //Decide what to do
        enemyAction = Random.Range(1, 100);

        //Roughly 70% they will attack
        if (enemyAction > 30)
        {
            enemyAttack();
        }
        //10% chance they'll do a strong attack
        else if (enemyAction < 30 && enemyAction > 20)
        {
            enemyChargeAttack();
        }
        //20% they guard
        else
        {
            enemyGuard();
        }

    } //end enemyTurn()

    // Update is called once per frame
    void Update()
    {
        //Set up a turn system, exit the battle if the enemy or player's HP reaches 0
        //Is it the enemy's turn?
        if (State == battleState.EnemyTurn)
        {
            enemyTurn();
        }

        //Health Check
        if (CAStats.hitpoints <= 0)
        {
            Debug.Log("Game Over! Go to game over screen!");
            SceneManager.LoadScene("end_scene");
        }
        if (enemyStats.hitpoints <= 0)
        {
            Debug.Log("We won! Right on! Go back to the dungeon");
            Debug.Log("Drops should be notified on the HUD to the player");
            SceneManager.LoadScene(gm.lastScene); //Previous level should be logged by GameManager or the Battle Manager right before the player attacks an enemy and enters the battle screen

        }
        
    }
}
