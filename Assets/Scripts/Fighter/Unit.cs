using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public enum Team {Neutral, Red, Blue, Green, Yellow}
    public Team team = Team.Neutral;
    public abstract void Hurt(float damage);
}
