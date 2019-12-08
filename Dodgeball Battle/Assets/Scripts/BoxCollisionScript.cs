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
