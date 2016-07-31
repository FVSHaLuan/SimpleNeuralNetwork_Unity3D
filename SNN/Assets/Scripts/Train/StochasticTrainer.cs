#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using SNN.Core;
using UnityEditor;

namespace SNN.Train
{
    [CreateAssetMenu]
    public class StochasticTrainer : ScriptableObject
    {
        [SerializeField]
        NeuralNet neuralNet;
        [SerializeField]
        LearningExamplesDatabase trainingSet;
        [SerializeField]
        LearningExamplesDatabase validationSet;

        [Header("Hype-parameters")]
        [SerializeField]
        float learningRate = 0.15f;
        [SerializeField]
        NeuralNet.RegularizationMethodKind regularizationMethod;
        [SerializeField]
        float regularizationRate = 0.01f;

        [Header("Stochastic")]
        [SerializeField]
        int minibatchSize;
        [SerializeField]
        int numberOfEpoches;

        [Header("Misc.")]
        [SerializeField]
        bool logValidationCostAfterEachEpoch = true;

        float lastComputedCost = float.NaN;

        [ContextMenu("Train")]
        public void Train()
        {
            Undo.RecordObject(neuralNet, "Stochastic training");

            lastComputedCost = float.NaN;

            //
            neuralNet.RegularizationMethod = regularizationMethod;
            neuralNet.RegularizationRate = regularizationRate;

            //
            trainingSet.CreateAnEpoch(minibatchSize);
            for (int epoch = 0; epoch < numberOfEpoches; epoch++)
            {
                bool cancel = !TrainForAnEpoch(epoch);

                if (logValidationCostAfterEachEpoch)
                {
                    LogValidationCost();
                }

                if (cancel)
                {
                    break;
                }

                trainingSet.ReFillMinibatches();
            }

            //
            EditorUtility.ClearProgressBar();
        }

        bool TrainForAnEpoch(int epochId)
        {
            for (int i = 0; i < trainingSet.MinibatchesCount; i++)
            {
                neuralNet.Learn(trainingSet.GetLearningExamples(i), learningRate);
                if (EditorUtility.DisplayCancelableProgressBar("Stochastic training...", string.Format("Epoch: {0:00000} - Minibatch: {1:00000} - Last cost: {2}", epochId, i, lastComputedCost), (float)epochId / (float)numberOfEpoches))
                {
                    return false;
                }
            };

            return true;
        }

        [ContextMenu("LogValidationCost")]
        void LogValidationCost()
        {
            lastComputedCost = CostHelper.ComputeCost(neuralNet, validationSet.LearningExamples);
            Debug.LogFormat("NeuralNet {0}, cost: {1}", neuralNet.name, lastComputedCost);
        }
    }

}
#endif