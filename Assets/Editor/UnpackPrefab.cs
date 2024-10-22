using UnityEditor;
using UnityEngine;

public class UnpackPrefab : EditorWindow
{
    // Open the window from the Unity menu
    [MenuItem("Tools/Prefab Unpacker")]
    public static void ShowWindow()
    {
        // Open the Prefab Unpacker window
        GetWindow<UnpackPrefab>("Prefab Unpacker");
    }

    private void OnGUI()
    {
        // Display instructions in the window
        GUILayout.Label("Press Ctrl + Space to Unpack the Prefab", EditorStyles.boldLabel);

        // Listen for key events in the GUI
        Event e = Event.current;
        if (e != null && e.type == EventType.KeyDown)
        {
            // Check if Ctrl and Space are pressed together
            if (e.control && e.keyCode == KeyCode.Space)
            {  
                UnpackSelectedPrefab();
            }
        }
    }

    // Unpack the selected prefab
    private void UnpackSelectedPrefab()
    {
        if (Selection.activeGameObject != null)
        {
            GameObject selectedObject = Selection.activeGameObject;

            // Check if the selected object is part of a prefab instance
            PrefabInstanceStatus prefabInstanceStatus = PrefabUtility.GetPrefabInstanceStatus(selectedObject);

            if (prefabInstanceStatus == PrefabInstanceStatus.Connected)
            {
                // Unpack the prefab instance completely
                PrefabUtility.UnpackPrefabInstance(selectedObject, PrefabUnpackMode.Completely, InteractionMode.UserAction);

                Debug.Log($"{selectedObject.name} prefab has been unpacked!");
            }
            else
            {
                Debug.LogWarning("The selected object is not a prefab instance.");
            }
        }
        else
        {
            Debug.LogWarning("No GameObject is selected.");
        }
    }
}
