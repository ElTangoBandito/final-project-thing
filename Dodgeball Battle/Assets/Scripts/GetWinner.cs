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
        winnerText.GetComponent<Text>().text = GiveMeWinner.winner.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
