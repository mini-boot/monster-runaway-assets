using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace FXV.ShieldEditorUtils
{
    [CustomEditor(typeof(ShieldPostprocess), true)]
    public class fxvShieldPostprocessEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            ShieldPostprocess p = (ShieldPostprocess)target;
            serializedObject.Update();

            List<string> hiddenProperties = new List<string>();

            if (GraphicsSettings.currentRenderPipeline != null) //not BuiltIn ?
            {
                hiddenProperties.Add("drawOrder");
            }

            if (!p.PostprocesOnSeparateAxes())
            {
                hiddenProperties.Add("kernelRadiusVertical");
                hiddenProperties.Add("sigmaVertical");
                hiddenProperties.Add("sampleStepVertical");
            }

            if (!p.IsGloballIlluminationSupported())
            {
                hiddenProperties.Add("globalIllumination");
            }

            if (!p.IsGloballIlluminationSupported() || !p.IsGloballIlluminationEnabled())
            {
                hiddenProperties.Add("giSampleRadius");
                hiddenProperties.Add("giNumberOfSamples");
                hiddenProperties.Add("giIntensity");
                hiddenProperties.Add("giLightRange");
                hiddenProperties.Add("giDenoiseStepWidth");
                hiddenProperties.Add("giDenoiseStepChange");
                hiddenProperties.Add("giDenoiseIterations");
                hiddenProperties.Add("giAtIteration");
            }

            DrawPropertiesExcluding(serializedObject, hiddenProperties.ToArray());

            if (!p.IsGloballIlluminationSupported())
            {
                EditorGUILayout.LabelField("    [INFO] Global Illumination is only avaialble in Deffered rendering.");
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}