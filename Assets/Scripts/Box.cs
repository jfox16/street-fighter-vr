using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private GameObject theDest;
    private GameObject player;
    private bool _nearPlayer;
    private bool _onPlayer;
    private float force = 200.0f;

    // Start is called before the first frame update
    void Start()
    {
        _onPlayer = false;
        //FindPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
        // player is missing need to fix
        float dist = Vector3.Distance(temp, player.transform.position);
        if(dist <= 2.5f)
        {
            _nearPlayer = true;
        }
        else
        {
            _nearPlayer = false;
        }
        if(!_onPlayer && _nearPlayer && Input.GetKeyDown("f"))
        {
            //GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            _onPlayer = true;
            transform.position = new Vector3(theDest.transform.position.x, 
            theDest.transform.position.y, theDest.transform.position.z - 5);
            transform.parent = GameObject.Find("Pick Up Point").transform;
        }
        else if(_onPlayer && Input.GetKeyDown("f"))
        {
            //GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            _onPlayer = false;
            transform.parent = null;
            GetComponent<Rigidbody>().AddForce(player.transform.forward * force);
        }
        //Debug.Log(player.transform.position);
    }

    private void FindPlayer()
    {
        GameController controller = GetComponent<GameController>();
        // player = controller.GetPlayer();
        theDest = GameObject.Find("Pick Up Point");
    }
}
