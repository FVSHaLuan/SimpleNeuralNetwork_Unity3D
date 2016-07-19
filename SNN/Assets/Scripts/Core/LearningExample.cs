﻿using UnityEngine;
using System.Collections;

namespace SNN.Core
{
    [System.Serializable]
    public class LearningExample
    {
        [SerializeField]
        float[] input;
        [SerializeField]
        float[] output;

        public float[] Input
        {
            get
            {
                return input;
            }

            set
            {
                input = value;
            }
        }

        public float[] Output
        {
            get
            {
                return output;
            }

            set
            {
                output = value;
            }
        }
    }

}