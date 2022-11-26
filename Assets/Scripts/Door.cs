using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Transform trans;
    [SerializeField] float doorSpeed;
    [SerializeField] GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {


       trans.position += new Vector3(0, doorSpeed, 0);

        if (trans.position.y >= 134)
        {
            doorSpeed = -doorSpeed;

        }

        if (trans.position.y <= 128)
        {
            doorSpeed = -doorSpeed;

        }
      
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
       // if (collision.gameObject.tag == "Player")
       // {

          //  if ( transform.position.y - player.transform.position.y <= 0.9)
            //{
              //  Debug.Log("under");
                //player.transform.position += new Vector3(0, 0, 0);
           // }
            
       // }
    }

   /* private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Exit");
            collision.gameObject.transform.parent = null;
        }
    }
*/
}
