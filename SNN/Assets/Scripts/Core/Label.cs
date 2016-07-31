using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    [System.Serializable]
    public class Label
    {
        [SerializeField]
        float[] activations;
        [SerializeField]
        string name;

        public float[] Activations
        {
            get
            {
                return activations;
            }

            set
            {
                activations = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
    }

}