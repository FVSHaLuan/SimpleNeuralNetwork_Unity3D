using UnityEngine;
using System.Collections;
using System;

namespace SNN.Core
{
    [CreateAssetMenu]
    public class NeuralNet : ScriptableObject, INeuralNet
    {
        [SerializeField]
        int inputSize;
        [SerializeField]
        NeuralLayer[] neuralLayer;

        #region INeuralNet
        public int InputSize
        {
            get
            {
                return inputSize;
            }
        }

        public float[] Compute(float[] input)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}