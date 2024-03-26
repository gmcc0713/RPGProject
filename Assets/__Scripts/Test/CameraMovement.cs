using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform m_objectTofollow;
    public float m_followSpeed = 10f;
    public float m_sensivity = 100f;
    public float clampAngle = 70f;

    private float rotX;
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 fonalDir;
    public float minDist;
    public float maxDist;
    public float finalDist;
}
