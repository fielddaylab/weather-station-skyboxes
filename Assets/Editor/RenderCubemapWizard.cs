using UnityEngine;
using UnityEditor;
using System.Collections;

public class RenderCubemapWizard : ScriptableWizard
{
    public Transform renderFromPosition;
    public Cubemap cubemap;
    public Camera camToCopy;

    void OnWizardUpdate()
    {
        string helpString = "Select transform to render from and cubemap to render into";
        bool isValid = (renderFromPosition != null) && (cubemap != null);
    }

    void OnWizardCreate()
    {
        cubemap = new Cubemap(2048, TextureFormat.RGBA32, false);
        // create temporary camera for rendering
        GameObject go = new GameObject("CubemapCamera");
        go.AddComponent<Camera>();
        // place it on the object
        go.transform.position = renderFromPosition.position;
        go.transform.rotation = Quaternion.identity;
        // camera settings
        Camera cam = go.GetComponent<Camera>();
        cam.CopyFrom(camToCopy);
        // render into cubemap
        cam.RenderToCubemap(cubemap);


        // destroy temporary camera
        DestroyImmediate(go);
    }

    [MenuItem("Tools/Render Cubemap")]
    static void RenderCubemap()
    {
        ScriptableWizard.DisplayWizard<RenderCubemapWizard>(
            "Render cubemap", "Render!");
    }
}