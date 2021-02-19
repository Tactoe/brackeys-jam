using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class IntroBattle : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private Volume v;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private Ease ease;
    private LensDistortion lens;
    private bool doLightIntro;


    private void Start()
    {
        doLightIntro = ActionRecorder.Instance.recordingForFirstTime;
        if (doLightIntro)
            LightIntro();
        else
            DoIntro();
    }

    private void DoIntro()
    {
        v.profile.TryGet(out lens);
        DOTween.To(x => lens.intensity.value = x, -1, 0, animationDuration).SetEase(ease);
        DOTween.To(x => lens.scale.value = x, 0.3f, 1, animationDuration).SetEase(ease);
        light.intensity = 17;
        light.DOIntensity(1, animationDuration).SetEase(ease);
        LightIntro();
    }

    private void LightIntro()
    {
        GameManager.Instance.FadeIn(animationDuration, Color.white);
    }


}
