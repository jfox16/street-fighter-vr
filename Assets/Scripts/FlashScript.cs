using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlashScript : MonoBehaviour
{
    [SerializeField] Image panel;
    // Start is called before the first frame update
    void Start()
    {
        panel.CrossFadeAlpha(0, 0, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHit()
    {
        Debug.Log("Player flashed");
        panel.CrossFadeAlpha(1, 0f, false);
        Invoke("FadeOut", 0f);
    }
    public void FadeOut()
    {
        panel.CrossFadeAlpha(0, .5f, false);
    }
}
