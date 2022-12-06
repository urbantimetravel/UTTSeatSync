using UnityEngine;
using M2MqttUnity;

/// <summary>
/// This script reads the rotations of the activated characters from the MQTT broker and transfers the rotations to the respective heads.
/// </summary>
namespace UrbanTimetravel.SeatSync
{
    public class CatchHeadRotation : MonoBehaviour
    {
        [HideInInspector] public int m_CharacterPosition;
        SeatGenerator m_SeatManager;

        private void Start()
        {
            m_SeatManager = FindObjectOfType<SeatGenerator>();
        }

        private void FixedUpdate()
        {
           if (gameObject.activeSelf && m_SeatManager.m_PlayerValue == m_CharacterPosition)
                FindObjectOfType<M2MqttUnityClient>().PublishMessage("VR/Clients/" + m_CharacterPosition + "/Rotation", transform.rotation.eulerAngles.ToString(), false);
        }

        public void RotateOtherHeads(string rotation)
        {
            if (m_SeatManager.m_PlayerValue != m_CharacterPosition)
            {
                transform.eulerAngles = StringToVector3(rotation);
            }
        }

        public static Vector3 StringToVector3(string sVector)
        {
            // Remove the parentheses
            if (sVector.StartsWith("(") && sVector.EndsWith(")"))
            {
                sVector = sVector.Substring(1, sVector.Length - 2);
            }

            // split the items
            string[] sArray = sVector.Split(',');

            // store as a Vector3
            Vector3 result = new Vector3(
                float.Parse(sArray[0]) / 10,
                float.Parse(sArray[1]) / 10,
                float.Parse(sArray[2]) / 10);

            return result;
        }

        public static Quaternion StringToQuaternion(string sQuaternion)
        {
            // Remove the parentheses
            if (sQuaternion.StartsWith("(") && sQuaternion.EndsWith(")"))
            {
                sQuaternion = sQuaternion.Substring(1, sQuaternion.Length - 2);
            }

            // split the items
            string[] sArray = sQuaternion.Split(',');

            // store as a Vector3
            Quaternion result = new Quaternion(
                float.Parse(sArray[0]),
                float.Parse(sArray[1]),
                float.Parse(sArray[2]),
                float.Parse(sArray[3]));

            return result;
        }
    }
}
