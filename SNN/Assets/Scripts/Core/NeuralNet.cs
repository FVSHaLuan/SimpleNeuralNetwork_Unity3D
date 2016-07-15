using UnityEngine;
using System.Collections;
using System;

namespace SNN.Core
{
    [CreateAssetMenu]
    public class NeuralNet : ScriptableObject, INeuralNet
    {
        [SerializeField, HideInInspector]
        int inputSize;
        [SerializeField, HideInInspector]
        NeuralLayer[] neuralLayers;
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

        #region INeuralNet
        public int InputSize
        {
            get
            {
                return inputSize;
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
            neuralLayers = new NeuralLayer[layersNodes.Length - 1];
            int currentNumberOfWeights = inputSize;
            for (int i = 0; i < neuralLayers.Length; i++)
            {
                neuralLayers[i] = new NeuralLayer(currentNumberOfWeights, layersNodes[i + 1]);
                currentNumberOfWeights = layersNodes[i + 1];
            }

            initialized = true;
        }
    }

}