namespace waveformCollapse;

public class Entanglement
{
    public string Name { get; }
    public readonly IEnumerable<Particle> Particles;
    private Solver? _solver;

    public void ValueAssigned(object value)
    {
        var assignments = Particles
            .Select(p => p.RestrictValue(value))
            .Where(a => a is not null)
            .Select(a => a!.Value)
            .ToHashSet();

        var uniqueValues = Particles
            .Where(p => p.Value is null)
            .SelectMany(p => p.possibleValues)
            .Where(v => v is not null)
            .GroupBy(p => p)
            .Where(g => g.Count() == 1)
            .Select(g => g.Key);

        foreach (var uniqueValue in uniqueValues)
        {
            assignments.UnionWith(
                Particles
                    .Where(p => p.Value is null)
                    .Where(p => p.possibleValues.Contains(uniqueValue))
                    .Select(p => (p, uniqueValue))
                    .ToHashSet()!);
        }

        _solver?.EnqueueNewAssignments(assignments);
    }

    public Entanglement(string name, params Particle[] particles)
    {
        Name = name;
        foreach (var particle in particles)
        {
            particle.Register(this);
        }

        Particles = [..particles.AsEnumerable().Distinct()];
    }

    public void Register(Solver? solver)
    {
        _solver = solver;
    }

    public override string ToString()
    {
        return $"{Name}: {String.Join(',', Particles.Select(p => p.Name+"="+p.Value))}";
    }
}