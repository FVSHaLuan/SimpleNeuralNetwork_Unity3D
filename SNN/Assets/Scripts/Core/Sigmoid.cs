using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    [System.Serializable]
    public class Sigmoid
    {
        [SerializeField, HideInInspector]
        int numberOfWeights;
        [SerializeField]
        float[] weights;
        [SerializeField]
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
            bias = Random.Range(-10, 10);
            weights = InitializeWeights(numberOfWeights);
        }

        public float Compute(float[] input)
        {
            if (input.Length != NumberOfWeights)
            {
                throw new System.ArgumentException();
            }
            return Compute(GetWeightedInput(input));
        }
        public float Compute(float weightedInput)
        {
            return 1.0f / (1.0f + Mathf.Exp(-weightedInput));
        }

        public float GetWeightedInput(float[] input)
        {
            return WeightedSum(input) + Bias;
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
                weights[i] = Random.Range(-10, 10);
            }
            return weights;
        }

    }

}