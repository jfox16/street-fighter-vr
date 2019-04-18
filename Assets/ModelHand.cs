using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelHand : MonoBehaviour
{
    public GameObject riggedHand, indexFinger, mrpFingers, thumbFinger;
    public OvrAvatarTouchController handController;

    public Transform position { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
    }

    void setPlayerHandPosition()
    {
    }
}
