using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    public static class CostHelper
    {
        public static float ComputeCost(NeuralNet neuralNet, LearningExample[] learningExamples)
        {
            switch (neuralNet.CostFunction)
            {
                case NeuralNet.CostFuntionKind.Quadratic:
                    return QuadraticWholeCost(neuralNet, learningExamples);
                case NeuralNet.CostFuntionKind.CrossEntropy:
                    return CrossEntropyWholeCost(neuralNet, learningExamples);
                default:
                    throw new System.NotImplementedException();
            }
        }

        #region Quadratic
        static float QuadraticSingleCost(float[] expectedOutput, float[] realOutput)
        {
            float singleCost = 0;
            for (int i = 0; i < expectedOutput.Length; i++)
            {
                singleCost += Mathf.Pow(expectedOutput[i] - realOutput[i], 2);
            }
            return singleCost / 2.0f;
        }

        static float QuadraticWholeCost(NeuralNet neuralNet, LearningExample[] learningExamples)
        {
            float wholeCost = 0;
            for (int i = 0; i < learningExamples.Length; i++)
            {
                wholeCost += QuadraticSingleCost(learningExamples[i].Output, neuralNet.Compute(learningExamples[i].Input));
            }
            return wholeCost / learningExamples.Length;
        }
        #endregion

        #region Cross Entropy     
        static float CrossEntropySingleCost(float[] expectedOutput, float[] realOutput)
        {
            float singleCost = 0;
            float n = expectedOutput.Length;

            for (int i = 0; i < n; i++)
            {
                float y = expectedOutput[i];
                float a = realOutput[i];
                singleCost += y * Mathf.Log(a) + (1 - y) * Mathf.Log(1 - a);
            }

            return singleCost;
        }
        static float CrossEntropyWholeCost(NeuralNet neuralNet, LearningExample[] learningExamples)
        {
            float wholeCost = 0;
            for (int i = 0; i < learningExamples.Length; i++)
            {
                wholeCost += CrossEntropySingleCost(learningExamples[i].Output, neuralNet.Compute(learningExamples[i].Input));
            }
            return -wholeCost / learningExamples.Length;
        }
        #endregion
    }

}