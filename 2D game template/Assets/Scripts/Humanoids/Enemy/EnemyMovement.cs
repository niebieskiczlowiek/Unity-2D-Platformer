using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float linearDrag;

    [Header("Components")] 
    [SerializeField] private Enemy enemyScript;
    [SerializeField] private Rigidbody2D enemyRb;

    public void FixedUpdate()
    {
        enemyRb.drag = enemyScript.GetHitStun().IsCoolingDown ? 0f : linearDrag;
    }
} 
