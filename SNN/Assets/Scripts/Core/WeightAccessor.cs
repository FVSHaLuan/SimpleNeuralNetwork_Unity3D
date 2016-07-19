using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    public class WeightAccessor
    {
        NeuralLayer[] neuralLayers;

        public WeightAccessor(NeuralLayer[] neuralLayers)
        {
            this.neuralLayers = neuralLayers;
        }

        public float this[int layer, int node, int weight]
        {
            get
            {
                return neuralLayers[layer].GetNode(node).Weights[weight];
            }

            set
            {
                neuralLayers[layer].GetNode(node).Weights[weight] = value;
            }
        }
    }

}