using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARRaycastManager))]

public class ModelPlacement : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject arCamera;
    [SerializeField] private GameObject exp;

    private ARRaycastManager _raycastManager;

    private List<ARRaycastHit> _hitResults = new List<ARRaycastHit>();

    private void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // UIをタップしている場合はスキップ
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                return;
            }
            // 説明を非表示に
            exp.SetActive(false);
            // rayを照射して平面に当たっていた場合
            if (_raycastManager.Raycast(Input.GetTouch(0).position, _hitResults))
            {
                Vector3 modelPos = _hitResults[0].pose.position;
                GameObject.Instantiate(model, modelPos, Quaternion.identity);
            }
            else
            {
                return;
            }
        }
    }
}
