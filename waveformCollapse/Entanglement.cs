namespace waveformCollapse;

public class Entanglement
{
    public readonly ICollection<Particle> Particles;
    private Solver? _solver;

    public void ValueAssigned(object value)
    {
        var assignments = Particles
            .Select(p => p.RestrictValue(value))
            .Where(a => a is not null)
            .Select(a => a!.Value)
            .ToHashSet();

        var uniqueValues = Particles
            .Where(p => p.value is null)
            .SelectMany(p => p.possibleValues)
            .GroupBy(p => p)
            .Where(g => g.Count() == 1)
            .Select(g => g.Key);

        foreach (var uniqueValue in uniqueValues)
        {
            assignments.UnionWith(
                Particles
                    .Where(p => p.value is null)
                    .Where(p => p.possibleValues.Contains(uniqueValue))
                    .Select(p => (p, uniqueValue))
                    .ToHashSet());
        }

        _solver?.EnqueueNewAssignments(assignments);
    }

    public Entanglement(params Particle[] particles)
    {
        foreach (var particle in particles)
        {
            particle.Register(this);
        }

        Particles = [..particles.AsEnumerable()];
    }

    public void Register(Solver? solver)
    {
        _solver = solver;
    }

    public override string ToString()
    {
        return $"{nameof(Particles)}: {Particles}";
    }
}