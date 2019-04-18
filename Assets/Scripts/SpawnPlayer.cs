using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject fighterPrefab;
    [SerializeField] GameController controller;
    [SerializeField] PrefabSelection playerSelector;
    public GameObject test;
    public string charName;
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        Transform spawnLocation = player.transform;
        Debug.Log(player.transform.position);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(go);
        }
        controller.SelectPlayer(test, spawnLocation);
        PlayerPrefs.SetString("CharacterSelection", charName);
    }
}
