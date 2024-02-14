namespace waveformCollapse;

public class Solver 
{
    private Situation situation { get; }
    private readonly Queue<(Particle, object)> _workQueue = new();

    public Solver(Situation situation)
    {
        this.situation = situation;
 
        Console.WriteLine();
        //reset possible values as we have to start fresh here
        situation.AllParticles.ToList()
            .ForEach(p =>
            {
                p.possibleValues = [..situation.AllPossibleValues];
                p.value = null;
                p.derived = null;
            });
        //Add this solver for all entanglements
        situation.AllEntanglements.ToList().ForEach(e => e.Register(this));
    }


    public (Particle, object)? SolveNextStep()
    {
        if (situation.IsSolved()) return null;
        
        var (particle, value) = GetNextAssignment() ?? default;
        if (particle == null) return null;
        
        particle.value = value;
        situation.lastSet = particle;

        return (particle, value);
    }

    private (Particle, object)? GetNextAssignment()
    {
        if (_workQueue.Count <= 0) return null;

        (Particle, object) assignment;
        do
        {
            assignment = _workQueue.Dequeue();
        } while (assignment.Item1 is { value: not null });

        return assignment;
    }

    public void EnqueueNewAssignments(ICollection<(Particle, object)> newAssignments)
    {
        newAssignments.ToList().ForEach(a => _workQueue.Enqueue(a));
    }
}