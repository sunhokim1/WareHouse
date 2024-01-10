using UnityEngine;
using ZXing;
using TMPro;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class QRCodeScanner : MonoBehaviour
{
    private string associatedSheet = "1C1zd5BJMwcOSQBgDgO8BZjzruSRQb_qYnlDcn8yjdJc";
    private string associatedWorksheet = "testSheet";
    [SerializeField]
    private TextMeshProUGUI productTextOut;
    [SerializeField]
    private TextMeshProUGUI placeTextOut;
    private WebCamTexture _cameraTexture;
    [SerializeField]
    private RawImage _rawImageBackground;
    [SerializeField]
    private AspectRatioFitter _aspectRatioFitter;
    [SerializeField]
    private RectTransform _scanZone;
    private bool _isCamAvaible;
    void Start()
    {
        SetUpCamera();
    }

    void Update()
    {
        UpdateCameraRender();
    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            _isCamAvaible = false;
            return;
        }
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                _cameraTexture = new WebCamTexture(devices[i].name, (int)_scanZone.rect.width, (int)_scanZone.rect.height);
                break;
            }
        }
        _cameraTexture.Play();
        _rawImageBackground.texture = _cameraTexture;
        _isCamAvaible = true;
    }

    private void UpdateCameraRender()
    {
        if (_isCamAvaible == false)
        {
            return;
        }
        float ratio = (float)_cameraTexture.width / (float)_cameraTexture.height;
        _aspectRatioFitter.aspectRatio = ratio;

        int orientation = _cameraTexture.videoRotationAngle;
        orientation = orientation * 3;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);

    }

    public void OnClickScan()
    {
        Scan();
    }

    private void Scan()
    {
        if (_cameraTexture == null || !_cameraTexture.didUpdateThisFrame)
        {
            return;
        }

        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(_cameraTexture.GetPixels32(), _cameraTexture.width, _cameraTexture.height);
            if (result != null)
            {
                productTextOut.text = result.Text;
                if (String.Equals(productTextOut.text, "https://me-qr.com/7Nczownx"))
                {
                    productTextOut.text = "mouse";
                    placeTextOut.text = "seoul";
                    UpdateGoogleSheets(productTextOut.text, placeTextOut.text);
                }
                else if (String.Equals(productTextOut.text, "https://me-qr.com/S871cNZB"))
                {
                    productTextOut.text = "mouse";
                    placeTextOut.text = "daejeon";
                    UpdateGoogleSheets(productTextOut.text, placeTextOut.text);
                }
                else
                {
                    productTextOut.text = "what";
                    placeTextOut.text = "where";
                }
            }
            else
            {
                placeTextOut.text = "where";
                productTextOut.text = "Failed to Read QR CODE";
            }
        }
        catch (Exception e)
        {
            productTextOut.text = "Scan Error";
            Debug.LogError("Scan Exception: " + e.Message);
        }
    }

    private void UpdateGoogleSheets(string productName, string placeName)
    {
        if (string.IsNullOrEmpty(placeName))
            return;

        GSTU_Search search = new GSTU_Search(associatedSheet, associatedWorksheet);
        SpreadsheetManager.Read(search, (GstuSpreadSheet ss) =>
        {
            int placeIndex = GetPlaceIndex(placeName);
            if (placeIndex == 1000)
            {
                Debug.LogError($"Invalid place name: {placeName}");
                return;
            }
            if (ss.rows.ContainsKey(productName))
            {
                int rowIndex = ss.rows[productName][0].Row();
                if (String.Equals(ss.rows[1][8], "Release"))
                {
                    PlaceParsing(rowIndex, placeIndex + 7, ss);
                }
                else
                    PlaceParsing(rowIndex, placeIndex, ss);
            }
            else
                Debug.LogError($"Item '{productName}' not found in spreadsheet.");
        });
    }

    private int GetPlaceIndex(string placeName)
    {
        switch (placeName.ToLower())
        {
            case "seoul": return 2;
            case "daejeon": return 3;
            case "busan": return 4;
            case "gwangju": return 5;
            case "wonju": return 6;
            default: return 1000;
        }
    }

    void PlaceParsing(int rowIndex, int placeIndex, GstuSpreadSheet ss)
    {
        if (placeIndex == 1000)
            return;
        if (int.TryParse(ss.rows[rowIndex][placeIndex].value, out int currentCount))
        {
            currentCount++;
            ss.rows[rowIndex][placeIndex].UpdateCellValue(associatedSheet, associatedWorksheet, currentCount.ToString());
        }
        else
        {
            Debug.LogError($"Cannot convert cell value to integer for product '{rowIndex}' in column {placeIndex}.");
        }
    }
}