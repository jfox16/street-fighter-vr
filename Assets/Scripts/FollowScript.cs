using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    [SerializeField] Vector3 offsetVec = new Vector3(0, 0, -10);

    void LateUpdate() {
        if (GameControllerDDOL.spawnedFighter != null) {
            Follow(GameControllerDDOL.spawnedFighter);
        }
    }

    void Follow(GameObject target) {
        transform.position = target.transform.position + target.transform.rotation*offsetVec;
        transform.rotation = target.transform.rotation;
    }
}
