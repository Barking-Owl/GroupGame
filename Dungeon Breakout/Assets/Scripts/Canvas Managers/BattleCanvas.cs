/**** 
 * Created by: Andrew Nguyen
 * Date Created: April 18, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: April 18, 2022
 * 
 * Description: Updates HUD Canvas referencing game manager. Manages battles
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCanvas : MonoBehaviour
{
    /*** VARIABLES ***/

    GameManager gm; //reference to game manager

    [Header("Canvas SETTINGS")]
    BattleManager bm;
    public Text levelTextbox; //textbox for level count
    public Text playerHealthTextbox; //textbox for the lives
    public Text enemyHealthTextbox; //textbox for the enemy
    public Text scoreTextbox; //textbox for the score
    public Text highScoreTextbox; //textbox for highscore
    
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



        SetHUD();
    }//end Start

    // Update is called once per frame
    void Update()
    {
        GetGameStats();
        SetHUD();

    }//end Update()

    void GetGameStats()
    {
        playerHealth = (int)bm.CAStats.hitpoints;
        enemyHealth = (int)bm.enemyStats.hitpoints;
        score = gm.Score;
        highscore = gm.HighScore;
    }

    void SetHUD()
    {
        //if texbox exsists update value
        if (levelTextbox) { levelTextbox.text = "Level " + level + "/" + totalLevels; }
        if (playerHealthTextbox) { playerHealthTextbox.text = "Health " + playerHealth; }
        if (scoreTextbox) { scoreTextbox.text = "Score " + score; }
        if (highScoreTextbox) { highScoreTextbox.text = "High Score " + highscore; }

    }//end SetHUD()

}
