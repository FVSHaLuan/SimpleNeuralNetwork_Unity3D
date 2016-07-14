using SNN.Core;

namespace SNN.Train
{
    public interface ITrainableNeuralNet : INeuralNet
    {
        float[] Weights { get; set; }
        float[] Biases { get; set; }
    }

}