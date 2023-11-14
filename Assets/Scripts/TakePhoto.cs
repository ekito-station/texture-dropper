using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour
{
    public int width = 800;
    public int height = 800;

    [SerializeField] private RawImage textureImage;
    [SerializeField] private GameObject cameraButton;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject takeButton;

    private Texture2D _screenShot;

    [SerializeField] private Material modelMaterial;

    public void OnCameraButtonClicked()
    {
        cameraButton.SetActive(false);
        closeButton.SetActive(true);
        takeButton.SetActive(true);
    }

    public void OnCloseButtonClicked()
    {
        cameraButton.SetActive(true);
        closeButton.SetActive(false);
        takeButton.SetActive(false);
    }
    
    public void OnTakeButtonClicked()
    {
        int destX = Screen.width / 2 - width / 2;
        int destY = Screen.height / 2 - height / 2;
        Debug.Log("destX: " + destX);
        Debug.Log("destY: " + destY);
        _screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        _screenShot.ReadPixels(new Rect(destX,destY,width,height), 0, 0);
        _screenShot.Apply();

        SetTrackedImage();
    }

    private void SetTrackedImage()
    {
        textureImage.texture = _screenShot;

        modelMaterial.SetTexture("_MainTex", textureImage.texture);
    }
}
