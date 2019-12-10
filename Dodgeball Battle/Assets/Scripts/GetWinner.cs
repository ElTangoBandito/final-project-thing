using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWinner : MonoBehaviour
{
    public GameObject winnerText;
    // Start is called before the first frame update
    void Start()
    {
        int winInt = GiveMeWinner.winner;
        if (winInt == 0) {
            winnerText.GetComponent<Text>().text = "It's a tie!";
        } else if (winInt == 1) {
            winnerText.GetComponent<Text>().text = "Player 1 wins!";
        } else if (winInt == 2) {
            winnerText.GetComponent<Text>().text = "Player 2 wins!";
        } else {
            winnerText.GetComponent<Text>().text = "invalid data passed";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
