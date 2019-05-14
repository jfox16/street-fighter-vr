using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    public GameObject car, score;
    private Destruction s;
    // Start is called before the first frame update
    void Start()
    {
        s = car.GetComponent<Destruction>();
    }

    // Update is called once per frame
    void Update()
    {
        int playerScore = s.getScore();
        score.GetComponent<TextMesh>().text = "Player Score: " + playerScore.ToString();
    }
}
