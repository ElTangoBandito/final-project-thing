using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
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
  }

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    this.transform.position += new Vector3(this.movementSpeed,0,0);
    if (this.kidType == 2)
    {

    }
    else
    {

    }
  }

  public bool checkDead()
  {
    return this.isDead;
  }
}
