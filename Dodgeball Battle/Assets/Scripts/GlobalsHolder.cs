using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChildInfo
{
  public string type;
  public int moveSpeedUnit;
  public Vector3 attackRangeUnit;
  public int attackSpeedUnit;
}
public class GlobalsHolder : MonoBehaviour
{
  public static string kidPrefabName = "Child";
  public static int kidsToWin;
  public static int zSpawnOffset = 2;
  public static Vector3 rotationPlayer1 = new Vector3(0, 90, 0);
  public static Vector3 spawnPointPlayer1 = new Vector3(-22, 0, 0);
  public static Vector3 rotationPlayer2 = new Vector3(0, -90, 0);
  public static Vector3 spawnPointPlayer2 = new Vector3(22, 0, 0);

  public ChildInfo[] childTypes;
}
