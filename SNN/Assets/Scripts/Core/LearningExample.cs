using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    [System.Serializable]
    public class LearningExample
    {
        [SerializeField]
        string label;
        [SerializeField]
        float[] input;
        [SerializeField]
        float[] output;

        public float[] Input
        {
            get
            {
                return input;
            }

            set
            {
                input = value;
            }
        }

        public float[] Output
        {
            get
            {
                return output;
            }

            set
            {
                output = value;
            }
        }

        public LearningExample(int inputSize, int outputSize)
        {
            input = new float[inputSize];
            output = new float[outputSize];
        }
    }

}