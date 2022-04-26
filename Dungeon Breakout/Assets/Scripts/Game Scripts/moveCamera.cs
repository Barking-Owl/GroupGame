/**** 
 * Created by: Camp Steiner
 * Date Created: 25 April, 2022
 * 
 * Last Edited by: Camp Steiner
 * Last Edited: 25 April, 2022
 * 
 * Description: Switch between rooms in the main level
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetCamera(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCamera(int to, int from)
    {
        GameObject charles = GameObject.Find("Charlie");
        CharlieArcher CA = CharlieArcher.CA;
        float DEFAULT_SIZE = 5f;
        switch (to)
        {
            case 0:
                gameObject.GetComponent<Camera>().orthographicSize = DEFAULT_SIZE;
                gameObject.transform.position = new Vector3(0, 1, -10);
                CA.topLeftBound = new Vector2(-4.3f, 2.95f);
                CA.botRightBound = new Vector2(6.5f, -2.0f);
                switch (from)
                {
                    case 0:
                        charles.transform.position = new Vector3(0, 0, 0);
                        break;
                    case 1:
                        charles.transform.position = new Vector3(-4.3f, 1f, 0);
                        break;
                }
                break;
            case 1:
                gameObject.GetComponent<Camera>().orthographicSize = DEFAULT_SIZE;
                gameObject.transform.position = new Vector3(-20, 1, -10);
                CA.topLeftBound = new Vector2(-25f, 2.95f);
                CA.botRightBound = new Vector2(-15.47f, -2.0f);
                switch (from)
                {
                    case 0:
                        charles.transform.position = new Vector3(-15.47f, 1f, 0);
                        break;
                    case 2:
                        charles.transform.position = new Vector3(-21.3f, 2.79f, 0);
                        break;
                }
                break;
            case 2:
                gameObject.GetComponent<Camera>().orthographicSize = DEFAULT_SIZE;
                gameObject.transform.position = new Vector3(-20f, 12.5f, -10);
                CA.topLeftBound = new Vector2(-25f, 15f);
                CA.botRightBound = new Vector2(-15.47f, 9.9f);
                switch (from)
                { 
                    case 1:
                        charles.transform.position = new Vector3(-21.3f, 9.9f, 0);
                        break;
                }
                break;

        }
    }
}
