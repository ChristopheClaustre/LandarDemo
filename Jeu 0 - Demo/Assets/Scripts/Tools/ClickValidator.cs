﻿using UnityEngine;
using System.Collections;
using System.IO;

public class ClickValidator : MonoBehaviour
{
    public Camera cam;
    Vector3 positionOnFow;

    //Attention, pour que cette fontion fonctionne la camera FoW doit etre EXACTEMENT de même dimention que le mesh FoW
    public bool isOnBlackFoW()
    {
        positionOnFow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 screenPos = cam.WorldToScreenPoint(positionOnFow);
        screenPos = new Vector3(screenPos.x, cam.pixelHeight - screenPos.y, screenPos.z);

        RenderTexture rend = cam.targetTexture;
        RenderTexture.active = rend;

        // Read pixels
        Texture2D tex = new Texture2D(1, 1);
        Rect rect = new Rect(Mathf.FloorToInt(screenPos.x), Mathf.FloorToInt(screenPos.y), 1, 1);
        tex.ReadPixels(rect, 0, 0);
        tex.Apply();
        Color32 pixelSample = tex.GetPixel(0, 0);

        if (pixelSample.r > 0 || pixelSample.g > 0 || pixelSample.b > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}