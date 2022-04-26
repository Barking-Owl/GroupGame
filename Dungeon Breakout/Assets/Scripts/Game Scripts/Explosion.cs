/**** 
 * Created by: Andrew Nguyen
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Camp Steiner
 * Last Edited: April 25, 2022
 * 
 * Description: Manages the explosion
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject summoner;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GM;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == other.tag || CharlieArcher.isDisabled || other.tag == "Wall") return; //don't collide with self/allies
        GameObject otherGO = other.gameObject;
        if (other.tag == "Door")
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "dungeon_00")
            {
                gm.tutorialActive = false;
                UnityEngine.SceneManagement.SceneManager.LoadScene("dungeon_01");
                return;
            }
            else
            {
                doorScript ds = otherGO.GetComponent<doorScript>();
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<moveCamera>().SetCamera(ds.destination, ds.source);
            }
        }

        //set reference to be used in battlemanager

        if (other.tag == "Enemy")
        {

            gm.playerRef = summoner;
            gm.enemyRef = otherGO;

            //go to battle scene
            gm.GoBattle();
        }
    }
    public void ExplosionEnd()
    {
        Destroy(gameObject);
    }
}
