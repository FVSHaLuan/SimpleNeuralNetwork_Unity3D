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
            if ((target as NeuralNet).Initialized)
            {
                EditorGUILayout.HelpBox(string.Format("InputSize: {0}", (target as NeuralNet).InputSize), MessageType.Info, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Not initialized", MessageType.Info, true);
            }
        }
    }

}