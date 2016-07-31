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
        NeuralNet.RegularizationMethodKind regularizationMethod = NeuralNet.RegularizationMethodKind.L2;
        [SerializeField]
        float regularizationRate = 0.01f;
        [SerializeField]
        int iterations = 100;
        [SerializeField]
        LearningExample[] learningExamples;

        [Header("Test")]
        [SerializeField]
        float[] testInput;
        [SerializeField]
        float[] computedOutput;
        
        [ContextMenu("InitializeLearningExample")]
        void InitializeLearningExample()
        {
            Undo.RecordObject(this, "InitializeLearningExample");

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
            Undo.RecordObject(this, "InitializeTestInput");

            testInput = new float[neuralNet.InputSize];
        }

        [ContextMenu("ComputeCost")]
        void ComputeCost()
        {
            Undo.RecordObject(this, "ComputeCost");

            Debug.Log(string.Format("Trainer {0}, cost = {1}", name, CostHelper.ComputeCost(neuralNet, learningExamples)));
        }

        [ContextMenu("Train")]
        void Train()
        {
            Undo.RecordObject(neuralNet, "Train");

            neuralNet.RegularizationMethod = regularizationMethod;
            neuralNet.RegularizationRate = regularizationRate;

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
            Undo.RecordObject(this, "Test");

            computedOutput = neuralNet.Compute(testInput);
        }
    }

}
#endif