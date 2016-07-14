using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    [System.Serializable]
    public class Sigmoid
    {
        [SerializeField]
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

            set
            {
                weights = value;
            }
        }

        public int NumberOfWeights
        {
            get
            {
                return numberOfWeights;
            }

            private set
            {
                numberOfWeights = value;
            }
        }

        public Sigmoid(int numberOfWeights)
        {
            Weights = new float[numberOfWeights];
            NumberOfWeights = numberOfWeights;
        }

        public float Compute(float[] input)
        {
            throw new System.NotImplementedException();
        }
    }

}