using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Postprocess : MonoBehaviour
{

    public Material PostprocessMaterial;
    public Material NormalRender;

    public enum EdgeDetectMode
    {
        TriangleDepthNormals = 0,
        RobertsCrossDepthNormals = 1,
        SobelDepth = 2,
        SobelDepthThin = 3,
        TriangleLuminance = 4,
    }

    public EdgeDetectMode mode = EdgeDetectMode.TriangleDepthNormals;

    public float sensitivityDepth = 1.0f;
    public float sensitivityNormals = 1.0f;
    public float lumThreshold = 0.2f;
    public float edgeExp = 1.0f;
    public float sampleDist = 1.0f;
    public float edgesOnly = 0.0f;
    public Color edgesOnlyBgColor = Color.white;

    public RenderTexture CameraRT;
    public RenderTexture Temp;

    public void Start()
    {
        Camera.main.depthTextureMode = DepthTextureMode.DepthNormals;// depthnormals are needed for outline effect

        // create new render textures
        CameraRT = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGBHalf);
        Temp = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGBHalf);

        Camera.main.targetTexture = CameraRT;// set one of the textures to the main camera
    }

    void OnPostRender()
    {
        // set edge detection variables
        Vector2 sensitivity = new Vector2(sensitivityDepth, sensitivityNormals);
        PostprocessMaterial.SetVector("_Sensitivity", new Vector4(sensitivity.x, sensitivity.y, 1.0f, sensitivity.y));
        PostprocessMaterial.SetFloat("_BgFade", edgesOnly);
        PostprocessMaterial.SetFloat("_SampleDistance", sampleDist);
        PostprocessMaterial.SetVector("_BgColor", edgesOnlyBgColor);
        PostprocessMaterial.SetFloat("_Exponent", edgeExp);
        PostprocessMaterial.SetFloat("_Threshold", lumThreshold);

        // Set the targets for the Blit as the buffer, with the depth buffer of the main camera
        Graphics.SetRenderTarget(Temp.colorBuffer, CameraRT.depthBuffer);
        // render normal, clean render
        Graphics.Blit(CameraRT, NormalRender);
        // render effect over it
        Graphics.Blit(CameraRT, PostprocessMaterial, (int)mode);

        // release rendertexture, next blit is to the screen
        RenderTexture.active = null;
        // render result to the screen
        Graphics.Blit(Temp, NormalRender);
    }

}