using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    

    [SerializeField] GameObject playerobj;

    Transform trans;
    playerMovement player;
    //bool move = true;



    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        player = playerobj.GetComponent<playerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();

        //  Debug.Log(transform.position);
    }

    // Daniels camera code
    void FollowPlayer()
    {
        float camheight;
        camheight = playerobj.transform.position.y;
        if(camheight <= 15)
        {
            camheight = 15;
        }
        trans.position = new Vector3(trans.position.x, camheight , trans.position.z); 
    }
    // Christians camera code
    void OnTriggerExit2D(Collider2D collision)
    {



        if (playerobj.transform.position.y > transform.position.y)
        {


            trans.position += new Vector3(0, 40f, 0);
            //move = false;
            //UnityEngine.Debug.Log(trans.position);
            //Debug.Log(playerobj.transform.position);

        }

        else if (playerobj.transform.position.y < transform.position.y)
        {

            trans.position -= new Vector3(0, 20f, 0);

        }



    }


}
