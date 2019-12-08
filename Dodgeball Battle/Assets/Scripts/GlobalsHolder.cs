using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KidInfo
{
  public string type;
  public int moveSpeedUnit;
  public int attackRangeUnit;

  public KidInfo(string type, int moveSpeedUnit, int attackRangeUnit)
  {
    this.type = type;
    this.moveSpeedUnit = moveSpeedUnit;
    this.attackRangeUnit = attackRangeUnit;
  }
}
public class GlobalsHolder : MonoBehaviour
{
  public static string kidPrefabName = "normalKid";
  public static int kidsToWin;
  public static float zSpawnOffset = 1.2f;
  public static Vector3 rotationPlayer1 = new Vector3(0, 90, 0);
  public static Vector3 spawnPointPlayer1 = new Vector3(-1, 0, 0);
  public static Vector3 rotationPlayer2 = new Vector3(0, -90, 0);
  public static Vector3 spawnPointPlayer2 = new Vector3(22, 0, 0);

  public static float kidMoveSpeed = 1.0f;

  private KidInfo[] kidTypes = new KidInfo[5];

  private void Awake()
  {
    kidTypes[0] = new KidInfo("normal", 2, 2);
    kidTypes[1] = new KidInfo("sniper", 1, 6);
    kidTypes[2] = new KidInfo("catcher", 2, 0);
    kidTypes[3] = new KidInfo("diagonal", 2, 2);
    kidTypes[4] = new KidInfo("charger", 3, 1);
  }

  public KidInfo getKidTypeInfo(int typeId){
    return kidTypes[typeId];
  }
}
