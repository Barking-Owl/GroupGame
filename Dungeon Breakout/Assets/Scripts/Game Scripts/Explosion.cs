/**** 
 * Created by: Andrew Nguyen
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: April 20, 2022
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
        if (gameObject.tag == other.tag || CharlieArcher.isDisabled) return; //don't collide with self/allies
        GameObject otherGO = other.gameObject;
        
        //set reference to be used in battlemanager
        gm.playerRef = summoner;
        gm.enemyRef = otherGO;
        
        //go to battle scene
        gm.GoBattle();
    }
    public void ExplosionEnd()
    {
        Destroy(gameObject);
    }
}
