using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Idle State Data", menuName = "Finite State Machine / State Data / Idle State Data")]
public class IdleStateData : ScriptableObject
{
    [Header("Hareket Deðiþkenleri")]
    public float moveSpeed;

    public float rotateSpeed;

    [Header("Süre Deðiþkenleri")]
    public float waitTime;

    [Header("Takip/Kovalama Deðiþkenleri")]
    public float chaseRadius;
}
