namespace waveformCollapse;

public class Solver 
{
    private Situation Situation { get; }
    private readonly Queue<(Particle, object)> _workQueue = new();

    public Solver(Situation situation)
    {
        Situation = situation;
 
        //reset possible values as we have to start fresh here
        var situation1 = situation;
        situation.AllParticles.ToList()
            .ForEach(p =>
            {
                p.possibleValues = [..situation1.AllPossibleValues];
                p.Value = null;
                p.Derived = null;
            });
        //Add this solver for all entanglements
        situation.AllEntanglements.ToList().ForEach(e => e.Register(this));
    }


    public (Particle, object)? SolveNextStep()
    {
        if (Situation.IsSolved()) return null;
        
        var (particle, value) = GetNextAssignment() ?? default;
        if (particle == null) return null;
        
        particle.Value = value;
        Situation.LastSet = particle;

        return (particle, value);
    }

    private (Particle, object)? GetNextAssignment()
    {
        if (_workQueue.Count <= 0) return null;

        (Particle, object) assignment;
        do
        {
            assignment = _workQueue.Dequeue();
        } while (assignment.Item1 is { Value: not null });

        return assignment;
    }

    public void EnqueueNewAssignments(IEnumerable<(Particle, object)> newAssignments)
    {
        newAssignments.ToList().ForEach(a => _workQueue.Enqueue(a));
    }
}