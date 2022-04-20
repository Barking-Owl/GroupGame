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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExplosionEnd()
    {
        Destroy(gameObject);
    }
}
