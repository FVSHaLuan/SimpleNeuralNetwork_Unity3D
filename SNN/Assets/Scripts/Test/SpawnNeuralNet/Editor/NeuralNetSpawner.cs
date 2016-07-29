using UnityEngine;
using System.Collections;
using SNN.Core;
using UnityEditor;

namespace SNN.Test
{
    public class NeuralNetSpawner : ScriptableObject
    {
        [SerializeField]
        string fileName = "";
        [SerializeField]
        NeuralNet.CostFuntionKind costFunction;
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
            current.fileName = string.Format("New NeuralNet_{0}", System.DateTime.Now.Ticks);
            Selection.activeObject = current;
        }

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            NeuralNet neuralNet = CreateInstance<NeuralNet>();
            neuralNet.Initialize(costFunction, layersNodes);
            string newAssetPath = string.Format("{0}/{1}.asset", AssetDatabase.GetAssetPath(path), fileName);
            AssetDatabase.CreateAsset(neuralNet, newAssetPath);
        }
    }

}