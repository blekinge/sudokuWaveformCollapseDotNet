namespace waveformCollapse;

public class Entanglement
{
    private readonly string _name;
    private readonly IEnumerable<Particle> _particles;
    private Action<IEnumerable<(Particle, object)>>? _callback;

    public void EliminateValueFromEntanglement(object value)
    {
        var assignments = _particles
            .Select(p => p.RestrictValue(value))
            .Where(a => a is not null)
            .Select(a => a!.Value)
            .ToHashSet();

        var uniqueValues = _particles
            .Where(p => p.Value is null)
            .SelectMany(p => p.PossibleValues)
            .Where(v => v is not null)
            .GroupBy(p => p)
            .Where(g => g.Count() == 1)
            .Select(g => g.Key);

        foreach (var uniqueValue in uniqueValues)
        {
            assignments.UnionWith(
                _particles
                    .Where(p => p.Value is null)
                    .Where(p => p.PossibleValues.Contains(uniqueValue))
                    .Select(p => (p, uniqueValue))
                    .ToHashSet()!);
        }

        _callback?.Invoke(assignments);
    }

    public Entanglement(string name, params Particle[] particles)
    {
        _name = name;
        foreach (var particle in particles)
        {
            particle.Register(this);
        }

        _particles = [..particles.AsEnumerable().Distinct()];
    }

    public override string ToString()
    {
        return $"{_name}: {string.Join(',', _particles.Select(p => p.Name+"="+p.Value))}";
    }

    public void NewAssignmentsCallback(Action<IEnumerable<(Particle, object)>>? callback)
    {
        _callback = callback;
    }
}