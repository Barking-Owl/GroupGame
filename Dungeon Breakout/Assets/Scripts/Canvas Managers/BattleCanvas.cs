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
    static public GameObject bmParent; //Reference to the battle manager object
    BattleManager bm = bmParent.GetComponent<BattleManager>();
    public Text levelTextbox; //textbox for level count
    public Text livesTextbox; //textbox for the lives
    public Text scoreTextbox; //textbox for the score
    public Text highScoreTextbox; //textbox for highscore
    
    //GM Data
    private int level;
    private int totalLevels;
    private int lives;
    private int score;
    private int highscore;

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
        lives = gm.Lives;
        score = gm.Score;
        highscore = gm.HighScore;
    }

    void SetHUD()
    {
        //if texbox exsists update value
        if (levelTextbox) { levelTextbox.text = "Level " + level + "/" + totalLevels; }
        if (livesTextbox) { livesTextbox.text = "Lives " + lives; }
        if (scoreTextbox) { scoreTextbox.text = "Score " + score; }
        if (highScoreTextbox) { highScoreTextbox.text = "High Score " + highscore; }

    }//end SetHUD()

}
