<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public Fighter fighter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(fighter.transform.position.x, fighter.transform.position.y, fighter.transform.position.z);
    }
    public void setFighter(Fighter f)
    {
        fighter = f;
    }

}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public Fighter fighter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(fighter.transform.position.x, fighter.transform.position.y, fighter.transform.position.z);
    }
    public void setFighter(Fighter f)
    {
        fighter = f;
    }

}
>>>>>>> 688f6b1587d8b5c1a065668bd5794e7a4848651e
