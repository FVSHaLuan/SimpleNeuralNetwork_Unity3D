using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    [System.Serializable]
    public class NeuralLayer
    {
        [SerializeField]
        Sigmoid[] nodes;

        public NeuralLayer(int size)
        {
            nodes = new Sigmoid[size];
        }        
    }

}