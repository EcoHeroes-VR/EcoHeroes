using _Game.Scripts;
using UnityEditor;
using UnityEditor.XR.Interaction.Toolkit;

/// <summary>
/// Description: Custom Editor for an XRSocketInteractor. Makes it possible to check the Tag of a GameObject before the socket is filled with the GameObject.\n
/// Author: Theresa Mayer\n
/// </summary>

[CustomEditor(typeof(SocketWithTagCheck))]
public class SocketWithTagEditor : XRSocketInteractorEditor
{
    private SerializedProperty targetTag = null;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        targetTag = serializedObject.FindProperty("targetTag");
    }

    protected override void DrawProperties()
    {
        base.DrawProperties();
        EditorGUILayout.PropertyField(targetTag);
    }
}
