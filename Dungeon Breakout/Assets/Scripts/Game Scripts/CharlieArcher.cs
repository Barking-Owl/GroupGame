/**** 
 * Created by: Andrew Nguyen
 * Date Created: 4 April, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: 4 April, 2022
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

    void Awake()
    {
        attack = 10f; //The attack value of the punch move.
        defense = 1.00f;
        speed = 5f;
        hitpoints = 100f;
    } //end Awake()

    // Start is called before the first frame update
    void Start()
    {
        
    } //end Start()

    // Update is called once per frame
    void Update()
    {
        if (hitpoints <= 0 )
        {
            Destroy(this); //Death animation
        }
    } //end Update()
}