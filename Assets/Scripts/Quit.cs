using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Quit : MonoBehaviour
{
    // Start is called before the first frame update

    Animator a;
    void Start()
    {
        a = GetComponent<Animator>();
    }
    IEnumerator OnTriggerEnter(Collider other)
    {
       
        a.Play("box_open", 0, .25f);
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }

}
