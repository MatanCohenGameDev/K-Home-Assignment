using UnityEngine;
using UnityEditor;
using System.IO;

public class PrefabIconScreenshotTool : EditorWindow
{
    GameObject prefab;

    int resolution = 512;
    float padding = 1.25f;

    // Camera angle controls
    float pitch = 20f;   // Up / Down
    float yaw = -30f;    // Left / Right
    float distance = 10f;

    [MenuItem("Tools/Prefab Icon Screenshot Tool")]
    static void Open()
    {
        GetWindow<PrefabIconScreenshotTool>("Prefab Icon Tool");
    }

    void OnGUI()
    {
        GUILayout.Label("Prefab Icon Screenshot Tool", EditorStyles.boldLabel);

        prefab = (GameObject)EditorGUILayout.ObjectField(
            "Prefab",
            prefab,
            typeof(GameObject),
            false
        );

        resolution = EditorGUILayout.IntField("Resolution", resolution);
        padding = EditorGUILayout.Slider("Padding", padding, 1.0f, 2.0f);

        GUILayout.Space(10);
        GUILayout.Label("Camera Settings", EditorStyles.boldLabel);

        pitch = EditorGUILayout.Slider("Pitch (Up / Down)", pitch, -89f, 89f);
        yaw = EditorGUILayout.Slider("Yaw (Left / Right)", yaw, -180f, 180f);
        distance = EditorGUILayout.Slider("Camera Distance", distance, 1f, 50f);

        GUILayout.Space(15);

        GUI.enabled = prefab != null;

        if (GUILayout.Button("Capture Transparent PNG"))
        {
            Capture();
        }

        GUI.enabled = true;
    }

    void Capture()
    {
        // Instantiate prefab
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        instance.transform.position = Vector3.zero;
        instance.transform.rotation = Quaternion.identity;

        // Calculate bounds
        Bounds bounds = CalculateBounds(instance);
        Vector3 center = bounds.center;

        // Create camera
        Camera cam = new GameObject("IconCamera").AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0, 0, 0, 0);
        cam.orthographic = true;
        cam.nearClipPlane = -100;
        cam.farClipPlane = 100;

        // Fit orthographic size
        float maxExtent = Mathf.Max(bounds.extents.x, bounds.extents.y, bounds.extents.z);
        cam.orthographicSize = maxExtent * padding;

        // Camera rotation from sliders
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 direction = rotation * Vector3.forward;

        cam.transform.position = center - direction * distance;
        cam.transform.rotation = rotation;

        // Render texture
        RenderTexture rt = new RenderTexture(resolution, resolution, 24, RenderTextureFormat.ARGB32);
        cam.targetTexture = rt;

        RenderTexture.active = rt;
        cam.Render();

        // Read pixels
        Texture2D tex = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, resolution, resolution), 0, 0);
        tex.Apply();

        // Save PNG
        string folder = "Assets/PrefabIcons";
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string path = $"{folder}/{prefab.name}.png";
        File.WriteAllBytes(path, tex.EncodeToPNG());

        // Cleanup
        RenderTexture.active = null;
        cam.targetTexture = null;

        DestroyImmediate(rt);
        DestroyImmediate(tex);
        DestroyImmediate(cam.gameObject);
        DestroyImmediate(instance);

        AssetDatabase.Refresh();
        Debug.Log($"Icon saved: {path}");
    }

    Bounds CalculateBounds(GameObject go)
    {
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
            return new Bounds(go.transform.position, Vector3.one);

        Bounds bounds = renderers[0].bounds;
        foreach (Renderer r in renderers)
            bounds.Encapsulate(r.bounds);

        return bounds;
    }
}
