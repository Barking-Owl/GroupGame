/**** 
 * Created by: Andrew Nguyen
 * Date Created: April 18, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: April 25, 2022
 * 
 * Description: Updates HUD Canvas referencing game manager. Manages battles
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleCanvas : MonoBehaviour
{
    /*** VARIABLES ***/

    GameManager gm; //reference to game manager
    GameObject[] battleHUDArray;

    [Header("Canvas SETTINGS")]
    BattleManager bm;
    public Text levelTextbox; //textbox for level count
    public Text playerHealthTextbox; //textbox for the lives
    public Text enemyHealthTextbox; //textbox for the enemy
    public Text scoreTextbox; //textbox for the score
    public Text highScoreTextbox; //textbox for highscore
    public Button itemsBox; //dynamic health for healing potions
    public Text itemsTextBox;
    public Text statusTextbox; //What is the enemy doing? Is it our turn?

    //GM Data, or BM. Health is displayed center screen.
    private int level;
    private int totalLevels;
    private int playerHealth;
    private int enemyHealth;
    private int score;
    private int highscore;

    private void Awake()
    {
        bm = GetComponent<BattleManager>();

    }
    private void Start()
    {
        gm = GameManager.GM; //find the game manager
        //reference to levle info
        level = gm.gameLevelsCount;
        totalLevels = gm.gameLevels.Length;
        itemsTextBox = itemsBox.gameObject.GetComponent<Text>();
        battleHUDArray = GameObject.FindGameObjectsWithTag("Player Battle Options");


        SetHUD();
    }//end Start

    // Update is called once per frame
    void Update()
    {
        GetGameStats();
        SetHUD();

        if (bm.State == BattleManager.battleState.EnemyTurn)
        {
            DisableBattleHUD();
        }
        else
        {
            EnableBattleHUD();
            statusTextbox.text = "Your turn.";
        }
    }//end Update()

    void EnableBattleHUD()
    {
        foreach (GameObject go in battleHUDArray)
        {
            go.SetActive(true);
        }
    } //end EnableBattleHUD()

    void DisableBattleHUD()
    {
        foreach (GameObject go in battleHUDArray)
        {
            go.SetActive(false);
        }
    } //end DisableBattleHUD()

    void GetGameStats()
    {
        playerHealth = (int)bm.CAStats.hitpoints;
        enemyHealth = (int)bm.enemyStats.hitpoints;
        score = gm.Score;
        highscore = gm.HighScore;
    } //end GetGameStats()

    void SetHUD()
    {
        //if texbox exsists update value
        if (levelTextbox) { levelTextbox.text = "Level " + level + "/" + totalLevels; }
        if (playerHealthTextbox) { playerHealthTextbox.text = "Player's Health " + playerHealth; }
        if (enemyHealthTextbox) { enemyHealthTextbox.text = "Enemy's Health " + enemyHealth; }
        if (scoreTextbox) { scoreTextbox.text = "Score " + score; }
        if (highScoreTextbox) { highScoreTextbox.text = "High Score " + highscore; }

        //New content - mainly heal needs to be taken care of as it will show how many health potions are left
        if (itemsTextBox) { itemsTextBox.text = "Heal (" + bm.CAStats.potions + ")"; }

    }//end SetHUD()

    //Go back to the previous scene
    public void Flee()
    {
        SceneManager.LoadScene(gm.lastScene);
    } //end Flee()

    //Raise Charlie Archer's defense for one turn
    public void Guard()
    {
        bm.playerGuard();
    } //end Guard()

    public void Attack()
    {
        Debug.Log("Attacking");
        bm.playerAttack();
    } //end Attack()

    public void Heal()
    {
        bm.playerHeal();
    } //end Heal()
}
