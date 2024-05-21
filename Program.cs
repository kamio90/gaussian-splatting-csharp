using gaussian_splatting_csharp.AI;
using gaussian_splatting_csharp.Gaussian;
using gaussian_splatting_csharp.UI;
using MathNet.Numerics.LinearAlgebra;
using Tensorflow;
using static Tensorflow.Binding;


internal class Program
{
    private static void Main(string[] args)
    {
        var mean = Vector<float>.Build.DenseOfArray(new float[] { 0, 0, 0 });
        var covariance = Matrix<float>.Build.DenseIdentity(3);
        var gaussians = new List<Gaussian>
        {
            new(Vector<float>.Build.DenseOfArray(new float[] { 0, 0, 0 }), Matrix<float>.Build.DenseIdentity(3)),
            new(Vector<float>.Build.DenseOfArray(new[] { .5f, .5f, 0 }), Matrix<float>.Build.DenseIdentity(3)),
            new(Vector<float>.Build.DenseOfArray(new[] { -.5f, -.5f, 0 }), Matrix<float>.Build.DenseIdentity(3)),
            new(Vector<float>.Build.DenseOfArray(new[] { .5f, -.5f, 0 }), Matrix<float>.Build.DenseIdentity(3)),
            new(Vector<float>.Build.DenseOfArray(new[] { -.5f, .5f, 0 }), Matrix<float>.Build.DenseIdentity(3)),
            new(Vector<float>.Build.DenseOfArray(new[] { -1.5f, 1.5f, 0 }), Matrix<float>.Build.DenseIdentity(3))
        };


        var deformationNetwork = new DeformationNetwork();
        var positionTensor = tf.constant(new float[] { 0, 0, 0 }, shape: new Shape(3));
        var encoded = deformationNetwork.Encode(positionTensor, 1.0f);
        var decoded = deformationNetwork.Decode(encoded);


        using (var game = new Game(800, 600, "Gussian Splatting"))
        {
            game.Gaussians = gaussians;
            game.Run();
        }

        Console.WriteLine("Rendering complete.");
    }
}