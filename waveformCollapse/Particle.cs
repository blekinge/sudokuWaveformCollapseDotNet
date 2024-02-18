using System.ComponentModel;

namespace waveformCollapse;

public abstract class Particle(string name) : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private readonly List<Entanglement> _entanglements = [];

    public void Register(Entanglement entanglement)
    {
        _entanglements.Add(entanglement);
    }

    private object? _value;

    public object? Value
    {
        get => _value;
        set
        {
            if (value is null) return;
            _value = value;
            foreach (var entanglement in _entanglements)
            {
                entanglement.ValueAssigned(value);
            }

            OnPropertyChanged(nameof(Value));
        }
    }

    public bool? Derived { get; set; }

    public string Name { get; } = name;

    private List<object?> _possibleValues = [];

    public List<object?> possibleValues
    {
        get => _possibleValues;
        set
        {
            if (Equals(value, _possibleValues)) return;
            _possibleValues = value;
            OnPropertyChanged(nameof(possibleValues));
        }
    }

    public override string ToString()
    {
        return
            $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}, {nameof(possibleValues)}: {String.Join(",", possibleValues)}";
    }


    public (Particle, object)? RestrictValue(object restriction)
    {
        if (Value != null) return null;

        var indexOfRestrictedValue = possibleValues.FindIndex(v => v?.Equals(restriction) ?? false);
        if (indexOfRestrictedValue == -1)
        {
            return null;
        }

        possibleValues[indexOfRestrictedValue] = default;
        OnPropertyChanged(nameof(possibleValues));

        if (possibleValues.Count(v => v is not null) != 1) return null;

        Console.WriteLine(this);
        return (this, possibleValues.First(v => v is not null))!;
    }
}