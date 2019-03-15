using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float damage = 2.0f;
    public float speed = 10.0f;

    private GameObject _player;
    private Shoot _shoot;

    // Start is called before the first frame update
    void Start()
    {
        getShoot();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0, speed*Time.deltaTime);
        //transform.position = Vector3.MoveTowards(_shoot.getPoint(), _shoot.getEndPoint(), speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Dummy"))
        {
            RedGuy _unit = other.GetComponent<RedGuy>();
            _unit.Hurt(damage);
            //Debug.Log("Hurt " + other.gameObject.ToString() + " for " + damage + " damage!");
        }
        Destroy(this.gameObject);
    }

    private void getShoot()
    {
        GameObject controller = GameObject.Find("Game Controller");
        GameController g = controller.GetComponent<GameController>();
        _player = g.getPlayer();
        _shoot = _player.GetComponent<Shoot>();
    }
}
