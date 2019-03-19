using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject fighterPrefab;
    [SerializeField] GameController controller;
    [SerializeField] PrefabSelection playerSelector;
    public GameObject test;
    // Start is called before the first frame update
    void Start()
    {
        fighterPrefab = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.GetComponentInParent<Fighter>().gameObject.GetComponentInChildren<Camera>());
        GameObject player = other.gameObject.GetComponentInParent<Fighter>().gameObject;
        Transform spawnLocation = player.transform;
        Debug.Log(player.transform.position);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(go);
        }
        controller.SelectPlayer(test, spawnLocation);
    }
}
