using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public List<int> kidStocks = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        kidStocks[0] = 5;
        kidStocks[1] = 3;
        kidStocks[2] = 7;
        kidStocks[3] = 1;
        kidStocks[4] = 2;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
