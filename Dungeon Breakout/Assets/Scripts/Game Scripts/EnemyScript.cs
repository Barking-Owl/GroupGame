/**** 
 * Created by: Andrew Nguyen
 * Date Created: 4 April, 2022
 * 
 * Last Edited by: Tyrese Peoples
 * Last Edited: 25 April, 2022
 * 
 * Description: General management for generic enemy
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //Variables//

    [Header("SET IN INSPECTOR")]
    public float maxHP; //Maximum hitpoints
    public float hitpoints; //Hitpoints of the enemy, variable
    public float attack; //How strong the enemy's attacks are
    public float defense; //How much can they resist an attack
    public float speed; //How well can they evade attacks?

    private boundsCheck bndCheck;

    public Vector3 pos
    {
        get { return (this.transform.position); }
        set { this.transform.position = value; }
    }

    public virtual void Move()
    {
        Vector3 temPos = pos;
        temPos.y -= speed * Time.deltaTime;
        pos = temPos;
    } // end Move()

    void Awake()
    {
        hitpoints = maxHP;

        bndCheck = GetComponent<boundsCheck>();
    } //end Awake()

    // Start is called before the first frame update
    void Start()
    {
        
    } //end Start()

    public void ScoreTally()
    {
        GameManager.score = GameManager.score + (int)maxHP*3;
    }

    // Update is called once per frame
    void Update()
    {
       // Move();

        if(bndCheck != null && bndCheck.offDown)
        {
            //if statement for changing enemy direction before it leaves screen
        }

        if (hitpoints <= 0)
        {
            Destroy(this);
            ScoreTally();
        }
    } //end Update()


}
