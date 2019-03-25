using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    // Team determines which attacks the Unit can be hit by
    public enum Team {Neutral=0, Red=1, Blue=2}
    [SerializeField] Team team = Team.Neutral; // Defaults to Neutral

    public abstract void Hurt(float damage);
}
