using UnityEngine;
using System.Collections;
using UnityEditor;
using SNN.Core;

namespace SNN.Test
{
    public class ComputeNeuralNetUsingRawData : ScriptableObject
    {
        static ComputeNeuralNetUsingRawData current;

        [SerializeField]
        NeuralNet neuralNet;
        [SerializeField]
        float[] input;
        [SerializeField]
        float[] output;

        [MenuItem("Test/Compute/Using raw data...")]
        public static void MenuItem()
        {
            if (current == null)
            {
                current = CreateInstance<ComputeNeuralNetUsingRawData>();
            }
            Selection.activeObject = current;
        }

        [ContextMenu("Compute")]
        void Compute()
        {
            output = neuralNet.Compute(input);
        }

        [ContextMenu("Randomize input")]
        void RandomizeInput()
        {
            input = new float[neuralNet.InputSize];
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = Random.Range(0.0f, 1.0f);
            }
        }

        [ContextMenu("RandomizeInputThenCompute")]
        void RandomizeInputThenCompute()
        {
            RandomizeInput();
            Compute();
        }
    }
}
