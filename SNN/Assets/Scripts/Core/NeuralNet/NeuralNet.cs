using UnityEngine;
using System.Collections;
using System;

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

            neuralNetAccessor = new NeuralNetAccessor(layersNodes);

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

            return neuralNetAccessor.Compute(input);
        }
        public void Compute(float[] input, float[] output)
        {
            neuralNetAccessor.Compute(input, output);
        }
        #endregion
    }

}