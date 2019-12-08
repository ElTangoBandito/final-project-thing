using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidController : MonoBehaviour
{
  private bool isDead;
  private bool reachedGoal;
  private int playerGroup;  //1: P1, 2: P2
  private int kidType;  //0: Normal, 1: Sniper, 2: Catcher, 3: Diagonal, 4: Charger
  private int laneNum;

  private int moveSpeedUnit;
  private int attackRangeUnit;

  private float movementSpeed;

  public void init(int playerGroup, int kidType, int laneNum)
  {
    GlobalsHolder globalVariable = GameObject.Find("GlobalVariableHolders").GetComponent<GlobalsHolder>();
    this.isDead = false;
    this.reachedGoal = false;
    this.playerGroup = playerGroup;
    this.kidType = kidType;
    this.movementSpeed = globalVariable.getKidTypeInfo(this.kidType).moveSpeedUnit * GlobalsHolder.kidMoveSpeed;
    this.laneNum = laneNum;
  }

  // Start is called before the first frame update
  void Start()
  {
    //test code
    // GlobalsHolder globalVariable = GameObject.Find("GlobalVariableHolders").GetComponent<GlobalsHolder>();
    // this.isDead = false;
    // this.reachedGoal = false;
    // this.playerGroup = 1;
    // this.kidType = 1;
    // this.movementSpeed = globalVariable.getKidTypeInfo(this.kidType).moveSpeedUnit * GlobalsHolder.kidMoveSpeed;
    // this.laneNum = 2;
    //end test code


    InvokeRepeating("throwBall", 2.0f, 1.0f);
  }

  // Update is called once per frame
  void Update()
  {
    this.transform.position += new Vector3(this.movementSpeed * (this.playerGroup == 1 ? 1 : -1), 0, 0) * Time.deltaTime;
  }

  public bool checkDead()
  {
    return this.isDead;
  }

  void throwBall()
  {
    GameObject instance = Instantiate(Resources.Load("Prefabs/Ball", typeof(GameObject))) as GameObject;
    instance.transform.position = this.transform.position + new Vector3(0.2f, 0.5f, -0.1f);
  }
}
