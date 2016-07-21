#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using SNN.Core;
using UnityEditor;

namespace SNN.Train
{
    [CreateAssetMenu]
    public class Trainer : ScriptableObject
    {
        [SerializeField]
        NeuralNet neuralNet;

        [Header("Learning parameters")]
        [SerializeField]
        float learningRate = 0.01f;
        [SerializeField]
        int iterations = 100;
        [SerializeField]
        LearningExample[] learningExamples;

        [Header("Test")]
        [SerializeField]
        float[] testInput;
        [SerializeField]
        float[] computedOutput;

        float SingleCost(float[] expectedOutput, float[] realOutput)
        {
            float singleCost = 0;
            for (int i = 0; i < expectedOutput.Length; i++)
            {
                singleCost += Mathf.Pow(expectedOutput[i] - realOutput[i], 2);
            }
            return singleCost / 2.0f;
        }

        float WholeCost(LearningExample[] learningExamples)
        {
            float wholeCost = 0;
            for (int i = 0; i < learningExamples.Length; i++)
            {
                wholeCost += SingleCost(learningExamples[i].Output, neuralNet.Compute(learningExamples[i].Input));
            }
            return wholeCost / learningExamples.Length;
        }

        [ContextMenu("InitializeLearningExample")]
        void InitializeLearningExample()
        {
            int inputSize = neuralNet.InputSize;
            int outputSize = neuralNet.OutputSize;

            for (int i = 0; i < learningExamples.Length; i++)
            {
                learningExamples[i] = new LearningExample(inputSize, outputSize);
            }
        }

        [ContextMenu("InitializeTestInput")]
        void InitializeTestInput()
        {
            testInput = new float[neuralNet.InputSize];
        }

        [ContextMenu("ComputeCost")]
        void ComputeCost()
        {
            Debug.Log(string.Format("Trainer {0}, cost = {1}", name, WholeCost(learningExamples)));
        }

        [ContextMenu("Train")]
        void Train()
        {
            Undo.RecordObject(neuralNet, "Train");

            for (int i = 0; i < iterations; i++)
            {

                if (!EditorUtility.DisplayCancelableProgressBar("Training...", string.Format("iteration = {0}/{1}", i + 1, iterations), (float)i / iterations))
                {
                    neuralNet.Learn(learningExamples, learningRate);
                }
                else
                {
                    Debug.Log(string.Format("Training cancelled at iteration = {0}", i));
                    EditorUtility.ClearProgressBar();
                    break;
                }
            }

            EditorUtility.ClearProgressBar();
        }

        [ContextMenu("Test")]
        void Test()
        {
            computedOutput = neuralNet.Compute(testInput);
        }
    }

}
#endif