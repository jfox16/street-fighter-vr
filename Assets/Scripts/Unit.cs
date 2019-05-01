using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public enum Team {Neutral, Red, Blue, Green, Yellow}
    public Team team;
    public abstract void Hurt(float damage);
}
