using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private int playerGroup;  //1: P1, 2: P2
    private int laneNum;

    private int moveSpeedUnit;
    private int attackRangeUnit;

    private Vector3 movementSpeed;

    private GameObject fromKid;

    public void init(int playerGroup, int kidType, Vector3 movementSpeed, GameObject fromKid)
    {
        this.playerGroup = playerGroup;
        this.gameObject.tag = (playerGroup == 1 ? "P1Kid" : "P2Kid");
        this.movementSpeed = movementSpeed;
        this.fromKid = fromKid;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.movementSpeed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == "school")
        {
            Destroy(this.gameObject);
        }
        if ((c.gameObject.tag == "P1Kid" && this.playerGroup == 2) || (c.gameObject.tag == "P2Kid" && this.playerGroup == 1))
        {
            if (c.gameObject.name.Contains("kid")){
                if (c.gameObject.GetComponent<KidController>().getKidType() != 2 || !c.gameObject.GetComponent<KidController>().invisible)
                {
                    Destroy(c.gameObject);
                    Destroy(this.gameObject);
                }
                else if (c.gameObject.GetComponent<KidController>().invisible)
                {
                    Destroy(this.gameObject);
                    Destroy(this.fromKid);
                    c.gameObject.GetComponent<KidController>().setInvisible();
                }
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if ((c.gameObject.tag == "P1Kid" && this.playerGroup == 2) || (c.gameObject.tag == "P2Kid" && this.playerGroup == 1))
        {
            Destroy(c.gameObject);
            Destroy(this.gameObject);
        }
    }
}
