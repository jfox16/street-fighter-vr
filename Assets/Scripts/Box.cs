using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private GameObject theDest;
    private GameObject player;
    private bool _nearPlayer;
    private bool _onPlayer;
    private bool _thrown;
    private float force = 10.0f;
    private float damage = 2.0f;

    /*
     * A bug where cannot pick up object all the time even if character is close enough   
     */   

    // Start is called before the first frame update
    void Start()
    {
        _onPlayer = false;
        _thrown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            theDest = GameObject.Find("Pick Up Point");
            Debug.Log("Player Found");
        }
        else
        {
            // offset for the box (design error)
            Vector3 temp = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
            float dist = Vector3.Distance(temp, player.transform.position);
            // check if player is close to the box if so then the player can pick up
            if (dist <= 1.5f)
            {
                _nearPlayer = true;
            }
            else
            {
                _nearPlayer = false;
            }
            // press f to pick up box if player is close enough
            if (!_onPlayer && _nearPlayer && Input.GetKeyDown("f"))
            {
                //GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                _onPlayer = true;
                transform.position = new Vector3(theDest.transform.position.x,
                theDest.transform.position.y, theDest.transform.position.z - 5);
                transform.parent = GameObject.Find("Pick Up Point").transform;
            }
            // press f again to throw the box
            else if (_onPlayer && Input.GetKeyDown("f"))
            {
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;
                _onPlayer = false;
                // throwing the box
                GetComponent<Rigidbody>().velocity = player.transform.forward * force;
                transform.parent = null;
                _thrown = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if the box hit the dummy
        if (_thrown)
        {
            if (other.tag.Equals("Dummy"))
            {
                RedGuy _unit = other.GetComponent<RedGuy>();
                _unit.Hurt(damage);
                Debug.Log("Hurt " + other.gameObject.ToString() + " for " + damage + " damage!");
            }
            Destroy(this.gameObject);
        }
    }
}
