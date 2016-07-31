using UnityEngine;
using System.Collections;
using System;
using TestSimpleRNG;

namespace SNN.Core
{
    [CreateAssetMenu]
    public partial class NeuralNet : ScriptableObject, ITrainableNeuralNet
    {
        [SerializeField]
        NeuralNetAccessor neuralNetAccessor;
        [SerializeField, HideInInspector]
        int inputSize;
        [SerializeField, HideInInspector]
        int outputSize;
        [SerializeField, HideInInspector]
        bool initialized = false;

        [SerializeField, Multiline]
        string description;

        public bool Initialized
        {
            get
            {
                return initialized;
            }
        }

        public void Initialize(CostFuntionKind costFunction, params int[] layersNodes)
        {
            if (initialized)
            {
                throw new System.InvalidOperationException();
            }

            if (layersNodes.Length < 2)
            {
                throw new System.ArgumentException();
            }

            inputSize = layersNodes[0];
            outputSize = layersNodes[layersNodes.Length - 1];

            neuralNetAccessor = new NeuralNetAccessor(layersNodes);

            // Back propagation
            this.costFunction = costFunction;

            ///
            InitializeWeightsAndBiases();

            //
            initialized = true;
        }

        [ContextMenu("Re-initialize weights and biases")]
        public void InitializeWeightsAndBiases()
        {
            SimpleRNG.SetSeedFromSystemTime();

            for (int layer = 0; layer < neuralNetAccessor.NumberOfLayers; layer++)
            {
                for (int node = 0; node < neuralNetAccessor.NodesInLayer(layer); node++)
                {
                    var sigmoid = neuralNetAccessor.GetSigmoid(layer, node);
                    sigmoid.Bias = (float)SimpleRNG.GetNormal(0, 1);
                    float standardDeviation = 1.0f / Mathf.Sqrt(sigmoid.Weights.Length);
                    for (int i = 0; i < sigmoid.Weights.Length; i++)
                    {
                        sigmoid.Weights[i] = (float)SimpleRNG.GetNormal(0, standardDeviation);
                    }
                }
            }
        }

        #region INeuralNet
        public int InputSize
        {
            get
            {
                return inputSize;
            }
        }
        public int OutputSize
        {
            get
            {
                return outputSize;
            }
        }

        public int NumberOfLayers
        {
            get
            {
                return neuralNetAccessor.NumberOfLayers;
            }
        }

        public float[] Compute(float[] input)
        {
            if (!initialized)
            {
                throw new System.InvalidOperationException();
            }

            return neuralNetAccessor.Compute(input);
        }
        public void Compute(float[] input, float[] output)
        {
            neuralNetAccessor.Compute(input, output);
        }

        public int NodesInLayer(int layer)
        {
            return neuralNetAccessor.NodesInLayer(layer);
        }

        public int WeightsOfNodeInLayer(int layer)
        {
            return neuralNetAccessor.WeightsOfNodeInLayer(layer);
        }
        #endregion
    }

}