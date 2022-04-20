/**** 
 * Created by: Andrew Nguyen
 * Date Created: 11 April, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: 20 April, 2022
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
    public enum battleState { PlayerTurn, EnemyTurn, BattleEnd};

    public battleState State;

    //Variables//

    //Before battle is instantiated, these things should be done [IN GAME MANAGER]
    // -Set the reference to playerRef in Game Manager
    // -Set reference to enemyRef in Game Manager
    // -Get the index of the dungeon right before the battle starts in Game Manager. After the battle is won by the player, load that.
    // -The enemy should be destroyed by getting the reference in GameManager.

    [Header("SET DYNAMICALLY")]
    public GameObject playerRef;
    public GameObject enemyRef;
    //References to Player and Enemy's stats.
    public CharlieArcher CAStats;
    public EnemyScript enemyStats;

    [Header("SET IN INSPECTOR")]
    //These transforms are empty gameobjects. The background will be a part of the canvas, but the two gameobjects won't move.
    //Spheres are only there for reference
    public Transform playerPos;
    public Transform enemyPos;

    

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

    //Player attack. These and other options are only available if it is the player's turn. Otherwise they will be HIDDEN.
    //May need use of update in a battle HUD script.
    public void playerAttack()
    {
        //Instantiate a new explosion, an explosion will suffice for attack animation

        //The explosion will have an event at the end of it that will tell itself in its script to destroy itself.
        //But this may not be the most efficient.

        //Then at the end...
        enemyStats.hitpoints -= CAStats.attack;

        State = battleState.EnemyTurn;
    } //end playerAttack()

    public void playerGuard()
    {
        //Nullify once player's turn is back.
        //The next attack, if they choose to attack, will do a quarter of the damage.
        CAStats.defense = 0.50f;

        State = battleState.EnemyTurn;
    } //end playerGuard()

    public void playerHeal()
    {
        //Subtract a healing potion and only do this if there is a healing potion. If there isn't, keep it on palyer's turn. If there is, go to enemy's turn and heal player.
        CAStats.hitpoints += 34; //Make sure there is a check that if it goes over 100 it goes back to 100.
        State = battleState.EnemyTurn;

    } //end playerHeal()

    public void enemyAttack()
    {
        State = battleState.PlayerTurn;
    } //end enemyAttack()

    public void enemyChargeAttack()
    {
        //The enemy will spend a turn charging an attack. They will always do the strong attack on their next turn, so set a flag for that.
        State = battleState.PlayerTurn;
    } //end enemyChargeAttack()

    public void enemyStrongAttack()
    {

    } //end enemyStrongAttack()

    public void enemyGuard()
    {
        State = battleState.PlayerTurn;
    }
    // Update is called once per frame
    void Update()
    {
        //Set up a turn system, exit the battle if the enemy or player's HP reaches 0

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
