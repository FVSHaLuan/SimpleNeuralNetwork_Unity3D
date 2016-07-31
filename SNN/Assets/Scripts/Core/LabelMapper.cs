using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SNN.Core
{
    [CreateAssetMenu]
    public class LabelMapper : ScriptableObject
    {
        [SerializeField]
        List<Label> labels;

        [Header("Test")]
        [SerializeField]
        float[] testActivations;

        public string GetName(float[] activations)
        {
            float squareDistance = CostHelper.SquareDistance(activations, labels[0].Activations);
            string name = labels[0].Name;
            for (int i = 1; i < labels.Count; i++)
            {
                float tmpSquareDistance = CostHelper.SquareDistance(activations, labels[i].Activations);
                if (tmpSquareDistance < squareDistance)
                {
                    squareDistance = tmpSquareDistance;
                    name = labels[i].Name;
                }
            }

            return name;
        }

        [ContextMenu("ComputeTest")]
        public void ComputeTest()
        {
            Debug.Log(GetName(testActivations));
        }
    }

}