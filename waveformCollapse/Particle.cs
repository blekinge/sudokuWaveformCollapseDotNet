using System.Collections.Specialized;
using System.ComponentModel;

namespace waveformCollapse;

public abstract class Particle(string name) : INotifyPropertyChanged, INotifyCollectionChanged
{
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    protected virtual void OnCollectionChanged(string collectionName, int index, object? value)
    {
        Console.WriteLine($"Particle {Name} changed on collection {collectionName} with {index} and {value}");
        CollectionChanged?.Invoke(this,
                                  new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace,
                                                                       value,
                                                                       index));
    }

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
            if (value is null) return;
            _value = value;
            OnPropertyChanged(nameof(Value));
            _entanglements.ToList()
                          .ForEach(e => e.EliminateValueFromEntanglement(value));
        }
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
        OnCollectionChanged(nameof(PossibleValues), indexOfRestrictedValue, restriction);
        OnPropertyChanged(nameof(PossibleValues));


        if (PossibleValues?.Count(v => v is not null) != 1) return null;

        Console.WriteLine(this);
        return (this, PossibleValues.First(v => v is not null))!;
    }
}