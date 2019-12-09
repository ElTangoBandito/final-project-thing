using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public static List<int> kidStocks = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0 ; i < GiveMeArrays.player2Stocks.Length; i++){
            kidStocks.Add(GiveMeArrays.player2Stocks[i]);
        }
        //kidStocks.Add(5);
        //kidStocks.Add(0);
        //kidStocks.Add(0);
        //kidStocks.Add(0);
        //kidStocks.Add(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
