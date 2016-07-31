using UnityEngine;
using System.Collections;
using SNN.Core;
using System.Collections.Generic;
using System;

namespace SNN.Train
{
    public abstract class LearningExamplesDatabase : ScriptableObject
    {
        [NonSerialized]
        List<LearningExample[]> minibatches;

        [Header("CreateAndEpochBenchMark")]
        [SerializeField]
        int testMinibatchSize;

        [NonSerialized]
        List<int> allIndices;
        [NonSerialized]
        int allIndicesCount;

        public abstract LearningExample[] LearningExamples { get; }

        public int MinibatchesCount
        {
            get
            {
                if (minibatches == null)
                {
                    return 0;
                }
                return minibatches.Count;
            }
        }

        public LearningExample[] GetLearningExamples(int minibatchIndex)
        {
            return minibatches[minibatchIndex];
        }

        public abstract int LearningExamplesCount { get; }

        public abstract LearningExample GetLearningExample(int index);

        public void CreateAnEpoch(int minibatchSize)
        {
            RefreshAllIndices();

            minibatches = new List<LearningExample[]>();
            while (allIndicesCount > 0)
            {
                minibatches.Add(GetMinibatch(minibatchSize));
            }
        }

        public void ReFillMinibatches()
        {
            RefreshAllIndices();

            foreach (var minibatch in minibatches)
            {
                int minibatchLength = minibatch.Length;
                for (int i = 0; i < minibatchLength; i++)
                {
                    int randomIndex = TakeRandomIndex();
                    minibatch[i] = GetLearningExample(randomIndex);
                }
            }
        }

        void RefreshAllIndices()
        {
            int numberOfIndices = LearningExamplesCount;

            int[] allIndicesArray = new int[numberOfIndices];
            for (int i = 0; i < numberOfIndices; i++)
            {
                allIndicesArray[i] = i;
            }
            allIndicesCount = numberOfIndices;
            allIndices = new List<int>(allIndicesArray);
        }

        LearningExample[] GetMinibatch(int size)
        {
            LearningExample[] minibatch;

            int maxSize = Mathf.Min(size, allIndices.Count);

            //
            minibatch = new LearningExample[maxSize];
            for (int i = 0; i < maxSize; i++)
            {
                minibatch[i] = GetLearningExample(TakeRandomIndex());
            }

            //
            return minibatch;
        }

        int TakeRandomIndex()
        {
            int indexInList = UnityEngine.Random.Range(0, allIndices.Count);
            int randomIndex = allIndices[indexInList];
            allIndices.RemoveAt(indexInList);
            allIndicesCount--;
            return randomIndex;
        }

        [ContextMenu("CreateAndEpochBenchMark")]
        public void CreateAndEpochBenchMark()
        {
            System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
            st.Start();
            CreateAnEpoch(testMinibatchSize);
            st.Stop();
            Debug.LogFormat("CreateAndEpochBenchMark: {0} milliseconds", st.ElapsedMilliseconds);
        }
    }
}
