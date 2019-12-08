using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    this.transform.position += new Vector3(3, 0, 0) * Time.deltaTime;
  }

  void OnCollisionEnter(Collision c)
  {
    Debug.Log(c.gameObject.name);
    if (c.gameObject.name =="school")
    {
      Destroy(this.gameObject);
    }
  }
}
