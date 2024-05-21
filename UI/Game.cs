using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Vector3 = OpenTK.Mathematics.Vector3;

namespace gaussian_splatting_csharp.UI;

public class Game : GameWindow
{
    private readonly Renderer _renderer;

    public Game(int width, int height, string title) : base(GameWindowSettings.Default,
        new NativeWindowSettings { Size = new Vector2i(width, height), Title = title })
    {
        _renderer = new Renderer();
    }

    public List<Gaussian.Gaussian> Gaussians { get; set; }


    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(Color4.CornflowerBlue);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        var viewMatrix = Matrix4.LookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.UnitY); // Camera setup
        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadIdentity();
        GL.Ortho(-1, 1, -1, 1, 0.1, 100.0);

        _renderer.Render(Gaussians, viewMatrix);

        SwapBuffers();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, Size.X, Size.Y);
    }
}