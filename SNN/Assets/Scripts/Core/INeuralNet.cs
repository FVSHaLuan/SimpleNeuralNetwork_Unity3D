
namespace SNN.Core
{
    public interface INeuralNet
    {
        int InputSize { get; }
        float[] Compute(float[] input);
    }

}