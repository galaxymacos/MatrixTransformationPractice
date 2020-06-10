using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidBeltTest : MonoBehaviour
{
    private List<Vector3> asteroids;
    
    public int points;
    public float length;

    public Vector3 rotationVector = new Vector3(0,90,0);
    
    private void Awake()
    {
        OnValidate();
    }

    private void Update()
    {
        // 创建旋转Matrix
        var matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(rotationVector * Time.deltaTime), Vector3.one);
        asteroids = asteroids.Select(last =>
        {
            // cast 4 by 4 matrix to 3 * 3
            return (Vector3) (matrix * last);
            
            var current = matrix * last;
            return new Vector3(current.x, current.y, current.z);
            
        }).ToList();
    }

    private void OnValidate()
    {
        asteroids = new List<Vector3>();
        for (int i = 0; i < points; i++)
        {
            float ratio = (float) i / points;
            ratio *= 2 * Mathf.PI;
            asteroids.Add(new Vector3(Mathf.Sin(ratio), 0, Mathf.Cos(ratio))*length);
            
            
        }
    }

    public void OnDrawGizmos()
    {
        if (asteroids != null)
        {
            foreach (Vector3 asteroid in asteroids)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(asteroid, 0.5f);
            }
        }
    }
} 
