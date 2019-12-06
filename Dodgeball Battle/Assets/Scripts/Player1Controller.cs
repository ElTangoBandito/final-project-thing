using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public static List<int> kidStocks = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        kidStocks.Add(5);
        kidStocks.Add(2);
        kidStocks.Add(1);
        kidStocks.Add(4);
        kidStocks.Add(0);
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
