using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionManager : MonoBehaviour
{
    // Create a new camera to mirror main camera
    Camera reflectionCamera;
    // Reference to main camera
    Camera MainCamera;
    RenderTexture renderTarget;

    [Range(0.0f, 1.0f)]
    public float reflectionFactor = 0.5f;
    public GameObject reflectionPlane;
    public Material Reflection;

    // Use this for initialisation
    void Start()
    {
        GameObject reflectionCameraGo = new GameObject("ReflectionCamera");
        reflectionCamera = reflectionCameraGo.AddComponent<Camera>();
        reflectionCamera.enabled = false;
        MainCamera = Camera.main;
        renderTarget = new RenderTexture(Screen.width, Screen.height, 24);
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalFloat("reflectionFactor", reflectionFactor);
    }

    private void OnPostRender()
    {
        RenderReflection();
    }

    // This will handle the logic for reflection
    void RenderReflection()
    {
        reflectionCamera.CopyFrom(MainCamera);

        Vector3 cameraDirectionWorldSpace = MainCamera.transform.forward;
        Vector3 cameraUpWorldSpace = MainCamera.transform.up;
        Vector3 cameraPositionWorldSpace = MainCamera.transform.position;

        // Transform the vectors to the floor's space
        Vector3 cameraDirectionPlaneSpace = reflectionPlane.transform.InverseTransformDirection(cameraDirectionWorldSpace);
        Vector3 cameraUpPlaneSpace = reflectionPlane.transform.InverseTransformDirection(cameraUpWorldSpace);
        Vector3 cameraPositionPlaneSpace = reflectionPlane.transform.InverseTransformDirection(cameraPositionWorldSpace);

        //mirror the vectors
        cameraDirectionPlaneSpace.y *= -1.0f;
        cameraUpPlaneSpace.y *= -1.0f;
        cameraPositionPlaneSpace.y *= -1.0f;

        // Transform the vectors back to world space
        cameraDirectionPlaneSpace = reflectionPlane.transform.TransformDirection(cameraDirectionPlaneSpace);
        cameraUpWorldSpace = reflectionPlane.transform.TransformDirection(cameraUpPlaneSpace);
        cameraPositionWorldSpace = reflectionPlane.transform.TransformPoint(cameraPositionPlaneSpace);


        // Set camera position and rotation
        reflectionCamera.transform.position = cameraPositionWorldSpace;
        reflectionCamera.transform.LookAt(cameraPositionWorldSpace + cameraDirectionWorldSpace, cameraUpWorldSpace);

        // Set render target for the reflection camera
        reflectionCamera.targetTexture = renderTarget;

        // Render the reflection camera
        reflectionCamera.Render();

        // Draw full screen quad
        DrawQuad();
    }

    void DrawQuad()
    {
        GL.PushMatrix();
        // Use ground Material to draw the quad
        Reflection.SetPass(0);
        Reflection.SetTexture("ReflectionTex", renderTarget);

        GL.LoadOrtho();

        GL.Begin(GL.QUADS);
        GL.TexCoord2(1.0f, 0.0f);
        GL.Vertex3(0.0f, 0.0f, 0.0f);
        GL.TexCoord2(1.0f, 1.0f);
        GL.Vertex3(0.0f, 1.0f, 0.0f);
        GL.TexCoord2(0.0f, 1.0f);
        GL.Vertex3(1.0f, 1.0f, 0.0f);
        GL.TexCoord2(0.0f, 0.0f);
        GL.Vertex3(1.0f, 0.0f, 0.0f);
        GL.End();

        GL.PopMatrix();
    }
}
