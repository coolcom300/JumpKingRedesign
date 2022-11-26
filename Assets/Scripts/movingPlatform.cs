using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    Transform trans;
    [SerializeField] float movement;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
       
    }

    // Update is called once per frame
    void Update()
    {

        
        trans.position += new Vector3(movement, 0, 0);

        if(trans.position.x >= 22)
        {
            movement = -movement;
           
        }

        if(trans.position.x <= -7)
        {
            movement = -movement;
           
        }

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(gameObject.transform,true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Exit");
            collision.gameObject.transform.parent = null;
        }
    }

}
