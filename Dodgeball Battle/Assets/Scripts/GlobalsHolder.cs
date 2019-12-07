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
  public static string kidPrefabName = "normalKid";
  public static int kidsToWin;
  public static float zSpawnOffset = 1.2f;
  public static Vector3 rotationPlayer1 = new Vector3(0, 90, 0);
  public static Vector3 spawnPointPlayer1 = new Vector3(-1, 0, 0);
  public static Vector3 rotationPlayer2 = new Vector3(0, -90, 0);
  public static Vector3 spawnPointPlayer2 = new Vector3(22, 0, -1);

  public ChildInfo[] childTypes;
}
