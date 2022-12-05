using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script generates seats that are variable by x and y. After that it creates a corresponding QR code for each seat.
/// </summary>
namespace UrbanTimetravel.SeatSync
{
    [RequireComponent(typeof(QrGenerator))]
    [RequireComponent(typeof(QrScanner))]
    [RequireComponent(typeof(SpawnCharacterOnSeat))]
    [RequireComponent(typeof(ControlHeadRotation))]
    public class SeatGenerator : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] GameObject m_PlayerCamera;

        [Header("Settings")]
        [SerializeField] int m_SeatMaxRow = 2;
        [SerializeField] int m_SeatMaxColumn = 2;
        [SerializeField] float m_Padding = 1.5f;
        [SerializeField] float m_SeatHeight = 0.25f;

        [Header("Prefab Objects")]
        [SerializeField] GameObject m_CoachPrefab;
        [SerializeField] GameObject m_SeatPrefab;
        [SerializeField] GameObject m_CharacterPrefab;

        [Header("QR Codes")]
        public List<Texture2D> m_QrCodes = new List<Texture2D>();
        List<GameObject> m_AllObjects = new List<GameObject>();
        public List<GameObject> m_AllPlayers = new List<GameObject>();
        [HideInInspector] public Dictionary<int, CatchHeadRotation> m_ActiveHeads = new Dictionary<int, CatchHeadRotation>();

        ControlHeadRotation m_ControlHeadRotation;

        int m_PlayerCount;
        [HideInInspector] public GameObject m_Coach;
        [HideInInspector] public int m_PlayerValue;

        private void Start()
        {
            DisableAllSeats();
            m_ControlHeadRotation = GetComponent<ControlHeadRotation>();

            m_PlayerValue = -1;
        }

        public void DisableAllSeats()
        {
            foreach (var player in m_AllPlayers)
            {
                player.SetActive(false);
            }
        }

        bool enable;

        public void EnableSeat(int value)
        {
            m_AllPlayers[value].SetActive(true);
            m_ActiveHeads.Add(value, m_AllPlayers[value].transform.GetComponentInChildren<CatchHeadRotation>());
            enable = true;

        }

        public void PlaceCameraToSeat(int value)
        {
            CatchHeadRotation m_HeadScript = m_AllPlayers[value].transform.GetComponentInChildren<CatchHeadRotation>();
            m_ControlHeadRotation.target = m_HeadScript.gameObject.transform;
            m_PlayerCamera.transform.position = m_HeadScript.gameObject.transform.position;

            m_HeadScript.m_CharacterPosition = value;

            m_PlayerValue = value;
        }

        public void QrGenerator(string text)
        {
            QrGenerator qrGenerator = GetComponent<QrGenerator>();
            Texture2D myQR = qrGenerator.generateQR(text);

            //then Save To Disk as PNG
            byte[] bytes = myQR.EncodeToPNG();

            string m_QrCodePath = Application.dataPath + "/QrCodes/";

            if (!Directory.Exists(m_QrCodePath))
            {
                Directory.CreateDirectory(m_QrCodePath);
            }

            File.WriteAllBytes(m_QrCodePath + text + ".png", bytes);
            m_QrCodes.Add(myQR);
        }

        public void SpawnSeats()
        {
            Debug.Log("Spawn");

            GameObject coach = Instantiate(m_CoachPrefab);
            m_AllObjects.Add(coach);
            m_Coach = coach;

            coach.transform.localScale = new Vector3(m_SeatMaxColumn * m_Padding, 0.5f, m_SeatMaxRow * m_Padding);
            float x = ((float)m_SeatMaxColumn / 2 * m_Padding) - (0.5f * m_Padding);
            float z = ((float)m_SeatMaxRow / 2 * m_Padding) - (0.5f * m_Padding);

            coach.transform.position = new Vector3(x, 0, z);

            for (int a = 0; a < m_SeatMaxColumn; a++)
            {
                for (int j = 0; j < m_SeatMaxRow; j++)
                {
                    var seat = Instantiate(m_SeatPrefab, new Vector3(a * m_Padding, 0, j * m_Padding), Quaternion.identity);
                    var player = Instantiate(m_CharacterPrefab, new Vector3(a * m_Padding, m_SeatHeight, j * m_Padding), Quaternion.identity);

                    seat.transform.SetParent(coach.transform);
                    player.transform.SetParent(seat.transform);

                    m_AllObjects.Add(seat);
                    m_AllObjects.Add(player);

                    m_AllPlayers.Add(player);

                    QrGenerator(m_PlayerCount.ToString());
                    seat.name = "Player" + m_PlayerCount;

                    m_PlayerCount += 1;
                }
            }
        }

        public void RemoveSeats()
        {
            Debug.Log("Clear");

            foreach (var objects in m_AllObjects)
            {
                DestroyImmediate(objects);
            }

            DestroyImmediate(m_Coach);

            m_PlayerCount = 0;
            m_AllObjects.Clear();
            m_AllPlayers.Clear();

            string m_QrCodePath = Application.dataPath + "/QrCodes/";

            if (Directory.Exists(m_QrCodePath))
            {
                Debug.Log("Path found");

                string[] files = Directory.GetFiles(m_QrCodePath);

                foreach (var file in files)
                {
                    File.Delete(file);
                }

                m_QrCodes.Clear();
                Debug.Log("QrCodes gelöscht");
            }
        }
    }
}
