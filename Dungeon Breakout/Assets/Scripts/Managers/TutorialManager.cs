/**** 
 * Created by: Camp Steiner
 * Date Created: 20 April, 2022
 * 
 * Last Edited by: Camp Steiner
 * Last Edited: 25 April, 2022
 * 
 * Description: Tutorial to teach the player controls and basic battle
 * 
****/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public int step;
    //stuff for various stages of the tutorial
    private bool left, right, up, down;
    private int count;
    

    [Header("Settings")]
    public Text tutorialText;
    public Color defaultTextColor;
    public int defaultFontSize;
    public GameObject enemyPrefab;
    public GameObject exitDoor;


    
    // Start is called before the first frame update
    void Start()
    {
        //tutorialText.color = defaultTextColor;
        //tutorialText.fontSize = defaultFontSize;
        //initial tutorial text;
        tutorialText.text = "Welcome, Adventurer!\nYou are Charlie Archer, an unfortunately named mage.\n\nMove using <color='#6363d5'>WASD</color> or the arrow keys.";
        //set up variables
        left = right = up = down = false;
        step = GameManager.GM.tutorialStage;
        GameManager.GM.tutorialActive = true;
        AdvanceTutorial();
        CharlieArcher.CA.topLeftBound = new Vector2(-8.9f, 4.75f);
        CharlieArcher.CA.botRightBound = new Vector2(8.9f,-3.31f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (step) //hopefully cut down some on computation by not checking all of these if not necessary
        {
            case 0:
                float x = Input.GetAxis("Horizontal");
                float y = Input.GetAxis("Vertical");
                if (x > 0) right = true;
                if (x < 0) left = true;
                if (y > 0) up = true;
                if (y < 0) down = true;
                if (up && down && left && right)
                {
                    step++;
                    AdvanceTutorial();
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    step++;
                    AdvanceTutorial();
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Space)) count++;
                if (count > 3)
                {
                    step++;
                    AdvanceTutorial();
                }
                break;
            /*case 4:
                Vector3 caPos = CharlieArcher.CA.gameObject.transform.position;
                if (caPos.x > 8.8f || caPos.x < -8.8f || caPos.y > 6.0f || caPos.y < -3.5f)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("dungeon_01");
                }
                break;*/
            default:
                return;
        }

    }

    void AdvanceTutorial()
    {
        switch (step) 
        {
            case 1:
                tutorialText.text = "Well done!\nYou have been trapped in this dungeon by a mysterious entity, with only rumors of a locked door to the exit somewhere.\nYou can cast an explosion using <color='#6f0000'>Space</color>";
                break;
            case 2:
                tutorialText.text = "There are enemies wandering this dungeon as well.\nBetter practice your attack a few more times, just to be safe.";
                break;
            case 3:
                GameObject enemy = Instantiate(enemyPrefab);
                //GameObject enemy = Instantiate(PrefabManager.Instance.EnemyPrefab);
                enemy.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-2f, 0f), 0);
                enemy.GetComponent<SpriteRenderer>().flipX = true;
                tutorialText.text = "Here comes an enemy now!\nCast <color='#6f0000'>Explode</color> on it to enter into battle!";
                GameManager.GM.tutorialStage = step;
                break;
            case 4:
                GameManager.GM.playerRef = null;
                GameManager.GM.enemyRef = null;
                tutorialText.text = "Well done! You vanquished the enemy!\nYou are now ready to begin your real quest.\nBe on your way now, and good luck escaping the dungeon.\nCast <color='#6f0000'>Explode</color> on the <color='#A27C5B'>door</color> to leave.";
                exitDoor.SetActive(true);
                GameManager.GM.tutorialActive = false;
                break;
        }
    }
}
