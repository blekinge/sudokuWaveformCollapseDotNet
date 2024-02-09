using System.Collections.ObjectModel;

namespace waveformCollapse;

public class Situation(
    ICollection<Particle?> allParticles,
    ICollection<Entanglement> allEntanglements,
    ICollection<object> allPossibleValues)
{

    public ICollection<Particle?> AllParticles = allParticles;

    public ICollection<Entanglement> AllEntanglements = allEntanglements;

    public HashSet<object> AllPossibleValues = allPossibleValues.ToHashSet();
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