namespace waveformCollapse;


public class Situation(
    IEnumerable<Particle> allParticles,
    IEnumerable<Entanglement> allEntanglements,
    List<object> allPossibleValues)
{
    
    public IEnumerable<Particle> AllParticles = allParticles;

    public IEnumerable<Entanglement> AllEntanglements = allEntanglements;

    public List<object> AllPossibleValues = allPossibleValues;
    

    public Particle? GetNamedParticle(string name)
    {
        return AllParticles.FirstOrDefault(p => p.Name == name);
    }
    public Particle? LastSet { get; set; }
    
    public bool IsSolved()
    {
        return AllParticles.AsEnumerable().All(particle => particle.Value is not null);
    }

    public override string ToString()
    {
        return
            $"{nameof(AllParticles)}: {AllParticles}, {nameof(AllEntanglements)}: {AllEntanglements}, {nameof(AllPossibleValues)}: {AllPossibleValues}, {nameof(LastSet)}: {LastSet}";
    }
}