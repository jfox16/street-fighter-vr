using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    [SerializeField] Vector3 offsetVec = new Vector3(0, 0, -10);

    void Update() {
        if (GameController.clientPlayer != null) {
            Follow(GameController.clientPlayer);
        }
    }

    void Follow(GameObject target) {
        transform.position = target.transform.position + offsetVec;
    }
}
