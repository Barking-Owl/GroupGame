/**** 
 * Created by: Akram Taghavi-Burris
 * Date Created: Feb 23, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: April 25, 2022
 * 
 * Description: Updates HUD Canvas referencing game manager. Level_01 is the battle screen. This script manages Overworld.
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCanvas : MonoBehaviour
{
    /*** VARIABLES ***/

    GameManager gm; //reference to game manager
    public GameObject ca; //reference to pc
    CharlieArcher CAScript;

    [Header("Canvas SETTINGS")]
    public Text levelTextbox; //textbox for level count
    public Text playerHealthTextbox; //textbox for the lives
    public Text scoreTextbox; //textbox for the score
    public Text highScoreTextbox; //textbox for highscore

    //GM Data
    private int level;
    private int totalLevels;
    private int health;
    private int score;
    private int highscore;

    private void Start()
    {
        gm = GameManager.GM; //find the game manager
        ca = GameObject.Find("Charlie");
        CAScript = ca.GetComponent<CharlieArcher>();

        //reference to levle info
        level = gm.gameLevelsCount;
        totalLevels = gm.gameLevels.Length;
        health = 100;

        Debug.Log(CAScript.hitpoints);

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
        health = 100;
        score = gm.Score;
        highscore = gm.HighScore;
    }

    void SetHUD()
    {
        //if texbox exsists update value
        if (levelTextbox) { levelTextbox.text = "Level " + level + "/" + totalLevels; }
        if (playerHealthTextbox) { playerHealthTextbox.text = "Player Health " + health; }
        if (scoreTextbox) { scoreTextbox.text = "Score " + score; }
        if (highScoreTextbox) { highScoreTextbox.text = "High Score " + highscore; }
    }//end SetHUD()

}
