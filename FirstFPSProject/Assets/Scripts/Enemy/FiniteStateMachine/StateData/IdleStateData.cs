using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Idle State Data", menuName = "Finite State Machine / State Data / Idle State Data")]
public class IdleStateData : ScriptableObject
{
    [Header("Hareket Değişkenleri")]
    public float moveSpeed;

    public float rotateSpeed;

    [Header("Süre Değişkenleri")]
    public float waitTime;

    [Header("Takip/Kovalama Değişkenleri")]
    public float chaseRadius;
}
