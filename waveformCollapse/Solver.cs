namespace waveformCollapse;

public class Solver
{
    private Situation Situation { get; }

    private readonly Queue<(Particle, object)> _workQueue = new();

    public Solver(Situation situation)
    {
        Situation = situation;

        //reset possible values as we have to start fresh here
        situation.AllParticles
                 .ForEach(p =>
                  {
                      List<object?> objPossibleValues = [..situation.AllPossibleValues];
                      p.PossibleValues = objPossibleValues;
                      p.Value = null;
                      p.Derived = null;
                  });
        //Add this solver for all entanglements
        situation.AllEntanglements
                 .ForEach(e => e.NewAssignmentsCallback(EnqueueNewAssignments));
    }


    public (Particle, object)? SolveNextStep()
    {
        if (Situation.IsSolved()) return null;

        var (particle, value) = GetNextAssignment() ?? default;
        if (particle == null) return null;

        if (!particle.PossibleValues.Contains(value)) return SolveNextStep();

        if (particle.Value is not null) return SolveNextStep();;
        
        particle.Value = value;
        Situation.LastSet = particle;
        return (particle, value);
    }

    private (Particle, object)? GetNextAssignment()
    {
        if (_workQueue.TryDequeue(out var assignment)) return assignment;
        return null;
    }

    private void EnqueueNewAssignments(IEnumerable<(Particle, object)> newAssignments)
    {
        newAssignments.ForEach(a => _workQueue.Enqueue(a));
    }
}