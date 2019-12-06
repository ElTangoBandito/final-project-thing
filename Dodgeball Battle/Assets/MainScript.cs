using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainScript : MonoBehaviour
{
    int player1GoalReachedNumber = 0; //3 to win.
    int player2GoalReachedNumber = 0;

    


    //public GameObject kid;
    bool player1PieceSelected = false;
    int player1SelectedPiece = 0;
    int player1SelectedLane = 0;
    bool player2PieceSelected = false;
    int player2SelectedPiece = 0;

    void resetPlayer1(){
        player1SelectedLane = 0;
        player1SelectedPiece = 0;
        player1PieceSelected = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(kid, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //placeKidInPlane
        if(player1PieceSelected){
            if(Input.GetKeyDown("1")){
                //kid = Instantiate(Resources.Load("Kid1")) as GameObject;
                //kid.transform.position = new Vector3(0,0,0);
                player1SelectedLane = 1;
                print("Placing kid " + player1SelectedPiece + " on lane 1");
            } 
            else if (Input.GetKeyDown("2")){
                player1SelectedLane = 2;
                print("Placing kid " + player1SelectedPiece + " on lane 2");
            } 
            else if (Input.GetKeyDown("3")){
                player1SelectedLane = 3;
                print("Placing kid " + player1SelectedPiece + " on lane 3");
            } 
            else if (Input.GetKeyDown("4")){
                player1SelectedLane = 4;
                print("Placing kid " + player1SelectedPiece + " on lane 4");
            } 
            else if (Input.GetKeyDown("0")){
                player1SelectedLane = 0;
                resetPlayer1();
                print("Canceling Placement");
            }
            string kidPrefabName = "Prefabs/Kid" + player1SelectedPiece;
            if(player1SelectedLane > 0){
                GameObject instance = Instantiate(Resources.Load(kidPrefabName, typeof(GameObject))) as GameObject;
                int yPosition = (player1SelectedLane - 1) * 2;
                instance.transform.position = new Vector3(0,0,yPosition);
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
        }
        if(player1SelectedLane > 0){
            resetPlayer1();
        }

        if(player1SelectedPiece > 0){
            player1PieceSelected = true;
        } else{
            resetPlayer1();
        }
    }
}
