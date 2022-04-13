/**** 
 * Created by: Andrew Nguyen
 * Date Created: 4 April, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: 13 April, 2022
 * 
 * Description: General management for boss. May merge with EnemyScript
 * 
 * https://github.com/Barking-Owl/GroupGame/issues/3
****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    //Variables//

    [Header("SET IN INSPECTOR")]
    public float maxHP; //Maximum hitpoints
    public float hitpoints; //Hitpoints of the enemy, variable
    public float attack; //How strong the enemy's attacks are
    public float defense; //How much can they resist an attack
    public float speed; //How well can they evade attacks?

    void Awake()
    {

    } //end Awake()
    
    // Start is called before the first frame update
    void Start()
    {
        
    } //end Start()

    public void ScoreTally()
    {
        //GameManager.score = GameManager.score + maxHP * 3;
    } //end ScoreTally()

    // Update is called once per frame
    void Update()
    {
        if (hitpoints <= 0)
        {
            Destroy(this);
            ScoreTally();
        } 
    } //end Update()
}
