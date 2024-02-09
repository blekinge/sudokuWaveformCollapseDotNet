namespace waveformCollapse;

public class Solver
{
    private Situation situation { get; }
    private readonly Queue<Assignment?> _workQueue = new();

    public Solver(Situation situation)
    {
        this.situation = situation;
        //reset possible values as we have to start fresh here
        situation.AllParticles.ToList()
            .ForEach(p => p.PossibleValues = [..situation.AllPossibleValues]);
        var loadInitialWork = LoadInitialWork();
        loadInitialWork.ForEach(a => _workQueue.Enqueue(a));
    }


    // public Assignment? SolveNextStep()
    // {
    //     if (!situation.IsSolved() && _workQueue.Count > 0)
    //     {
    //         var nextAssignment = GetNextAssignment();
    //         var deriveConsequencesOfAssignment = DeriveConsequencesOfAssignment(nextAssignment);
    //         EnqueueNewAssignments(deriveConsequencesOfAssignment);
    //         return nextAssignment;
    //     }
    //     return null;
    // }

    Assignment? GetNextAssignment()
    {
        Assignment? assignment;
        do
        {
            assignment = _workQueue.Dequeue();
        } while (assignment?.Particle is { Value: not null, Derived: true });

        return assignment;
    }

    public void EnqueueNewAssignments(List<Assignment?> newAssignments)
    {
        newAssignments.ForEach(a => _workQueue.Enqueue(a));
    }

    private List<Assignment?> LoadInitialWork()
    {
        return situation.AllParticles
            .Where(p => p.Value is not null)
            .Where(p => p.Derived is null or false)
            .Select(p => new Assignment(particle: p, value: p.Value!))
            .ToList();
    }
}