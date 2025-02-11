using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraVirtual : MonoBehaviour
{
    [Header("Cam")]
    [SerializeField] private CinemachineVirtualCamera _camera;

    [SerializeField] private float _cameraLimitDistance;
    [SerializeField] private float _cameraStep;
    private CinemachineFramingTransposer _cameraSettings;


    private void Awake()
    {
        _cameraSettings = _camera.gameObject.GetComponent<CinemachineFramingTransposer>();
    }

    private void SetCameraDistance(int blocksCount)
    {
        float newCameraDistance = blocksCount / _cameraLimitDistance + _cameraStep;
        if (newCameraDistance > _cameraLimitDistance) _cameraSettings.m_CameraDistance = newCameraDistance;
        else _cameraSettings.m_CameraDistance = _cameraLimitDistance;
    }
}
