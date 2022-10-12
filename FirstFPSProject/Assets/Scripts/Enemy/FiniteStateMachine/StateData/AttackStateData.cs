using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Attack State Data", menuName = "Finite State Machine / State Data / Attack State Data")]

public class AttackStateData : ScriptableObject
{
    [Header("Hareket Deðiþkenleri")]
    public float moveSpeed;
    public float rotateSpeed;

    [Header("Algýlama Deðiþkenleri")]
    public float attackRadius;
    public float chaseRadius;

    [Header("Saldýrý Deðiþkenleri")]
    public float attackTime = 3;

    [Header("Bekleme Deðiþkenleri")]
    public float attackWaitTime = 1;
}
