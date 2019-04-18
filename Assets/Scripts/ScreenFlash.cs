using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public static ScreenFlash Instance;
    [SerializeField] Image image;

    void Awake() {
        Instance = this;
        image.enabled = true;
        image.CrossFadeAlpha(0, 0, false);
    }

    public static void PlayerHit() {
        Instance.image.CrossFadeAlpha(1, 0, false);
        Instance.image.CrossFadeAlpha(0, 0.5f, false);
    }


}
