﻿using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using DG;
//--------------------------------------------------------------------
//Follows the player along the 2d plane, using a continuous lerp
//--------------------------------------------------------------------
public class BasicCameraTracker : MonoBehaviour {
    public GameObject m_Target = null;
    [SerializeField] float m_InterpolationFactor = 0.0f;
    public bool m_UseFixedUpdate = false;
    [SerializeField] float m_ZDistance = 10.0f, yOffset = 5;
    private CameraShake cs;

    private void Start()
    {
        cs = GetComponent<CameraShake>();
    }

    void FixedUpdate () 
	{
        if (m_UseFixedUpdate)
        {
            Interpolate(Time.fixedDeltaTime);
        }
	}

    void LateUpdate()
    {
        if (!m_UseFixedUpdate)
        {
            Interpolate(Time.deltaTime);
        }
    }

    public void DoShake(float shakeDuration, float shakeStrength, int shakeVibrato)
    {
        
        cs.ShakeCamera(shakeStrength, shakeDuration);
    }

    void Interpolate(float a_DeltaTime)
    {
        if (m_Target == null)
        {
            return;
        }
        Vector3 diff = m_Target.transform.position + new Vector3(0, yOffset, -m_ZDistance) - transform.position;
        transform.position += diff * m_InterpolationFactor * a_DeltaTime;
    }
}
