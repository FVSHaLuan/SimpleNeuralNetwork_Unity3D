using UnityEngine;
using System.Collections;
using SNN.Core;
using System.Collections.Generic;
using System;

namespace SNN.Train
{
    [CreateAssetMenu]
    public class RawLearningExamplesDatabase : LearningExamplesDatabase
    {
        [Header("RawLearningExamplesDatabase")]
        [SerializeField]
        LearningExample[] learningExamples;

        public override int LearningExamplesCount
        {
            get
            {
                return learningExamples.Length;
            }
        }

        public override LearningExample[] LearningExamples
        {
            get
            {
                return learningExamples;
            }           
        }

        public override LearningExample GetLearningExample(int index)
        {
            return learningExamples[index];
        }
    }
}
