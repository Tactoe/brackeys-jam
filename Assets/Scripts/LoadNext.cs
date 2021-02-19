using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LoadNext : MonoBehaviour
{
    private Light light;
    private Volume v;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private Ease ease;
    private LensDistortion lens;

    private void Start()
    {
        light = FindObjectOfType<Light>();
        v = FindObjectOfType<Volume>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().Play();
        FindObjectOfType<GroundedCharacterController>().enabled = false;
        Time.timeScale = 0.3f;
        v.profile.TryGet(out lens);
        //DOTween.To(lens.intensity, 10);
        Camera.main.transform.DORotate(Vector3.forward * 180, animationDuration).SetEase(ease);
        DOTween.To(x => lens.intensity.value = x, 0, -1, animationDuration).SetEase(ease);
        DOTween.To(x => lens.scale.value = x, 1, 0.3f, animationDuration).SetEase(ease);
        //lens.intensity.value = -1;
        //AnimateVignette();
        light.DOIntensity(17, animationDuration).SetEase(ease);
        GameManager.Instance.LoadSceneFade("Battle", animationDuration, Color.white);
       //GameManager.Instance.NextScene(); 
    }


}
