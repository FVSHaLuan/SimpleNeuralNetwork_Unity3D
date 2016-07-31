using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    public interface INeuralNetAccessor
    {
        BiasAccessor BiasAccessor { get; }
        WeightAccessor WeightAccessor { get; }

        int NumberOfLayers
        {
            get;
        }

        int NodesInLayer(int layer);

        int WeightsOfNodeInLayer(int layer);
    }

}