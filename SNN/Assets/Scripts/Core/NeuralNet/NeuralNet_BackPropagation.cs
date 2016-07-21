using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SNN.Core
{
    public partial class NeuralNet
    {
        const float smallDelta = 0.001f;

        [System.NonSerialized]
        bool initializedBackpropagation = false;

        List<float[]> weightedInputs;
        List<float[]> activations;
        List<float[]> deltas;

        void BackPropagation(LearningExample learningExample, INeuralNetAccessor outputGradient)
        {
            InitializeBackpropagation();
            ComputeForward(learningExample.Input);
            ComputeBackward(learningExample, outputGradient);
        }

        void ComputeForward(float[] input)
        {
            float[] currentInput = input;

            for (int i = 0; i < neuralNetAccessor.NumberOfLayer; i++)
            {
                neuralNetAccessor.GetNeuralLayer(i).GetWeightedInputs(currentInput, weightedInputs[i]);
                neuralNetAccessor.GetNeuralLayer(i).ComputeWithWeightedInputs(weightedInputs[i], activations[i]);
                currentInput = activations[i];
            }
        }

        void ComputeBackward(LearningExample learningExample, INeuralNetAccessor outputGradient)
        {
            int lastLayer = neuralNetAccessor.NumberOfLayer - 1;
            ComputeLastDeltas(learningExample.Output, outputGradient);
            ComputeWeightGradients(lastLayer, learningExample.Input, outputGradient);
            for (int layer = lastLayer - 1; layer >= 0; layer--)
            {
                ComputeDeltas(layer, outputGradient);
                ComputeWeightGradients(layer, learningExample.Input, outputGradient);
            }
        }

        void ComputeWeightGradients(int layer, float[] learningExampleInput, INeuralNetAccessor outputGradient)
        {
            int numberOfNodes = neuralNetAccessor.NodesInLayer(layer);
            int numberOfWeights = neuralNetAccessor.WeightsOfNodeInLayer(layer);

            for (int node = 0; node < numberOfNodes; node++)
            {
                for (int weight = 0; weight < numberOfWeights; weight++)
                {
                    float a;
                    if (layer == 0)
                    {
                        a = learningExampleInput[weight];
                    }
                    else
                    {
                        a = activations[layer - 1][weight];
                    }
                    outputGradient.WeightAccessor[layer, node, weight] = a * outputGradient.BiasAccessor[layer, node];
                }
            }
        }

        void ComputeDeltas(int layer, INeuralNetAccessor outputGradient)
        {
            float sigmoidDash;
            float error;

            int numberOfNodes = neuralNetAccessor.NodesInLayer(layer);
            int numberOfNextLayerNodes = neuralNetAccessor.NodesInLayer(layer + 1);
            NeuralLayer neuralLayer = neuralNetAccessor.GetNeuralLayer(layer);

            for (int node = 0; node < numberOfNodes; node++)
            {
                // Compute sigmoidDash
                sigmoidDash = neuralLayer.GetNode(node).Compute(weightedInputs[layer][node] + smallDelta) - activations[layer][node];

                // Compute error
                error = 0;
                for (int nextNode = 0; nextNode < numberOfNextLayerNodes; nextNode++)
                {
                    error += outputGradient.BiasAccessor[layer + 1, nextNode] * outputGradient.WeightAccessor[layer, nextNode, node];
                }

                // Comput delta
                outputGradient.BiasAccessor[layer, node] = sigmoidDash * error;
            }
        }
        void ComputeLastDeltas(float[] learningExampleOutput, INeuralNetAccessor outputGradient)
        {
            float sigmoidDash;
            float deltaCa;

            int lastLayer = neuralNetAccessor.NumberOfLayer - 1;
            int numberOfFinalNodes = neuralNetAccessor.NodesInLayer(lastLayer);
            NeuralLayer lastNeuralLayer = neuralNetAccessor.GetNeuralLayer(lastLayer);

            for (int i = 0; i < numberOfFinalNodes; i++)
            {
                sigmoidDash = lastNeuralLayer.GetNode(i).Compute(weightedInputs[lastLayer][i] + smallDelta) - activations[lastLayer][i];
                deltaCa = activations[lastLayer][i] - learningExampleOutput[i];
                outputGradient.BiasAccessor[lastLayer, i] = sigmoidDash * deltaCa;
            }
        }

        void InitializeBackpropagation()
        {
            if (initializedBackpropagation)
            {
                return;
            }

            // Initialize weightedInputs
            weightedInputs = new List<float[]>(neuralNetAccessor.NumberOfLayer);
            for (int i = 0; i < neuralNetAccessor.NumberOfLayer; i++)
            {
                weightedInputs.Add(new float[neuralNetAccessor.WeightsOfNodeInLayer(i)]);
            }

            // Initialize activations
            activations = new List<float[]>(neuralNetAccessor.NumberOfLayer);
            for (int i = 0; i < neuralNetAccessor.NumberOfLayer; i++)
            {
                activations.Add(new float[neuralNetAccessor.NodesInLayer(i)]);
            }

            // Initialize deltas
            deltas = new List<float[]>(neuralNetAccessor.NumberOfLayer);
            for (int i = 0; i < neuralNetAccessor.NumberOfLayer; i++)
            {
                deltas.Add(new float[neuralNetAccessor.NodesInLayer(i)]);
            }

            initializedBackpropagation = true;
        }

    }

}