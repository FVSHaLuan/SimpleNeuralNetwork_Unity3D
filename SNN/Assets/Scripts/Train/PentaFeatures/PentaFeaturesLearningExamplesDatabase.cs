#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using SNN.Core;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace SNN.Train
{
    [CreateAssetMenu]
    public class PentaFeaturesLearningExamplesDatabase : LearningExamplesDatabase
    {
        const int FeaturesCount = 5;
        const int FeatureSize = 2;

        [SerializeField, HideInInspector]
        List<LearningExample> learningExamples;

        [Header("PentaFeaturesLearningExamplesDatabase")]
        [SerializeField]
        int numberOfExamples = 1000;

        public override LearningExample[] LearningExamples
        {
            get
            {
                return learningExamples.ToArray();
            }
        }

        public override int LearningExamplesCount
        {
            get
            {
                return learningExamples.Count;
            }
        }

        [ContextMenu("Generate LearningExamples")]
        public void GenerateLearningExamples()
        {
            Undo.RecordObject(this, "GenerateLearningExamples");

            learningExamples = new List<LearningExample>();

            for (int i = 0; i < numberOfExamples; i++)
            {
                learningExamples.Add(GenerateLearningExample());
            }

            Debug.Log("Finished GenerateLearningExamples");
        }

        LearningExample GenerateLearningExample()
        {
            return GenerateLearningExample(GetRandomeFeatures());
        }

        LearningExample GenerateLearningExample(float[] features)
        {
            // input
            List<float> learningInput = new List<float>();
            for (int featureIndex = 0; featureIndex < features.Length; featureIndex++)
            {
                for (int i = 0; i < FeatureSize; i++)
                {
                    learningInput.Add(GetInputNode(features[featureIndex]));
                }
            }

            // output
            List<float> learningOutput = new List<float>();
            learningOutput.AddRange(features);

            ///
            return new LearningExample(FeaturesCount * FeatureSize, FeaturesCount)
            {
                Input = learningInput.ToArray(),
                Output = learningOutput.ToArray()
            };
        }

        float GetInputNode(float featureRepresentation)
        {
            float nodeValue = UnityEngine.Random.Range(0, 0.5f);

            if (featureRepresentation == 1)
            {
                nodeValue += 0.5f;
            }

            return nodeValue;
        }

        float[] GetRandomeFeatures()
        {
            float[] features = new float[FeaturesCount];
            for (int i = 0; i < FeaturesCount; i++)
            {
                features[i] = UnityEngine.Random.Range(0, 100) > 50 ? 1 : 0;
            }
            return features;
        }

        public void Reset()
        {
            GenerateLearningExamples();
        }

        public override LearningExample GetLearningExample(int index)
        {
            return learningExamples[index];
        }
    }

}
#endif