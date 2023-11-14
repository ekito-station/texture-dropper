using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LightEstimate : MonoBehaviour
{
    [SerializeField] private ARCameraManager cameraManager;

    private Light _light;
    
    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    private void OnEnable()
    {
        if (cameraManager != null) cameraManager.frameReceived += OnCameraFrameChanged;
    }

    private void OnDisable()
    {
        if (cameraManager != null) cameraManager.frameReceived -= OnCameraFrameChanged;
    }

    private void OnCameraFrameChanged(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            //明るさ (float)
            float brightness = args.lightEstimation.averageBrightness.Value;
            Debug.Log("明るさ: " + brightness);
            _light.intensity = brightness;
            RenderSettings.ambientIntensity = brightness;
            
            //色補正 (Color)
            if (args.lightEstimation.colorCorrection.HasValue)
            {
                Color colorCorrection = args.lightEstimation.colorCorrection.Value;
                Debug.Log("色補正: " + colorCorrection);
                _light.color = colorCorrection;    //DirectionalLight に反映
            }
        }
    }
    
    //光源推定を止めたい場合に使う関数
    public void StopLightEstimation()
    {
        //アクション関数から削除する
        if (cameraManager != null)
            cameraManager.frameReceived -= OnCameraFrameChanged;
        cameraManager.requestedLightEstimation = LightEstimation.None;
        this.enabled = false;
    }
}
