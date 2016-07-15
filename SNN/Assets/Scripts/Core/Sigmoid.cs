using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    [System.Serializable]
    public class Sigmoid
    {
        [SerializeField, HideInInspector]
        int numberOfWeights;
        [SerializeField, HideInInspector]
        float[] weights;
        [SerializeField, HideInInspector]
        float bias;

        public float Bias
        {
            get
            {
                return bias;
            }

            set
            {
                bias = value;
            }
        }

        public float[] Weights
        {
            get
            {
                return weights;
            }
        }

        public int NumberOfWeights
        {
            get
            {
                return numberOfWeights;
            }
        }

        public Sigmoid(int numberOfWeights)
        {
            this.numberOfWeights = numberOfWeights;
            bias = Random.Range(0.0f, 1.0f);
            weights = InitializeWeights(numberOfWeights);
        }

        public float Compute(float[] input)
        {
            if (input.Length != NumberOfWeights)
            {
                throw new System.ArgumentException();
            }

            return 1 / (1 + (float)System.Math.Exp((-WeightedSum(input) - Bias)));
        }

        float WeightedSum(float[] input)
        {
            float weightedSum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                weightedSum += input[i] * Weights[i];
            }
            return weightedSum;
        }

        float[] InitializeWeights(int numberOfWeights)
        {
            float[] weights = new float[numberOfWeights];
            for (int i = 0; i < numberOfWeights; i++)
            {
                weights[i] = Random.Range(0.0f, 1.0f);
            }
            return weights;
        }

    }

}