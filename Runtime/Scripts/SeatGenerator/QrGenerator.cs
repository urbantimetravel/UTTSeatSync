using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

namespace UrbanTimetravel.SeatSync
{
    public class QrGenerator : MonoBehaviour
    {
        private WebCamTexture camTexture;
        private Rect screenRect;

        private static Color32[] Encode(string textForEncoding, int width, int height)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width
                }
            };
            return writer.Write(textForEncoding);
        }

        public Texture2D generateQR(string text)
        {
            var encoded = new Texture2D(256, 256, TextureFormat.RGB24, false);
            var color32 = Encode(text, encoded.width, encoded.height);
            encoded.SetPixels32(color32);
            encoded.Apply();
            return encoded;
        }
    }

}
