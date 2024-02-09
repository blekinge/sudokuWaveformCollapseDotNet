namespace waveformCollapse;

public class Particle(ICollection<object> possibleValues)
{
    public object? Value
    {
        get => _value;
        set
        {
            _value = value;
            if (value is null) return;
            foreach (var entanglement in _entanglements)
            {
                entanglement.ValueAssigned(value);
            }
        }
    }

    public bool? Derived { get; set; }

    private readonly List<Entanglement> _entanglements = [];
    private object? _value;

    public void Register(Entanglement entanglement)
    {
        _entanglements.Add(entanglement);
    }

    public ICollection<object> PossibleValues { get; set; } = possibleValues;

    public Assignment? RestrictValue(object value)
    {
        if (Value != null) return null;
        if (!PossibleValues.Remove(value)) return null;
        if (PossibleValues is not { Count: 1 }) return null;
        return new Assignment(this, PossibleValues.First());
    }

    public override string ToString()
    {
        return
            $"{nameof(Value)}: {Value}, {nameof(Derived)}: {Derived}, {nameof(PossibleValues)}: {PossibleValues}";
    }
}