using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    public float proximityThreshold = 0.5f;  // Seuil de proximité pour le changement de direction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB;  // Simplifié, pas besoin de .transform
    }

    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        rb.velocity = new Vector2((currentPoint == pointB ? speed : -speed), 0);  // Simplifié avec un opérateur ternaire

        if (Vector2.Distance(transform.position, currentPoint.position) < proximityThreshold)
        {
            flip();
            currentPoint = (currentPoint == pointB) ? pointA : pointB;  // Simplifié avec un opérateur ternaire
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.position, 0.5f);
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
}