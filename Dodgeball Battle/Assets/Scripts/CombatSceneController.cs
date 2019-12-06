﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CombatSceneController : MonoBehaviour
{
    int player1GoalReachedNumber = 0; //3 to win.
    int player2GoalReachedNumber = 0;
    int kidsToWin = 3;

    List<GameObject> kidsArray = new List<GameObject>();
    //List<GameObject> ballsArray = new List<GameObject>();


    private bool player1PieceSelected = false;
    private int player1SelectedPiece = 0;
    private int player1SelectedLane = 0;
    private bool player2PieceSelected = false;
    private int player2SelectedPiece = 0;

    void resetPlayer1(){
        player1SelectedLane = 0;
        player1SelectedPiece = 0;
        player1PieceSelected = false;
    }

    void updateKidsStockUI(){
        GameObject myCanvas = GameObject.Find("Canvas");
        myCanvas.transform.Find("Kid1").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[0].ToString();
        myCanvas.transform.Find("Kid2").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[1].ToString();
        myCanvas.transform.Find("Kid3").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[2].ToString();
        myCanvas.transform.Find("Kid4").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[3].ToString();
        myCanvas.transform.Find("Kid5").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[4].ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        updateKidsStockUI();
        //Instantiate(kid, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //placeKidInPlane
        if(player1PieceSelected){
            if(Input.GetKeyDown("1")){
                player1SelectedLane = 1;
            } 
            else if (Input.GetKeyDown("2")){
                player1SelectedLane = 2;
            } 
            else if (Input.GetKeyDown("3")){
                player1SelectedLane = 3;
            } 
            else if (Input.GetKeyDown("4")){
                player1SelectedLane = 4;
            } 
            else if (Input.GetKeyDown("0")){
                player1SelectedLane = 0;
                resetPlayer1();
                print("Canceling Placement");
            }
            if(player1SelectedLane > 0){
                int zPosition = (player1SelectedLane - 1) * GlobalsHolder.zSpawnOffset;
                Vector3 spawnPos = GlobalsHolder.spawnPointPlayer1;
                spawnPos.z += zPosition;

                //check if space is occupied
                bool isSpawnTaken = false;
                for (int i = 0; i < kidsArray.Count; i++){
                    if(kidsArray[i].transform.position == spawnPos){
                        isSpawnTaken = true;
                    }
                }
                if(isSpawnTaken){
                    print("Spawn is occupied. Unable to spawn kid.");
                } else{
                    print("Placing kid " + player1SelectedPiece + " on lane " + player1SelectedLane);
                    string kidPrefabName = "Prefabs/" + GlobalsHolder.kidPrefabName;

                    GameObject instance = Instantiate(Resources.Load(kidPrefabName, typeof(GameObject))) as GameObject;
                    instance.transform.position = spawnPos;
                    kidsArray.Add(instance);

                    Player1Controller.kidStocks[player1SelectedPiece - 1]--;
                }
                
            }
        }

        //selectKid
        if (!player1PieceSelected){
            if(Input.GetKeyDown("1")){
                player1SelectedPiece = 1;
                print("Select kid 1");
            } else if (Input.GetKeyDown("2")){
                player1SelectedPiece = 2;
                print("Select kid 2");
            } else if (Input.GetKeyDown("3")){
                player1SelectedPiece = 3;
                print("Select kid 3");
            } else if (Input.GetKeyDown("4")){
                player1SelectedPiece = 4;
                print("Select kid 4");
            } else if (Input.GetKeyDown("5")){
                player1SelectedPiece = 5;
                print("Select kid 5");
            }
            if(player1SelectedPiece != 0 && Player1Controller.kidStocks[player1SelectedPiece - 1] == 0){
                print("Kid" + player1SelectedPiece + " is out of stock.");
                player1SelectedPiece = 0;
            }
        }
        if(player1SelectedLane > 0){
            resetPlayer1();
        }

        if(player1SelectedPiece > 0){
            player1PieceSelected = true;
        } else{
            resetPlayer1();
        }
        updateKidsStockUI();
    }
}
