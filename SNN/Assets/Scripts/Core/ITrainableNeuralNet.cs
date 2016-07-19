using SNN.Core;

namespace SNN.Core
{
    public interface ITrainableNeuralNet : INeuralNet, INeuralNetAccessor
    {
        void Learn(LearningExample[] learningExamples, float learningRate);
    }

}