/**** 
 * Created by: Andrew Nguyen
 * Date Created: 4 April, 2022
 * 
 * Last Edited by: Camp Steiner
 * Last Edited: 23 April, 2022
 * 
 * Description: General management for player
 * 
 * https://github.com/Barking-Owl/GroupGame/issues/2
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharlieArcher : MonoBehaviour
{
    //Singleton
    #region CharlieArcher Singleton
    static private CharlieArcher ca; //refence GameManager
    static public CharlieArcher CA { get { return ca; } } //public access to read only gm 

    //Check to make sure only one of the PC is in the scene
    void CheckPCIsInScene()
    {

        //Check if instance is null
        if (ca == null)
        {
            ca = this; //set ca to this GameObject
            Debug.Log(ca);
        }
        else //else if gm is not null a Game Manager must already exsist
        {
            Destroy(this.gameObject); //In this case you need to delete this gm
        }
        DontDestroyOnLoad(this); //Do not delete the GameManager when scenes load
        Debug.Log(ca);
    }//end CheckPCIsInScene()
    #endregion

    //Variables//
    public float attack; // attack value of player. By default it's 10
    public float defense;
    public float speed;
    public float hitpoints;
    public int potions; //how many potions the PC has. Start with 3, get more from dead enemies.
    public GameObject explosionPrefab;
    public static bool isDisabled;

    private Animator animController;
    private SpriteRenderer spriteRender;
    public enum direction { UP=1, RIGHT, DOWN, LEFT };
    public direction lastDir;

    void Awake()
    {
        attack = 10f; //The attack value of the punch move.
        defense = 1.00f;
        speed = 2f;
        hitpoints = 100f;

        //charlie starts off facing down
        lastDir = direction.DOWN;
        animController = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        isDisabled = false;
    } //end Awake()

    // Start is called before the first frame update
    void Start()
    {
        
    } //end Start()

    // Update is called once per frame
    void Update()
    {
       
        if (isDisabled) return;
        if (hitpoints <= 0 )
        {
            Destroy(this); //Death animation
        }

        //movement
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
        //send stuff to blend tree for animation
        animController.SetFloat("xAxis", x);
        animController.SetFloat("yAxis", y);

        //flip sprite if we are going left
        spriteRender.flipX = (x < 0);

        Vector3 pos = transform.position;
        pos.x += x * speed * Time.deltaTime;
        pos.y += y * speed * Time.deltaTime;
        transform.position = pos;


        //set idle animation based on direction traveling
        if(x > 0) //heading right
        {
            if (y == 0) lastDir = direction.RIGHT; //player moving solely on x axis
            else
            {
                if (y > 0) lastDir = (x > y) ? direction.RIGHT : direction.UP; //if more up than right, up / if more right than up, right
                if (y < 0) lastDir = (x > -y) ? direction.RIGHT : direction.DOWN; //same but for down
            }
        }
        if(x < 0)
        {
            if (y == 0) lastDir = direction.LEFT; //player moving solely on x axis
            else
            {
                if (y > 0) lastDir = (-x > y) ? direction.LEFT : direction.UP; //if more up than left, up / if more left than up, left
                if (y < 0) lastDir = (-x > -y) ? direction.LEFT : direction.DOWN; //same but for down
            }
        }
        if (x == 0) //player moving solely on y axis
        {
            if (y > 0) lastDir = direction.UP;
            if (y < 0) lastDir = direction.DOWN;
        }

        animController.SetFloat("lastDir", (float)lastDir);
        //oh how i wish for a left facing animation
        if (lastDir == direction.LEFT) spriteRender.flipX = true;

        //skills
        //maybe want to rework these to use the Unity Input Axes so controllers would work by default?
        if (Input.GetKeyDown(KeyCode.Space))
        { //main attack
            GameObject explosion = Instantiate<GameObject>(explosionPrefab);
            explosion.transform.localScale *= 0.2f;
            //set position to be the players
            //plus offset for if the player is moving
            //plus offset for direction facing (because x = 2,4 and y = 1,3)
            int ld = (int)lastDir;
            explosion.transform.position = gameObject.transform.position + (new Vector3(x, y, 0) * 0.5f) + (new Vector3( ((ld+1)%2) * -(ld-3), (ld%2) * -(ld-2), 0));
            //store reference to charlie on the explosion
            explosion.GetComponent<Explosion>().summoner = this.gameObject;
        }

    } //end Update()
}
