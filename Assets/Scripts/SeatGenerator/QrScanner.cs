using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using M2MqttUnity;

namespace UrbanTimetravel.SeatSync
{
    public class QrScanner : MonoBehaviour
    {
        WebCamTexture webcamTexture;
        string QrCode = string.Empty;
        public AudioSource beepSound;

        void Start()
        {
            webcamTexture = new WebCamTexture(256, 256);

            StartCoroutine(GetQRCode());
        }

        IEnumerator GetQRCode()
        {
            IBarcodeReader barCodeReader = new BarcodeReader();
            webcamTexture.Play();
            var snap = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32, false);
            while (string.IsNullOrEmpty(QrCode))
            {
                try
                {
                    snap.SetPixels32(webcamTexture.GetPixels32());
                    var Result = barCodeReader.Decode(snap.GetRawTextureData(), webcamTexture.width, webcamTexture.height, RGBLuminanceSource.BitmapFormat.ARGB32);
                    if (Result != null)
                    {
                        QrCode = Result.Text;
                        if (!string.IsNullOrEmpty(QrCode))
                        {
                            Debug.Log("DECODED TEXT FROM QR: " + QrCode);

                            GetComponent<SeatGenerator>().EnableSeat(int.Parse(QrCode));
                            GetComponent<SeatGenerator>().PlaceCameraToSeat(int.Parse(QrCode));
                            FindObjectOfType<M2MqttUnityClient>().PublishMessage("VR/Clients/" + QrCode, QrCode, true);
                            break;
                        }
                    }
                }
                catch (Exception ex) { Debug.LogWarning(ex.Message); }
                yield return null;
            }
            webcamTexture.Stop();
        }
    }
}
