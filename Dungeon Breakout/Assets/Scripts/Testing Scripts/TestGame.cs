/**** 
 * Created by: Akram Taghavi-Burris
 * Date Created: Feb 23, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Feb 26, 2022
 * 
 * Description: Check for score update [TESTINGS ONLY]
****/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TEST SCRIPT FOR CHECKING SCORE UPDATE
public class TestGame : MonoBehaviour
{
    public int point = 100; 

    // Update is called once per frame
    void Update()
    {
        //add points
        if (Input.GetKeyUp("return")) {
            GameManager.GM.UpdateScore(point);
        }

        //lose live
        if (Input.GetKeyUp("backspace"))
        {
            GameManager.GM.LostLife();
        }

    }//end Update()
}
