using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    public interface IBackPropagation
    {
        void BackPropagate(LearningExample learningExample, INeuralNetAccessor outputGradient);
    }

}