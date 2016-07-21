using UnityEngine;
using System.Collections;
using System;

namespace SNN.Core
{
    [Serializable]
    public class NeuralNetAccessor : INeuralNetAccessor
    {
        [SerializeField]
        int[] initializedParameters;

        [SerializeField, HideInInspector]
        NeuralLayer[] neuralLayers;

        [NonSerialized]
        BiasAccessor biasAccessor = null;
        [NonSerialized]
        WeightAccessor weightAccessor = null;

        public int[] InitializedParameters
        {
            get
            {
                return initializedParameters;
            }
        }

        public int NumberOfLayer
        {
            get
            {
                return initializedParameters.Length - 1;
            }
        }

        public int NodesInLayer(int layer)
        {
            return initializedParameters[layer + 1];
        }

        public int WeightsOfNodeInLayer(int layer)
        {
            return initializedParameters[layer];
        }

        public NeuralLayer GetNeuralLayer(int layer)
        {
            return neuralLayers[layer];
        }

        public Sigmoid GetSigmoid(int layer, int sigmoid)
        {
            return neuralLayers[layer].GetNode(sigmoid);
        }

        public float[] Compute(float[] input)
        {
            float[] currentResult = input;
            for (int i = 0; i < neuralLayers.Length; i++)
            {
                currentResult = neuralLayers[i].Compute(currentResult);
            }
            return currentResult;
        }

        public void Compute(float[] input, float[] output)
        {
            throw new System.NotImplementedException();
        }

        #region INeuralNetAccessor
        public BiasAccessor BiasAccessor
        {
            get
            {
                if (biasAccessor == null)
                {
                    biasAccessor = new BiasAccessor(neuralLayers);
                }
                return biasAccessor;
            }
        }

        public WeightAccessor WeightAccessor
        {
            get
            {
                if (weightAccessor == null)
                {
                    weightAccessor = new WeightAccessor(neuralLayers);
                }
                return weightAccessor;
            }
        }
        #endregion

        public NeuralNetAccessor(params int[] layersNodes)
        {
            if (layersNodes.Length < 2)
            {
                throw new System.ArgumentException();
            }

            int inputSize = layersNodes[0];
            neuralLayers = new NeuralLayer[layersNodes.Length - 1];
            int currentNumberOfWeights = inputSize;
            for (int i = 0; i < neuralLayers.Length; i++)
            {
                neuralLayers[i] = new NeuralLayer(currentNumberOfWeights, layersNodes[i + 1]);
                currentNumberOfWeights = layersNodes[i + 1];
            }

            initializedParameters = new int[layersNodes.Length];
            for (int i = 0; i < layersNodes.Length; i++)
            {
                initializedParameters[i] = layersNodes[i];
            }
        }

        public void SetAllBiasesAndWeightsTo(float value)
        {
            for (int layer = 0; layer < NumberOfLayer; layer++)
            {
                for (int node = 0; node < NodesInLayer(layer); node++)
                {
                    BiasAccessor[layer, node] = value;
                    for (int weight = 0; weight < WeightsOfNodeInLayer(layer); weight++)
                    {
                        WeightAccessor[layer, node, weight] = value;
                    }
                }
            }
        }

        public void MultiplyAllBiasesAndWeightsWith(float multiplier)
        {
            for (int layer = 0; layer < NumberOfLayer; layer++)
            {
                for (int node = 0; node < NodesInLayer(layer); node++)
                {
                    BiasAccessor[layer, node] *= multiplier;
                    for (int weight = 0; weight < WeightsOfNodeInLayer(layer); weight++)
                    {
                        WeightAccessor[layer, node, weight] *= multiplier;
                    }
                }
            }
        }

        public void AddAllBiasesAndWeightsWith(INeuralNetAccessor otherNeuralNetAccessor)
        {
            for (int layer = 0; layer < NumberOfLayer; layer++)
            {
                for (int node = 0; node < NodesInLayer(layer); node++)
                {
                    BiasAccessor[layer, node] += otherNeuralNetAccessor.BiasAccessor[layer, node];
                    for (int weight = 0; weight < WeightsOfNodeInLayer(layer); weight++)
                    {
                        WeightAccessor[layer, node, weight] += otherNeuralNetAccessor.WeightAccessor[layer, node, weight];
                    }
                }
            }
        }
    }

}