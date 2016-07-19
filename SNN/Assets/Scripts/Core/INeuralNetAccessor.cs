using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    public interface INeuralNetAccessor
    {
        BiasAccessor BiasAccessor { get; }
        WeightAccessor WeightAccessor { get; }
    }

}