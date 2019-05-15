using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    [SerializeField] Vector3 mechaOffsetVec = new Vector3(0, 2, -2);
    [SerializeField] Vector3 ucOffsetVec = new Vector3(0, 2, -2);

    void LateUpdate() {
        if (GameControllerDDOL.spawnedFighter != null) {
            Follow(GameControllerDDOL.spawnedFighter);
        }
    }

    void Follow(GameObject target) 
    {
        Vector3 _offsetVec = Vector3.zero;
        switch(GameControllerDDOL.selectedFighter) {
            case GameControllerDDOL.Fighter.Mecha:
                _offsetVec = mechaOffsetVec;
                break;
            case GameControllerDDOL.Fighter.Unitychan:
                _offsetVec = ucOffsetVec;
                break;
        }

        transform.position = target.transform.position + target.transform.rotation*_offsetVec;
        transform.rotation = target.transform.rotation;
    }
}
