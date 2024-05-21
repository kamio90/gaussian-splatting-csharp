using MathNet.Numerics.LinearAlgebra;

namespace gaussian_splatting_csharp.Gaussian;

public class Gaussian
{
    public Gaussian(Vector<float> mean, Matrix<float> covariance)
    {
        Mean = mean;
        Covariance = covariance;
    }

    public Vector<float> Mean { get; set; }
    public Matrix<float> Covariance { get; set; }

    public void ApplyTransformation(Matrix<float> transformaiton)
    {
        Covariance = transformaiton * Covariance * transformaiton.Transpose();
    }
}