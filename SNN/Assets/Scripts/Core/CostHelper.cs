using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    public static class CostHelper
    {
        public static float ComputeCost(NeuralNet neuralNet, LearningExample[] learningExamples)
        {
            float cost = 0;

            // C0
            switch (neuralNet.CostFunction)
            {
                case NeuralNet.CostFuntionKind.Quadratic:
                    cost = QuadraticWholeCost(neuralNet, learningExamples);
                    break;
                case NeuralNet.CostFuntionKind.CrossEntropy:
                    cost = CrossEntropyWholeCost(neuralNet, learningExamples);
                    break;
                default:
                    throw new System.NotImplementedException();
            }

            // Regularization
            switch (neuralNet.RegularizationMethod)
            {
                case NeuralNet.RegularizationMethodKind.None:
                    break;
                case NeuralNet.RegularizationMethodKind.L2:
                    cost += L2Cost(neuralNet, learningExamples.Length);
                    break;
                case NeuralNet.RegularizationMethodKind.L1:
                    cost += L1Cost(neuralNet, learningExamples.Length);
                    break;
                default:
                    throw new System.NotImplementedException();
            }

            //
            return cost;
        }

        #region Quadratic
        static float QuadraticSingleCost(float[] expectedOutput, float[] realOutput)
        {
            return SquareDistance(expectedOutput, realOutput) / 2.0f;
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

        public static float SquareDistance(float[] x, float[] y)
        {
            float sd = 0;
            for (int i = 0; i < x.Length; i++)
            {
                sd += Mathf.Pow(x[i] - y[i], 2);
            }
            return sd;
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

                float singleCost = CrossEntropySingleCost(learningExamples[i].Output, neuralNet.Compute(learningExamples[i].Input));
                wholeCost += singleCost;
            }
            return -wholeCost / learningExamples.Length;
        }
        #endregion

        #region Regularization
        static float L2Cost(NeuralNet neuralNet, int numberOfTests)
        {
            float l2Cost = 0;

            for (int layer = 0; layer < neuralNet.NumberOfLayers; layer++)
            {
                for (int node = 0; node < neuralNet.NodesInLayer(layer); node++)
                {
                    for (int weight = 0; weight < neuralNet.WeightsOfNodeInLayer(layer); weight++)
                    {
                        l2Cost += Mathf.Pow(neuralNet.WeightAccessor[layer, node, weight], 2);
                    }
                }
            }

            return l2Cost * neuralNet.RegularizationRate / 2.0f / numberOfTests;
        }

        static float L1Cost(NeuralNet neuralNet, int numberOfTests)
        {
            float l1Cost = 0;

            for (int layer = 0; layer < neuralNet.NumberOfLayers; layer++)
            {
                for (int node = 0; node < neuralNet.NodesInLayer(layer); node++)
                {
                    for (int weight = 0; weight < neuralNet.WeightsOfNodeInLayer(layer); weight++)
                    {
                        l1Cost += Mathf.Abs(neuralNet.WeightAccessor[layer, node, weight]);
                    }
                }
            }

            return l1Cost * neuralNet.RegularizationRate / numberOfTests;
        }
        #endregion
    }

}