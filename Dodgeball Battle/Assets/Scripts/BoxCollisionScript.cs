using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollisionScript : MonoBehaviour
{
    private bool spawnBlocked;
    private int boxGroup;

    void OnTriggerStay(Collider c)
    {
        if (boxGroup == 1){
            if (c.gameObject.tag == "P1Kid"){
                spawnBlocked = true;
            } 
        } else if (boxGroup == 2){
            if (c.gameObject.tag == "P2Kid"){
                spawnBlocked = true;
            } 
        }
        //questionable checking by name method to prevent ball from scoring
        if (c.gameObject.name.Contains("kid")){// != "Ball(Clone)" && c.gameObject.name != "Ball"){
            if (boxGroup == 1){
                if(c.gameObject.tag == "P2Kid"){
                    print(c.gameObject.name);
                    Destroy(c.gameObject);
                    CombatSceneController.player2GoalReached();
                }
            } else if(boxGroup == 2){
                if(c.gameObject.tag == "P1Kid"){
                    Destroy(c.gameObject);
                    CombatSceneController.player1GoalReached();
                }
            }
        }
    }

    void OnTriggerExit(Collider c){
        if (boxGroup == 1){
            if (c.gameObject.tag == "P1Kid"){
                spawnBlocked = false;
            }
        } 
        if (boxGroup == 2){
            if (c.gameObject.tag == "P2Kid"){
                spawnBlocked = false;
            }
        } 
    }

    public void setGroup(int groupIn){
        boxGroup = groupIn;
    }

    public bool isSpawnBlocked(){
        return spawnBlocked;
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnBlocked = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
