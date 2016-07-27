using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SNN.Core
{
    public partial class NeuralNet
    {
        public enum CostFuntionKind { Quadratic, CrossEntropy }

        [SerializeField, HideInInspector]
        CostFuntionKind costFunction;

        [System.NonSerialized]
        IBackPropagation backPropagation;

        public CostFuntionKind CostFunction
        {
            get
            {
                return costFunction;
            }
        }

        void InitializeBackPropagation()
        {
            switch (CostFunction)
            {
                case CostFuntionKind.Quadratic:
                    backPropagation = new BackPropagationQuadratic(neuralNetAccessor);
                    break;
                case CostFuntionKind.CrossEntropy:
                    throw new System.NotImplementedException();
                default:
                    throw new System.NotImplementedException();
            }
        }
    }

}