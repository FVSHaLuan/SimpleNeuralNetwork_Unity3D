using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    [System.Serializable]
    public class NeuralLayer
    {
        [SerializeField, HideInInspector]
        Sigmoid[] nodes;
        [SerializeField, HideInInspector]
        int inputSize;
        [SerializeField, HideInInspector]
        int nodeSize;

        public int NodeSize
        {
            get
            {
                return nodeSize;
            }
        }

        public Sigmoid GetNode(int nodeIndex)
        {
            return nodes[nodeIndex];
        }

        public NeuralLayer(int inputSize, int nodeSize)
        {
            this.inputSize = inputSize;
            this.nodeSize = nodeSize;
            InitializeNodes(nodeSize, inputSize);
        }

        public float[] Compute(float[] input)
        {
            if (input.Length != inputSize)
            {
                throw new System.ArgumentException();
            }

            float[] output = new float[NodeSize];
            Compute(input, output);
            return output;
        }

        public void ComputeWithWeightedInputs(float[] weightedInput, float[] activations)
        {
            for (int i = 0; i < NodeSize; i++)
            {
                activations[i] = nodes[i].Compute(weightedInput[i]);
            }
        }

        public void GetWeightedInputs(float[] input,float[] weightedInput)
        {
            for (int i = 0; i < weightedInput.Length; i++)
            {
                weightedInput[i] = nodes[i].GetWeightedInput(input);
            }
        }

        public void Compute(float[] input, float[] output)
        {
            if (output.Length != NodeSize)
            {
                throw new System.ArgumentException();
            }

            for (int i = 0; i < NodeSize; i++)
            {
                output[i] = nodes[i].Compute(input);
            }
        }

        void InitializeNodes(int numberOfNodes, int numberOfWeights)
        {
            nodes = new Sigmoid[numberOfNodes];
            for (int i = 0; i < numberOfNodes; i++)
            {
                nodes[i] = new Sigmoid(numberOfWeights);
            }
        }
    }

}