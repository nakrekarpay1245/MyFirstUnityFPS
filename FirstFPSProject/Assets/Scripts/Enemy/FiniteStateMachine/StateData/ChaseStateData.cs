using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Chase State Data", menuName = "Finite State Machine / State Data / Chase State Data")]

public class ChaseStateData : ScriptableObject
{
    [Header("Hareket De�i�kenleri")]
    public float moveSpeed;
    public float rotateSpeed;

    [Header("Sald�r� De�i�kenleri")]
    public float attackRadius;
    public float chaseRadius;
}
