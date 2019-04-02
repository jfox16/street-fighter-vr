using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : Hittable
{
    public enum Team {Neutral=0, Red, Blue}
    public Team team;
}
