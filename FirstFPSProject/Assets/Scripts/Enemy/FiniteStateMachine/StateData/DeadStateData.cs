using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(fileName = "Dead State Data", menuName = "Finite State Machine / State Data / Dead State Data")]
public class DeadStateData : ScriptableObject
{
    [Header("Hareket De�i�kenleri")]
    public float speed;
}
