using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    [ExecuteInEditMode]
    [RequireComponent (typeof (Camera))]
    public class EdgeDetection : MonoBehaviour
    {
        public float sensitivityDepth = 1.0f;
        public float sensitivityNormals = 1.0f;
        public float lumThreshold = 0.2f;
        public float sampleDist = 1.0f;
        public float edgesOnly = 0.0f;
        public Color edgesOnlyBgColor = Color.white;

        public Shader edgeDetectShader;
        private Material edgeDetectMaterial = null;

        public bool CheckResources ()
		{
            edgeDetectMaterial = new Material (edgeDetectShader);
            if (edgeDetectMaterial)
            {
                edgeDetectMaterial.hideFlags = HideFlags.DontSave;
            }

            return true;
        }


        [ImageEffectOpaque]
        void OnRenderImage (RenderTexture source, RenderTexture destination)
		{
            if (CheckResources () == false)
			{
                Graphics.Blit (source, destination);
                return;
            }

            edgeDetectMaterial.SetFloat ("_BgFade", edgesOnly);
            edgeDetectMaterial.SetFloat ("_SampleDistance", sampleDist);
            edgeDetectMaterial.SetVector ("_BgColor", edgesOnlyBgColor);
            edgeDetectMaterial.SetFloat ("_Threshold", lumThreshold);

            Graphics.Blit (source, destination, edgeDetectMaterial);
        }
    }
}
