using Tensorflow;
using static Tensorflow.Binding;

namespace gaussian_splatting_csharp.AI;

public class DeformationNetwork
{
    private readonly Graph _graph;
    private Session _session;

    public DeformationNetwork()
    {
        tf.compat.v1.disable_eager_execution(); //Remove eager execution
        _graph = tf.Graph().as_default();
        _session = tf.Session(_graph);
    }

    public Tensor Encode(Tensor position, float timestamp)
    {
        var encodedFeature = tf.multiply(position, timestamp);
        return encodedFeature;
    }

    public Tensor Decode(Tensor encodedFeature)
    {
        using (var scope = tf.name_scope("Decode"))
        {
            var decodedFeature = tf.divide(encodedFeature, tf.constant(2.0f));
            return decodedFeature;
        }
    }
}