namespace waveformCollapse;


public class Situation(
    ICollection<Particle> allParticles,
    ICollection<Entanglement> allEntanglements,
    ICollection<object> allPossibleValues)
{
    
    public ICollection<Particle> AllParticles = allParticles;

    public ICollection<Entanglement> AllEntanglements = allEntanglements;

    public HashSet<object> AllPossibleValues = allPossibleValues.ToHashSet();

    public Particle? GetNamedParticle(string name)
    {
        return AllParticles.FirstOrDefault(p => p.name == name);
    }
    public Particle? lastSet { get; set; }
    
    public bool IsSolved()
    {
        return AllParticles.AsEnumerable().All(particle => particle.value is not null);
    }

    public override string ToString()
    {
        return
            $"{nameof(AllParticles)}: {AllParticles}, {nameof(AllEntanglements)}: {AllEntanglements}, {nameof(AllPossibleValues)}: {AllPossibleValues}, {nameof(lastSet)}: {lastSet}";
    }
}