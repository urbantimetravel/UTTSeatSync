using UnityEditor;
using UnityEngine;

namespace UrbanTimetravel.SeatSync
{
#if UNITY_EDITOR
    [CustomEditor(typeof(SeatGenerator))]
    public class SeatInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Hey, this is our customer field");
            base.OnInspectorGUI();

            SeatGenerator generator = (SeatGenerator)target;

            if (GUILayout.Button("Create Seats"))
            {
                generator.SpawnSeats();
            }

            if (GUILayout.Button("Remove Seats"))
            {
                generator.RemoveSeats();
            }
        }
    }
#endif
}
