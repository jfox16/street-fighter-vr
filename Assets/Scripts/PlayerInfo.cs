using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo PI;
    public int selectedCharacter;
    public GameObject[] allCharacters;


    private void OnEnable()
    {
        if (PlayerInfo.PI == null)
        {
            PlayerInfo.PI = this;
        }
        else
        {
            if (PlayerInfo.PI == null)
            {
                Destroy(PlayerInfo.PI.gameObject);
                PlayerInfo.PI = this;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("My Character"))
        {
            selectedCharacter = PlayerPrefs.GetInt("My Character");
        }
        else
        {
            selectedCharacter = 0;
            PlayerPrefs.SetInt("My Character", selectedCharacter);
        }
        
    }
}
