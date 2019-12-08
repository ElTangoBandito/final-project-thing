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
  public static List<string> kidPrefabsNameList = new List<string>();
  public static string kidPrefabName = "normalKid";
  public static int kidsToWin;
  public static float zSpawnOffset = 1.2f;
  public static Vector3 rotationPlayer1 = new Vector3(0, 90, 0);
  public static Vector3 spawnPointPlayer1 = new Vector3(-1, 0, 0);
  public static Vector3 rotationPlayer2 = new Vector3(0, -90, 0);
  public static Vector3 spawnPointPlayer2 = new Vector3(11, 0, 0);
  public static Vector3 goalLineOnPlayer1 = new Vector3();
  public static Vector3 goalLineOnPlayer2 = new Vector3();

  public static float kidMoveSpeed = 1.0f;

  public static float ballMoveSpeed = 2.0f;

  private KidInfo[] kidTypes = new KidInfo[5];

  private void Awake()
  {
    goalLineOnPlayer1 = new Vector3(spawnPointPlayer1.x + 1, spawnPointPlayer1.y, spawnPointPlayer1.z);
    goalLineOnPlayer2 = new Vector3(spawnPointPlayer2.x - 1, spawnPointPlayer2.y, spawnPointPlayer2.z);

    kidPrefabsNameList.Add("Normal");
    kidPrefabsNameList.Add("Sniper");
    kidPrefabsNameList.Add("Catcher");
    kidPrefabsNameList.Add("Diagonal");
    kidPrefabsNameList.Add("Charger");

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
