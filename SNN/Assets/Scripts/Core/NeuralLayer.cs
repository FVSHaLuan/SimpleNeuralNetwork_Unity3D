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

        Sigmoid GetNode(int nodeIndex)
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

            float[] output = new float[nodeSize];
            Compute(input, output);
            return output;
        }

        public void Compute(float[] input, float[] output)
        {
            if (output.Length != nodeSize)
            {
                throw new System.ArgumentException();
            }

            for (int i = 0; i < nodeSize; i++)
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