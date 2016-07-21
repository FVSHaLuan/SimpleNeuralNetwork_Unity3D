using UnityEngine;
using System.Collections;
using SNN.Core;
using UnityEditor;

namespace SNN.Test
{
    public class NeuralNetSpawner : ScriptableObject
    {
        [SerializeField]
        int[] layersNodes = new int[3] { 2, 2, 2 };
        [SerializeField]
        Object path;

        static NeuralNetSpawner current;

        [MenuItem("SNN/Spawn NeuralNet...")]
        public static void MenuItem()
        {
            if (current == null)
            {
                current = CreateInstance<NeuralNetSpawner>();
            }
            Selection.activeObject = current;
        }

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            NeuralNet neuralNet = CreateInstance<NeuralNet>();
            neuralNet.Initialize(layersNodes);
            string newAssetPath = string.Format("{0}/New NeuralNet_{1}.asset", AssetDatabase.GetAssetPath(path), System.DateTime.Now.Ticks);
            AssetDatabase.CreateAsset(neuralNet, newAssetPath);
        }
    }

}