using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraSpecial : MonoBehaviour
{
    public float cost;
    public Fighter fighter;
    // Start is called before the first frame update
    void Start()
    {
        fighter = this.gameObject.GetComponentInParent<Fighter>();
        cost = fighter.specialCost;
        Debug.Log("Aura Special created");
        StartCoroutine("TickDownSpecial");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.fighter.special <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator TickDownSpecial()
    {
        while (this.fighter.special > 0)
        {
            if (this.fighter.special > 0)
            {
                Debug.Log("Special" + this.fighter.special);
                this.fighter.special -= cost;
            }
            if (this.fighter.special < 0)
            {
                this.fighter.special = 0;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
