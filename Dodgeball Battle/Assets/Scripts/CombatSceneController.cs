using System.Collections;
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
    private int player2SelectedLane = 0;

    void resetPlayer1(){
        player1SelectedLane = 0;
        player1SelectedPiece = 0;
        player1PieceSelected = false;
    }

    void resetPlayer2(){
        player2SelectedLane = 0;
        player2SelectedPiece = 0;
        player2PieceSelected = false;
    }
    void updateKidsStockUI(){
        GameObject myCanvas = GameObject.Find("Canvas");
        myCanvas.transform.Find("Kid1").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[0].ToString();
        myCanvas.transform.Find("Kid2").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[1].ToString();
        myCanvas.transform.Find("Kid3").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[2].ToString();
        myCanvas.transform.Find("Kid4").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[3].ToString();
        myCanvas.transform.Find("Kid5").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[4].ToString();
        //player2Canvas update
        //myCanvas.transform.Find("Kid1").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[0].ToString();
        //myCanvas.transform.Find("Kid2").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[1].ToString();
        //myCanvas.transform.Find("Kid3").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[2].ToString();
        //myCanvas.transform.Find("Kid4").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[3].ToString();
        //myCanvas.transform.Find("Kid5").gameObject.GetComponent<Text>().text = Player1Controller.kidStocks[4].ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        updateKidsStockUI();
        //Instantiate(kid, transform.position, transform.rotation);
    }

    void getPlayer1Input(){
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
                float zPosition = (player1SelectedLane - 1) * GlobalsHolder.zSpawnOffset;
                Vector3 spawnPos = GlobalsHolder.spawnPointPlayer1;
                spawnPos.z += zPosition;

                //check if space is occupied
                bool isSpawnTaken = false;
                for (int i = 0; i < kidsArray.Count; i++){
                    if(kidsArray[i].transform.position.z == spawnPos.z && kidsArray[i].transform.position.x <= spawnPos.x + 0.35 && kidsArray[i].transform.position.x >= spawnPos.x){
                        isSpawnTaken = true;
                    }
                }
                if(isSpawnTaken){
                    print("Spawn is occupied. Unable to spawn kid.");
                } else{
                    print("Placing kid " + player1SelectedPiece + " on lane " + player1SelectedLane);
                    string kidPrefabName = "Prefabs/" + GlobalsHolder.kidPrefabsNameList[player1SelectedPiece - 1];

                    GameObject instance = Instantiate(Resources.Load(kidPrefabName, typeof(GameObject))) as GameObject;
                    instance.transform.position = spawnPos;
                    instance.transform.eulerAngles = GlobalsHolder.rotationPlayer1;
                    kidsArray.Add(instance);
                    instance.GetComponent<KidController>().init(1, player1SelectedPiece - 1, player1SelectedLane);
                    Player1Controller.kidStocks[player1SelectedPiece - 1]--;
                }
                
            }
        }

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
    }

    void getPlayer2Input(){
        if(player2PieceSelected){
            if(Input.GetKeyDown("[1]")){
                player2SelectedLane = 1;
            } 
            else if (Input.GetKeyDown("[2]")){
                player2SelectedLane = 2;
            } 
            else if (Input.GetKeyDown("[3]")){
                player2SelectedLane = 3;
            } 
            else if (Input.GetKeyDown("[4]")){
                player2SelectedLane = 4;
            } 
            else if (Input.GetKeyDown("[0]")){
                player2SelectedLane = 0;
                resetPlayer2();
                print("Canceling Placement");
            }
            if(player2SelectedLane > 0){
                float zPosition = (player2SelectedLane - 1) * GlobalsHolder.zSpawnOffset;
                Vector3 spawnPos = GlobalsHolder.spawnPointPlayer2;
                spawnPos.z += zPosition;

                //check if space is occupied
                bool isSpawnTaken = false;
                for (int i = 0; i < kidsArray.Count; i++){
                    if(kidsArray[i].transform.position.z == spawnPos.z && kidsArray[i].transform.position.x >= spawnPos.x - 0.35 && kidsArray[i].transform.position.x <= spawnPos.x){
                        isSpawnTaken = true;
                    }
                }
                if(isSpawnTaken){
                    print("Spawn is occupied. Unable to spawn kid.");
                } else{
                    print("Placing kid " + player2SelectedPiece + " on lane " + player2SelectedLane);
                    string kidPrefabName = "Prefabs/" + GlobalsHolder.kidPrefabsNameList[player2SelectedPiece - 1];

                    GameObject instance = Instantiate(Resources.Load(kidPrefabName, typeof(GameObject))) as GameObject;
                    instance.transform.position = spawnPos;
                    instance.transform.eulerAngles = GlobalsHolder.rotationPlayer2;
                    kidsArray.Add(instance);
                    instance.GetComponent<KidController>().init(2, player2SelectedPiece - 1, player2SelectedLane);
                    Player2Controller.kidStocks[player2SelectedPiece - 1]--;
                }
                
            }
        }

        if (!player2PieceSelected){
            if(Input.GetKeyDown("[1]")){
                player2SelectedPiece = 1;
                print("Select kid 1");
            } else if (Input.GetKeyDown("[2]")){
                player2SelectedPiece = 2;
                print("Select kid 2");
            } else if (Input.GetKeyDown("[3]")){
                player2SelectedPiece = 3;
                print("Select kid 3");
            } else if (Input.GetKeyDown("[4]")){
                player2SelectedPiece = 4;
                print("Select kid 4");
            } else if (Input.GetKeyDown("[5]")){
                player2SelectedPiece = 5;
                print("Select kid 5");
            }
            if(player2SelectedPiece != 0 && Player2Controller.kidStocks[player2SelectedPiece - 1] == 0){
                print("Kid" + player2SelectedPiece + " is out of stock.");
                player2SelectedPiece = 0;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //placeKidInPlane
        getPlayer1Input();
        getPlayer2Input();
        if(player1SelectedLane > 0){
            resetPlayer1();
        }

        if(player1SelectedPiece > 0){
            player1PieceSelected = true;
        } else{
            resetPlayer1();
        }

        if(player2SelectedLane > 0){
            resetPlayer2();
        }

        if(player2SelectedPiece > 0){
            player2PieceSelected = true;
        } else{
            resetPlayer2();
        }
        updateKidsStockUI();

        //add kids to remove list then remove by index from end of list.
        List<int> indexToRemove = new List<int>();
        for (int i = 0; i < kidsArray.Count; i++){
            if (kidsArray[i].GetComponent<KidController>().checkDead()){
                indexToRemove.Add(i);
            }
        }

        for (int i = indexToRemove.Count - 1; i >= 0; i--){
            kidsArray.RemoveAt(indexToRemove[i]);
        }
    }
}
