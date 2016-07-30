using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    public partial class NeuralNet
    {
        public enum RegularizationMethodKind
        {
            None,
            L2,
            L1
        }

        [Header("Regularization")]
        [SerializeField]
        RegularizationMethodKind regularizationMethod = RegularizationMethodKind.L2;
        [SerializeField]
        float regularizationRate = 0.01f;

        public RegularizationMethodKind RegularizationMethod
        {
            get
            {
                return regularizationMethod;
            }

            set
            {
                regularizationMethod = value;
            }
        }

        public float RegularizationRate
        {
            get
            {
                return regularizationRate;
            }

            set
            {
                regularizationRate = value;
            }
        }

        void AddRegularization(RegularizationMethodKind regularizationMethod, NeuralNetAccessor costFunctionGradient, float regularizationRate)
        {
            switch (regularizationMethod)
            {
                case RegularizationMethodKind.None:
                    break;
                case RegularizationMethodKind.L2:
                    AddRegularizationL2(costFunctionGradient, regularizationRate);
                    break;
                case RegularizationMethodKind.L1:
                    AddRegularizationL1(costFunctionGradient, regularizationRate);
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        void AddRegularizationL2(NeuralNetAccessor costFunctionGradient, float regularizationRate)
        {
            for (int layer = 0; layer < costFunctionGradient.NumberOfLayers; layer++)
            {
                for (int node = 0; node < costFunctionGradient.NodesInLayer(layer); node++)
                {
                    var costFunctionGradientWeights = costFunctionGradient.GetSigmoid(layer, node).Weights;
                    var neuralNetWeights = neuralNetAccessor.GetSigmoid(layer, node).Weights;
                    for (int i = 0; i < costFunctionGradientWeights.Length; i++)
                    {
                        costFunctionGradientWeights[i] += neuralNetWeights[i] * regularizationRate;
                    }
                }
            }
        }

        void AddRegularizationL1(NeuralNetAccessor costFunctionGradient, float regularizationRate)
        {
            for (int layer = 0; layer < costFunctionGradient.NumberOfLayers; layer++)
            {
                for (int node = 0; node < costFunctionGradient.NodesInLayer(layer); node++)
                {
                    var costFunctionGradientWeights = costFunctionGradient.GetSigmoid(layer, node).Weights;
                    var neuralNetWeights = neuralNetAccessor.GetSigmoid(layer, node).Weights;
                    for (int i = 0; i < costFunctionGradientWeights.Length; i++)
                    {
                        costFunctionGradientWeights[i] += Mathf.Sign(neuralNetWeights[i]) * regularizationRate;
                    }
                }
            }
        }
    }
}
