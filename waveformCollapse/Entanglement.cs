namespace waveformCollapse;

public class Entanglement
{
    public readonly ICollection<Particle> Particles;

    public Entanglement(params Particle[] particles)
    {
        foreach (var particle in particles)
        {
            particle.Register(this);
        }

        Particles = [..particles.AsEnumerable()];
    }

    public ICollection<Assignment> ValueAssigned(object value)
    {
        var assignments = Particles
            .Select(p => p.RestrictValue(value))
            .Where(a => a is not null)
            .Select(a => a!)
            .ToHashSet();

        var uniqueValues = Particles
            .Where(p => p.Value is null)
            .SelectMany(p => p.PossibleValues)
            .GroupBy(p => p)
            .Where(g => g.Count() == 1)
            .Select(g => g.Key);

        foreach (var uniqueValue in uniqueValues)
        {
            assignments.UnionWith(
                Particles
                    .Where(p => p.Value is null)
                    .Where(p => p.PossibleValues.Contains(uniqueValue))
                    .Select(p => new Assignment(p, uniqueValue))
                    .ToHashSet());
        }

        return assignments;
    }

    public override string ToString()
    {
        return $"{nameof(Particles)}: {Particles}";
    }
}