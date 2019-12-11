using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CombatSceneController : MonoBehaviour
{
    private static int player1GoalReachedNumber = 0; //3 to win.
    private static int player2GoalReachedNumber = 0;
    private int kidsToWin = 3;


    //List<GameObject> kidsArray = new List<GameObject>();
    //List<GameObject> ballsArray = new List<GameObject>();

    private int kidID = 0;
    private bool player1PieceSelected = false;
    private int player1SelectedPiece = 0;
    private int player1SelectedLane = 0;
    private List<GameObject> player1CollisionBoxes = new List<GameObject>();
    private bool player2PieceSelected = false;
    private int player2SelectedPiece = 0;
    private int player2SelectedLane = 0;
    private List<GameObject> player2CollisionBoxes = new List<GameObject>();

    private bool gameOver = false;
    private string gameOverMessage = "";

    public AudioSource spawnSound;
    public AudioSource outOfStockSound;
    public AudioSource goalSound;
    public AudioSource unselectSound;

    public AudioSource hitSound;

    public static AudioSource kidHitSound;
    private static AudioSource goalReachedSound;
    public static void player1GoalReached()
    {
        goalReachedSound.Play();
        player1GoalReachedNumber++;
        GameObject myCanvas = GameObject.Find("Canvas");
        myCanvas.transform.Find("Player1Goal").gameObject.GetComponent<Text>().text = player1GoalReachedNumber.ToString();
    }
    public static void player2GoalReached()
    {
        goalReachedSound.Play();
        player2GoalReachedNumber++;
        GameObject myCanvas = GameObject.Find("Canvas");
        myCanvas.transform.Find("Player2Goal").gameObject.GetComponent<Text>().text = player2GoalReachedNumber.ToString();
    }

    void checkWinner()
    {
        int winner = 0;
        /*
        if (player1GoalReachedNumber == kidsToWin && player2GoalReachedNumber == kidsToWin)
        {
            gameOverMessage = "Its a tie.";
        }
        else if (player1GoalReachedNumber == kidsToWin)
        {
            gameOverMessage = "Player 1 has won";
            winner = 1;
        }
        else if (player2GoalReachedNumber == kidsToWin)
        {
            gameOverMessage = "Player 2 has won";
            winner = 2;
        }
        */
        bool player1StockIsEmpty = true;
        bool player2StockIsEmpty = true;
        for (int i = 0; i < Player1Controller.kidStocks.Count; i++)
        {
            if (Player1Controller.kidStocks[i] > 0)
            {
                player1StockIsEmpty = false;
            }
        }
        for (int i = 0; i < Player2Controller.kidStocks.Count; i++)
        {
            if (Player2Controller.kidStocks[i] > 0)
            {
                player2StockIsEmpty = false;
            }
        }
        bool sceneHasKid = false;
        GameObject[] gameObjectArray = (GameObject[])FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < gameObjectArray.Length; i++)
        {
            if (gameObjectArray[i].name.Contains("kid"))
            {
                sceneHasKid = true;
            }
        }
        if (player1StockIsEmpty && player2StockIsEmpty && !sceneHasKid)
        {
            if (player1GoalReachedNumber == player2GoalReachedNumber)
            {
                gameOverMessage = "Its a tie.";
            }
            else if (player1GoalReachedNumber > player2GoalReachedNumber)
            {
                gameOverMessage = "Player 1 has won";
                winner = 1;
            }
            else if (player2GoalReachedNumber > player1GoalReachedNumber)
            {
                gameOverMessage = "Player 2 has won";
                winner = 2;
            }
        }
        if (!(string.Equals(gameOverMessage, "")) && !gameOver)
        {
            gameOver = true;
            GiveMeWinner.winner = winner;
            //print(gameOverMessage);
        }

    }
    void resetPlayer1()
    {
        player1SelectedLane = 0;
        player1SelectedPiece = 0;
        player1PieceSelected = false;
    }

    void resetPlayer2()
    {
        player2SelectedLane = 0;
        player2SelectedPiece = 0;
        player2PieceSelected = false;
    }
    
    void resetGameState(){
        player1GoalReachedNumber = 0;
        player2GoalReachedNumber = 0;
        gameOver = false;
        GameObject myCanvas = GameObject.Find("Canvas");
        myCanvas.transform.Find("Player1Goal").gameObject.GetComponent<Text>().text = player1GoalReachedNumber.ToString();
        myCanvas.transform.Find("Player2Goal").gameObject.GetComponent<Text>().text = player2GoalReachedNumber.ToString();
    }
    void updateKidsStockUI()
    {
        // J/E: {"Normal", "Sniper", "Catcher", "Twoballs", "Runner"}
        GameObject myCanvas = GameObject.Find("Canvas");
        myCanvas.transform.Find("Kid1").gameObject.GetComponent<Text>().text = GlobalsHolder.kidNames[0] + ": " + Player1Controller.kidStocks[0].ToString();
        myCanvas.transform.Find("Kid2").gameObject.GetComponent<Text>().text = GlobalsHolder.kidNames[1] + ": " + Player1Controller.kidStocks[1].ToString();
        myCanvas.transform.Find("Kid3").gameObject.GetComponent<Text>().text = GlobalsHolder.kidNames[2] + ": " + Player1Controller.kidStocks[2].ToString();
        myCanvas.transform.Find("Kid4").gameObject.GetComponent<Text>().text = GlobalsHolder.kidNames[3] + ": " + Player1Controller.kidStocks[3].ToString();
        myCanvas.transform.Find("Kid5").gameObject.GetComponent<Text>().text = GlobalsHolder.kidNames[4] + ": " + Player1Controller.kidStocks[4].ToString();
        //player2Canvas update
        myCanvas.transform.Find("P2Kid1").gameObject.GetComponent<Text>().text = GlobalsHolder.kidNames[0] + ": " + Player2Controller.kidStocks[0].ToString();
        myCanvas.transform.Find("P2Kid2").gameObject.GetComponent<Text>().text = GlobalsHolder.kidNames[1] + ": " + Player2Controller.kidStocks[1].ToString();
        myCanvas.transform.Find("P2Kid3").gameObject.GetComponent<Text>().text = GlobalsHolder.kidNames[2] + ": " + Player2Controller.kidStocks[2].ToString();
        myCanvas.transform.Find("P2Kid4").gameObject.GetComponent<Text>().text = GlobalsHolder.kidNames[3] + ": " + Player2Controller.kidStocks[3].ToString();
        myCanvas.transform.Find("P2Kid5").gameObject.GetComponent<Text>().text = GlobalsHolder.kidNames[4] + ": " + Player2Controller.kidStocks[4].ToString();
    }
    // Start is called before the first frame update

    void spawnCollisionBoxesPlayer1()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject collisionBox = Instantiate(Resources.Load("Prefabs/CollisionBox", typeof(GameObject)) as GameObject);
            Vector3 boxPos = GlobalsHolder.spawnPointPlayer1;
            boxPos.z += GlobalsHolder.zSpawnOffset * i;
            collisionBox.transform.position = boxPos;
            collisionBox.GetComponent<BoxCollisionScript>().setGroup(1);
            player1CollisionBoxes.Add(collisionBox);
        }
    }

    void spawnCollisionBoxesPlayer2()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject collisionBox = Instantiate(Resources.Load("Prefabs/CollisionBox", typeof(GameObject)) as GameObject);
            Vector3 boxPos = GlobalsHolder.spawnPointPlayer2;
            boxPos.z += GlobalsHolder.zSpawnOffset * i;
            collisionBox.transform.position = boxPos;
            collisionBox.GetComponent<BoxCollisionScript>().setGroup(2);
            player2CollisionBoxes.Add(collisionBox);
        }
    }

    void Awake(){
        spawnCollisionBoxesPlayer1();
        spawnCollisionBoxesPlayer2();
        goalReachedSound = goalSound;
        kidHitSound = hitSound;
    }

    void Start()
    {
        resetGameState();
        updateKidsStockUI();
        //Instantiate(kid, transform.position, transform.rotation);
    }

    void getPlayer1Input()
    {
        if (player1PieceSelected)
        {
            if (Input.GetKeyDown("1"))
            {
                player1SelectedLane = 1;
            }
            else if (Input.GetKeyDown("2"))
            {
                player1SelectedLane = 2;
            }
            else if (Input.GetKeyDown("3"))
            {
                player1SelectedLane = 3;
            }
            else if (Input.GetKeyDown("4"))
            {
                player1SelectedLane = 4;
            }
            else if (Input.GetKeyDown("0"))
            {
                player1SelectedLane = 0;
                resetPlayer1();
                unselectSound.Play();
                //print("Canceling Placement");
            }
            if (player1SelectedLane > 0)
            {
                float zPosition = (player1SelectedLane - 1) * GlobalsHolder.zSpawnOffset;
                Vector3 spawnPos = GlobalsHolder.spawnPointPlayer1;
                spawnPos.z += zPosition;

                //check if space is occupied
                bool isSpawnTaken = false;
                if (player1CollisionBoxes[player1SelectedLane - 1].GetComponent<BoxCollisionScript>().isSpawnBlocked())
                {
                    isSpawnTaken = true;
                }

                if (isSpawnTaken)
                {
                    //print("Spawn is occupied. Unable to spawn kid.");
                }
                else
                {
                    //print("Placing kid " + player1SelectedPiece + " on lane " + player1SelectedLane);
                    string kidPrefabName = "Prefabs/" + GlobalsHolder.kidPrefabsNameList[player1SelectedPiece - 1];

                    GameObject instance = Instantiate(Resources.Load(kidPrefabName, typeof(GameObject))) as GameObject;
                    instance.name = "kid" + kidID;
                    kidID++;
                    instance.transform.position = spawnPos;
                    instance.transform.eulerAngles = GlobalsHolder.rotationPlayer1;
                    //kidsArray.Add(instance);
                    instance.GetComponent<KidController>().init(1, player1SelectedPiece - 1, player1SelectedLane);
                    Player1Controller.kidStocks[player1SelectedPiece - 1]--;
                    spawnSound.Play();
                }

            }
        }

        if (!player1PieceSelected)
        {
            if (Input.GetKeyDown("1"))
            {
                player1SelectedPiece = 1;
                print("Select kid 1");
            }
            else if (Input.GetKeyDown("2"))
            {
                player1SelectedPiece = 2;
                print("Select kid 2");
            }
            else if (Input.GetKeyDown("3"))
            {
                player1SelectedPiece = 3;
                print("Select kid 3");
            }
            else if (Input.GetKeyDown("4"))
            {
                player1SelectedPiece = 4;
                print("Select kid 4");
            }
            else if (Input.GetKeyDown("5"))
            {
                player1SelectedPiece = 5;
                print("Select kid 5");
            }
            if (player1SelectedPiece != 0 && Player1Controller.kidStocks[player1SelectedPiece - 1] == 0)
            {
                outOfStockSound.Play();
                print("Kid" + player1SelectedPiece + " is out of stock.");
                player1SelectedPiece = 0;
            }
        }
    }

    void getPlayer2Input()
    {
        if (player2PieceSelected)
        {
            if (Input.GetKeyDown("[1]"))
            {
                player2SelectedLane = 1;
            }
            else if (Input.GetKeyDown("[2]"))
            {
                player2SelectedLane = 2;
            }
            else if (Input.GetKeyDown("[3]"))
            {
                player2SelectedLane = 3;
            }
            else if (Input.GetKeyDown("[4]"))
            {
                player2SelectedLane = 4;
            }
            else if (Input.GetKeyDown("[0]"))
            {
                unselectSound.Play();
                player2SelectedLane = 0;
                resetPlayer2();
                //print("Canceling Placement");
            }
            if (player2SelectedLane > 0)
            {
                float zPosition = (player2SelectedLane - 1) * GlobalsHolder.zSpawnOffset;
                Vector3 spawnPos = GlobalsHolder.spawnPointPlayer2;
                spawnPos.z += zPosition;

                //check if space is occupied
                bool isSpawnTaken = false;
                if (player2CollisionBoxes[player2SelectedLane - 1].GetComponent<BoxCollisionScript>().isSpawnBlocked())
                {
                    isSpawnTaken = true;
                }
                if (isSpawnTaken)
                {
                    //print("Spawn is occupied. Unable to spawn kid.");
                }
                else
                {
                    //print("Placing kid " + player2SelectedPiece + " on lane " + player2SelectedLane);
                    string kidPrefabName = "Prefabs/" + GlobalsHolder.kidPrefabsNameList[player2SelectedPiece - 1];

                    GameObject instance = Instantiate(Resources.Load(kidPrefabName, typeof(GameObject))) as GameObject;
                    instance.name = "kid" + kidID;
                    kidID++;
                    instance.transform.position = spawnPos;
                    instance.transform.eulerAngles = GlobalsHolder.rotationPlayer2;
                    //kidsArray.Add(instance);
                    instance.GetComponent<KidController>().init(2, player2SelectedPiece - 1, player2SelectedLane);
                    Player2Controller.kidStocks[player2SelectedPiece - 1]--;
                    spawnSound.Play();
                }

            }
        }

        if (!player2PieceSelected)
        {
            if (Input.GetKeyDown("[1]"))
            {
                player2SelectedPiece = 1;
                print("Select kid 1");
            }
            else if (Input.GetKeyDown("[2]"))
            {
                player2SelectedPiece = 2;
                print("Select kid 2");
            }
            else if (Input.GetKeyDown("[3]"))
            {
                player2SelectedPiece = 3;
                print("Select kid 3");
            }
            else if (Input.GetKeyDown("[4]"))
            {
                player2SelectedPiece = 4;
                print("Select kid 4");
            }
            else if (Input.GetKeyDown("[5]"))
            {
                player2SelectedPiece = 5;
                print("Select kid 5");
            }
            if (player2SelectedPiece != 0 && Player2Controller.kidStocks[player2SelectedPiece - 1] == 0)
            {
                outOfStockSound.Play();
                print("Kid" + player2SelectedPiece + " is out of stock.");
                player2SelectedPiece = 0;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //placeKidInPlane
        if (!gameOver)
        {
            getPlayer1Input();
            getPlayer2Input();
            if (player1SelectedLane > 0)
            {
                resetPlayer1();
            }

            if (player1SelectedPiece > 0)
            {
                player1PieceSelected = true;
            }
            else
            {
                resetPlayer1();
            }

            if (player2SelectedLane > 0)
            {
                resetPlayer2();
            }

            if (player2SelectedPiece > 0)
            {
                player2PieceSelected = true;
            }
            else
            {
                resetPlayer2();
            }
            updateKidsStockUI();
            checkWinner();
        } else{
            SceneManager.LoadScene(3);
        }
    }
}
