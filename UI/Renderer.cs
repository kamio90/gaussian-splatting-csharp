using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using QuickFont;
using QuickFont.Configuration;

namespace gaussian_splatting_csharp.UI;

public class Renderer
{
    private readonly QFont _font;

    public Renderer()
    {
        var fontBuilderConfig = new QFontBuilderConfiguration(true);
        _font = new QFont("arial.tft", 16, fontBuilderConfig);
    }

    public void Render(List<Gaussian.Gaussian> guassians, Matrix4 viewMatrix)
    {
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadMatrix(ref viewMatrix);
        GL.PointSize(10.0f); //Setup point size
        foreach (var guassian in guassians) RenderGaussian(guassian);
        RenderText("Hello, OpenGL!", 10, 10);
    }

    private void RenderGaussian(Gaussian.Gaussian gaussian)
    {
        GL.Color3(1.0f, 0.0f, 0.0f); // points in red
        GL.Begin(PrimitiveType.Points);
        GL.Vertex3(gaussian.Mean[0], gaussian.Mean[1], gaussian.Mean[2]);
        GL.EndTransformFeedback();
    }

    private void RenderText(string text, int x, int y)
    {
        GL.PushMatrix();
        GL.LoadIdentity();
        GL.MatrixMode(MatrixMode.Projection);
        GL.PushMatrix();
        GL.LoadIdentity();
        GL.Ortho(0, 800, 600, 0, -1, 1);

        _font.Print(text, new Vector2(x, y));

        GL.PopMatrix();
        GL.MatrixMode(MatrixMode.Modelview);
        GL.PopMatrix();
    }
}