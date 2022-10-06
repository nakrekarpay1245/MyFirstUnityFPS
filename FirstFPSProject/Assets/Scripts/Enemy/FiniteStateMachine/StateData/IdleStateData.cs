using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Idle State Data", menuName = "Finite State Machine / State Data / Idle State Data")]
public class IdleStateData : ScriptableObject
{
    [Header("Hareket De�i�kenleri")]
    public float moveSpeed;

    public float rotateSpeed;

    [Header("S�re De�i�kenleri")]
    public float waitTime;

    [Header("Sald�r� De�i�kenleri")]
    [HideInInspector]
    public float attackRadius;
}
