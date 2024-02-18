using System.Collections.Specialized;
using System.ComponentModel;

namespace waveformCollapse;

public abstract class Particle(string name) : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        Console.WriteLine($"Particle {Name} changed on {propertyName}");
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

            if (value is null)
            {
                //TODO proper restoration of state when value becomes unset
                var allowedValuesToRestore = RestorePossibleValues();
                if (allowedValuesToRestore != null) PossibleValues = allowedValuesToRestore;
                _entanglements.ForEach(e => e.Recalculate(_value));
            }
            else
            {
                _entanglements.ForEach(e => e.EliminateValueFromEntanglement(value));
            }
            _value = value;

            OnPropertyChanged(nameof(Value));
        }
    }

    private List<object?>? RestorePossibleValues()
    {
        List<object?>? allowedValuesToRestore = null;
        foreach (var allow in _entanglements.Select(entanglement => entanglement.AllowedValues()))
        {
            if (allowedValuesToRestore == null)
            {
                allowedValuesToRestore = [..allow];
            }
            else
            {
                foreach (var (item, index) in allow.WithIndex())
                {
                    if (item == null)
                    {
                        allowedValuesToRestore[index] = null;
                    }
                }
            }
        }

        return allowedValuesToRestore;
    }

    public bool? Derived { get; set; }

    public string Name { get; } = name;

    public List<object?> PossibleValues { get; set; } = [];

    public override string ToString()
    {
        return
            $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}, {nameof(PossibleValues)}: {String.Join(",", PossibleValues)}";
    }


    public (Particle, object)? RestrictValue(object restriction)
    {
        if (Value != null) return null;

        var indexOfRestrictedValue = PossibleValues?.FindIndex(v => v?.Equals(restriction) ?? false) ?? -1;
        if (indexOfRestrictedValue is -1)
        {
            return null;
        }

        PossibleValues![indexOfRestrictedValue] = default;
        OnPropertyChanged(nameof(PossibleValues));


        if (PossibleValues?.Count(v => v is not null) != 1) return null;

        Console.WriteLine(this);
        return (this, PossibleValues.First(v => v is not null))!;
    }
}