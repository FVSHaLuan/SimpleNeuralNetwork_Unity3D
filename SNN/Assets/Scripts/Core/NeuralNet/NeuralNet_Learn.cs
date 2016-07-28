using UnityEngine;
using System.Collections;
using System;

namespace SNN.Core
{
    public partial class NeuralNet
    {
        [NonSerialized]
        BiasAccessor biasAccessor = null;
        [NonSerialized]
        WeightAccessor weightAccessor = null;

        [NonSerialized]
        NeuralNetAccessor costFunctionGradient;
        [NonSerialized]
        NeuralNetAccessor backPropagationOutput;

        [NonSerialized]
        bool initializedLearning = false;

        #region ITrainableNeuralNet
        public BiasAccessor BiasAccessor
        {
            get
            {
                return neuralNetAccessor.BiasAccessor;
            }
        }

        public WeightAccessor WeightAccessor
        {
            get
            {

                return neuralNetAccessor.WeightAccessor;
            }
        }

        public void Learn(LearningExample[] learningExample, float learningRate)
        {
            if (!initializedLearning)
            {
                InitializeLearning();
            }

            costFunctionGradient.SetAllBiasesAndWeightsTo(0);

            for (int i = 0; i < learningExample.Length; i++)
            {
                backPropagation.BackPropagate(learningExample[i], backPropagationOutput);
                costFunctionGradient.AddAllBiasesAndWeightsWith(backPropagationOutput);
            }

            costFunctionGradient.MultiplyAllBiasesAndWeightsWith(-1.0f / learningExample.Length * learningRate);
            neuralNetAccessor.AddAllBiasesAndWeightsWith(costFunctionGradient);
        }
        #endregion

        void InitializeLearning()
        {
            costFunctionGradient = new NeuralNetAccessor(neuralNetAccessor.InitializedParameters);
            backPropagationOutput = new NeuralNetAccessor(neuralNetAccessor.InitializedParameters);
            InitializeBackPropagation();
            initializedLearning = true;
        }

    }

}