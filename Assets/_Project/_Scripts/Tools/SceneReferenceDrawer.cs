#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SceneReference))]
public class SceneReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Draw the scene asset reference field
        var sceneAssetProp = property.FindPropertyRelative("sceneAsset");
        var sceneNameProp = property.FindPropertyRelative("sceneName");

        EditorGUI.PropertyField(position, sceneAssetProp, label);

        // Update the scene name if the scene asset is assigned
        if (sceneAssetProp.objectReferenceValue != null)
        {
            var sceneAsset = sceneAssetProp.objectReferenceValue as SceneAsset;
            sceneNameProp.stringValue = sceneAsset.name;
        }

        EditorGUI.EndProperty();
    }
}
#endif
