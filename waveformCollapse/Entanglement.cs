namespace waveformCollapse;

public class Entanglement
{
    private string Name { get; }
    private readonly IEnumerable<object> _allAllowedValues;
    private IEnumerable<Particle> Particles { get; }
    private Action<IEnumerable<(Particle, object)>>? _callback;
    private Dictionary<object, object?> Restrictions { get; }

    public void EliminateValueFromEntanglement(object value)
    {
        Restrictions[value] = null;
        var assignments = Particles
                         .Select(p => p.RestrictValue(value))
                         .Where(a => a is not null)
                         .Select(a => a!.Value)
                         .ToHashSet();

        var uniqueValues = Particles
                          .Where(p => p.Value is null)
                          .SelectMany(p => p.PossibleValues)
                          .Where(v => v is not null)
                          .GroupBy(p => p)
                          .Where(g => g.Count() == 1)
                          .Select(g => g.Key);

        foreach (var uniqueValue in uniqueValues)
        {
            assignments.UnionWith(
                Particles
                   .Where(p => p.Value is null)
                   .Where(p => p.PossibleValues.Contains(uniqueValue))
                   .Select(p => (p, uniqueValue))
                   .ToHashSet()!);
        }

        _callback?.Invoke(assignments);
    }

    public Entanglement(string name, List<object> allAllowedValues, params Particle[] particles)
    {
        Name = name;
        _allAllowedValues = allAllowedValues;
        Restrictions = allAllowedValues.ToDictionary(p => p, p => p);
        foreach (var particle in particles)
        {
            particle.Register(this);
        }

        Particles =
        [
            ..particles.AsEnumerable()
                       .Distinct()
        ];
    }

    public override string ToString()
    {
        return $"{Name}: {string.Join(',', Particles.Select(p => p.Name + "=" + p.Value))}";
    }

    public void NewAssignmentsCallback(Action<IEnumerable<(Particle, object)>>? callback)
    {
        _callback = callback;
    }

    public List<object?> AllowedValues()
    {
        Dictionary<object, object?> localValues = _allAllowedValues.ToDictionary(v => v, v => v)!;
        Particles.Select(p => p.Value)
                  .Where(p => p is not null)
                  .Cast<object>()
                  .ForEach(v => localValues[v] = null);
        return localValues.Values
                          .ToList();
    }

    public void Recalculate(object? valueUnset)
    {
    }
}