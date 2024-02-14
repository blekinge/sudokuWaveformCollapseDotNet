namespace waveformCollapse;

public abstract class Particle(string name, ICollection<object> possibleValues)
{
    private readonly List<Entanglement> _entanglements = [];

    public void Register(Entanglement entanglement)
    {
        _entanglements.Add(entanglement);
    }

    private object? _value;

    public object? value
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

    public bool? derived { get; set; }

    public string name { get; } = name;
    public ICollection<object> possibleValues { get; set; } = possibleValues;

    public override string ToString()
    {
        return
            $"{nameof(value)}: {value}, {nameof(derived)}: {derived}, {nameof(possibleValues)}: {possibleValues}";
    }


    public (Particle, object)? RestrictValue(object value)
    {
        if (this.value != null) return null;
        if (!possibleValues.Remove(value)) return null;
        if (possibleValues is not { Count: 1 }) return null;
        return (this, possibleValues.First());
    }
}