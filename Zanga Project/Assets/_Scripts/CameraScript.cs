using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3 offset;
    GameObject player;
    Vector3 veclocity;
    float smoothTime = 0.1f;
    float xMin,xMax,xPos,yMin,yMax,yPos;
    void Start()
    {
        //offset = new Vector3(-0.1f,3.5f,-10);
        player= GameObject.FindGameObjectWithTag("Player");
    }
    void LateUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref veclocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        var bounds = new Bounds (player.transform.position , Vector3.zero);
        return bounds.center;
    }
}
