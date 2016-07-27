using UnityEngine;
using System.Collections;
using UnityEditor;

namespace SNN.Core
{
    [CustomEditor(typeof(NeuralNet))]
    public class NeuralNetInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            NeuralNet neuralNet = target as NeuralNet;
            if (neuralNet.Initialized)
            {
                EditorGUILayout.HelpBox(string.Format("InputSize: {0}\nCost funtion: {1}", neuralNet.InputSize, neuralNet.CostFunction), MessageType.Info, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Not initialized", MessageType.Info, true);
            }
        }
    }

}