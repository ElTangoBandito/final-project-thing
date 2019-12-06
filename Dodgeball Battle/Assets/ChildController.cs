using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        this.isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool checkDead(){
        return this.isDead;
    }
}
