
namespace SNN.Core
{
    public interface INeuralNet
    {
        int InputSize { get; }
        int OutputSize { get; }        
        float[] Compute(float[] input);
    }

}