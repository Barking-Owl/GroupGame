/**** 
 * Created by: Andrew Nguyen
 * Date Created: 11 April, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: 11 April, 2022
 * 
 * Description: Manages battles. Communicates with Inventory, UI, Player, and Enemies.
 * 
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //New battlemanager with player then enemy, make a battle manager for these two enemies to fight. Get their stats, and have them fight.

    //Variables//
    public bool playerTurn; //How can we discern if it is the player's turn?
    public bool isBattling;
    public CharlieArcher player; //Reference to the player
    public EnemyScript enemy; //Reference to the enemy

    //References to Player and Enemy

    // Start is called before the first frame update
    void Start()
    {
        //Get player and enemy HP

    }

    // Update is called once per frame
    void Update()
    {
        //Set up a turn system, exit the battle if the enemy or player's HP reaches 0
    }
}
