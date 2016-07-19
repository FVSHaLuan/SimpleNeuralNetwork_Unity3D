using UnityEngine;
using System.Collections;
using System;

namespace SNN.Core
{
    [CreateAssetMenu]
    public class NeuralNet : ScriptableObject, ITrainableNeuralNet
    {
        [SerializeField, HideInInspector]
        int inputSize;
        [SerializeField, HideInInspector]
        int outputSize;
        [SerializeField, HideInInspector]
        NeuralLayer[] neuralLayers;
        [SerializeField, HideInInspector]
        bool initialized = false;

        [SerializeField, Multiline]
        string description;

        [NonSerialized]
        BiasAccessor biasAccessor = null;
        [NonSerialized]
        WeightAccessor weightAccessor = null;

        public bool Initialized
        {
            get
            {
                return initialized;
            }
        }

        public void Initialize(params int[] layersNodes)
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
            neuralLayers = new NeuralLayer[layersNodes.Length - 1];
            int currentNumberOfWeights = inputSize;
            for (int i = 0; i < neuralLayers.Length; i++)
            {
                neuralLayers[i] = new NeuralLayer(currentNumberOfWeights, layersNodes[i + 1]);
                currentNumberOfWeights = layersNodes[i + 1];
            }

            initialized = true;
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
        public float[] Compute(float[] input)
        {
            if (!initialized)
            {
                throw new System.InvalidOperationException();
            }

            float[] currentResult = input;
            for (int i = 0; i < neuralLayers.Length; i++)
            {
                currentResult = neuralLayers[i].Compute(currentResult);
            }
            return currentResult;
        }
        #endregion

        #region ITrainableNeuralNet
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

        public void Learn(LearningExample[] learningExample, float learningRate)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }

}