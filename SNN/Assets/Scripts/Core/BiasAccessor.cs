using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    public class BiasAccessor
    {
        NeuralLayer[] neuralLayers;

        public BiasAccessor(NeuralLayer[] neuralLayers)
        {
            this.neuralLayers = neuralLayers;
        }

        public float this[int layer, int node]
        {
            get
            {
                return neuralLayers[layer].GetNode(node).Bias;
            }

            set
            {
                neuralLayers[layer].GetNode(node).Bias = value;
            }
        }

    }

}