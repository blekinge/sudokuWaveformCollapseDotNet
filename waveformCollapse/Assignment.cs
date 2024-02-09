namespace waveformCollapse;

public class Assignment(Particle particle, object value)
{
    public readonly Particle Particle = particle;
    public readonly object Value = value;

    public override string ToString()
    {
        return $"{nameof(Particle)}: {Particle}, {nameof(Value)}: {Value}";
    }
}