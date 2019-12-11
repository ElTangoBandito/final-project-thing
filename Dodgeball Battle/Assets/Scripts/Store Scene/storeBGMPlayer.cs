using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storeBGMPlayer : MonoBehaviour
{
    public AudioSource storeBGM;
    // Start is called before the first frame update
    void Start()
    {
        storeBGM.PlayDelayed(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
