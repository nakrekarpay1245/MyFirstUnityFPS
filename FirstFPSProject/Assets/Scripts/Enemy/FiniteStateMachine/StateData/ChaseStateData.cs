using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Chase State Data", menuName = "Finite State Machine / State Data / Chase State Data")]

public class ChaseStateData : ScriptableObject
{
    [Header("Hareket Deðiþkenleri")]
    public float moveSpeed;
    public float rotateSpeed;

    [Header("Saldýrý Deðiþkenleri")]
    public float attackRadius;
    public float chaseRadius;
}
