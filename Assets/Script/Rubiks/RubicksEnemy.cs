using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubicksEnemy : MonoBehaviour
{
    public float speed = 1.0f;
    void Update()
    {
        Vector3 worldAngle = transform.eulerAngles;
        worldAngle.y += speed;
        transform.eulerAngles = worldAngle;
    }
}
