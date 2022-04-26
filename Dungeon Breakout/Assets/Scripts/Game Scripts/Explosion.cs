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
        if (gameObject.tag == other.tag) return; //don't collide with self/allies
        //need to keep the two battlers around in the next scene
        DontDestroyOnLoad(summoner);
        DontDestroyOnLoad(other.gameObject);
        //set reference to be used in battlemanager
<<<<<<< Updated upstream
        //maybe want to look into saving stats instead of whole gameobject?
        gm.playerRef = summoner;
        gm.enemyRef = other.gameObject;
        //hide in the next scene
        summoner.GetComponent<SpriteRenderer>().enabled = false;
        other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //go to battle scene
        gm.GoBattle();
=======
        if(other.tag == "Enemy")
        {
            gm.playerRef = summoner;
            gm.enemyRef = otherGO;

            //go to battle scene
            gm.GoBattle();
        }

>>>>>>> Stashed changes
    }
    public void ExplosionEnd()
    {
        Destroy(gameObject);
    }
}
