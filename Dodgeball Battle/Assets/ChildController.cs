using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    private bool isDead;
    private int playerGroup;  //1: P1, 2: P2
    private int childType;  //0: Normal, 1: Sniper, 2: Catcher, 3: Diagonal, 4: Charger
    private int laneNum;

    private float movementSpeed;

    // Start is called before the first frame update
    void Start(int playerGroup, int childType, int laneNum)
    {
        this.isDead = false;
        this.playerGroup = playerGroup;
        this.childType = childType;
        this.laneNum = laneNum;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.childType==2){

        }else{

        }
    }

    public bool checkDead(){
        return this.isDead;
    }
}
