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

  public bool invisible = false;

  public void init(int playerGroup, int kidType, int laneNum)
  {
    GlobalsHolder globalVariable = GameObject.Find("GlobalVariableHolders").GetComponent<GlobalsHolder>();
    this.isDead = false;
    this.reachedGoal = false;
    this.playerGroup = playerGroup;
    this.kidType = kidType;
    if (this.kidType == 2) this.invisible = true;
    this.gameObject.tag = (playerGroup == 1 ? "P1Kid" : "P2Kid");
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

    if (this.kidType != 2 && this.kidType != 4)
    {
      InvokeRepeating("throwBall", 1.0f, 1.0f);
    }
    if (this.kidType == 4)
    {
      GameObject chargerBall = this.transform.Find("Ball").gameObject;
      if (chargerBall != null)
      {
        chargerBall.GetComponent<BallController>().init(this.playerGroup, this.kidType, new Vector3(0, 0, 0), this.gameObject);
      }
    }
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
    if (this.kidType == 3)
    {
      GlobalsHolder globalVariable = GameObject.Find("GlobalVariableHolders").GetComponent<GlobalsHolder>();
      Vector3 ball1Movement = new Vector3(globalVariable.getKidTypeInfo(kidType).attackRangeUnit * GlobalsHolder.ballMoveSpeed * (this.playerGroup == 1 ? 1 : -1), 0, globalVariable.getKidTypeInfo(kidType).attackRangeUnit * GlobalsHolder.ballMoveSpeed);
      GameObject ball1 = Instantiate(Resources.Load("Prefabs/Ball", typeof(GameObject))) as GameObject;
      ball1.transform.position = this.transform.position + new Vector3(0.0f, 0.5f, 0.37f);
      ball1.GetComponent<BallController>().init(this.playerGroup, this.kidType, ball1Movement, this.gameObject);

      Vector3 ball2Movement = new Vector3(globalVariable.getKidTypeInfo(kidType).attackRangeUnit * GlobalsHolder.ballMoveSpeed * (this.playerGroup == 1 ? 1 : -1), 0, globalVariable.getKidTypeInfo(kidType).attackRangeUnit * GlobalsHolder.ballMoveSpeed * -1);
      GameObject ball2 = Instantiate(Resources.Load("Prefabs/Ball", typeof(GameObject))) as GameObject;
      ball2.transform.position = this.transform.position + new Vector3(0.0f, 0.5f, -0.37f);
      ball2.GetComponent<BallController>().init(this.playerGroup, this.kidType, ball2Movement, this.gameObject);
    }
    // else if (this.kidType == 4)
    // {
    //   GlobalsHolder globalVariable = GameObject.Find("GlobalVariableHolders").GetComponent<GlobalsHolder>();
    //   Vector3 ballMovement = new Vector3(globalVariable.getKidTypeInfo(kidType).attackRangeUnit * GlobalsHolder.ballMoveSpeed * (this.playerGroup == 1 ? 1 : -1), 0, 0);
    //   GameObject ball = Instantiate(Resources.Load("Prefabs/Ball", typeof(GameObject))) as GameObject;
    //   ball.transform.position = this.transform.position + new Vector3(0.25f * (this.playerGroup == 1 ? 1 : -1), 0.5f, 0.0f);
    //   ball.gameObject.GetComponent<Rigidbody>().useGravity = false;
    //   ball.GetComponent<BallController>().init(this.playerGroup, this.kidType, ballMovement);
    // }
    else
    {
      GlobalsHolder globalVariable = GameObject.Find("GlobalVariableHolders").GetComponent<GlobalsHolder>();
      Vector3 ballMovement = new Vector3(globalVariable.getKidTypeInfo(kidType).attackRangeUnit * GlobalsHolder.ballMoveSpeed * (this.playerGroup == 1 ? 1 : -1), 0, 0);
      GameObject ball = Instantiate(Resources.Load("Prefabs/Ball", typeof(GameObject))) as GameObject;
      ball.transform.position = this.transform.position + new Vector3(0.25f * (this.playerGroup == 1 ? 1 : -1), 0.5f, 0.0f);
      ball.GetComponent<BallController>().init(this.playerGroup, this.kidType, ballMovement, this.gameObject);
    }
  }

  public int getKidType()
  {
    return this.kidType;
  }

  public void setInvisible(){
    if(this.invisible == true){
      this.invisible = false;
      Invoke("setInvisible", 1.0f);
    }else{
      this.invisible = true;
    }
  }
}
