


public abstract class BaseBuff
{
    public abstract string Name { get; }
    public abstract string Description { get; }
    public virtual int MaxStack => int.MaxValue;

    public virtual void Apply(Character character)
    {
        character.BuffCounters[Name] = character.BuffCounters.ContainsKey(Name) ? character.BuffCounters[Name] + 1 : 1;
    }

    public virtual void Apply(Character character, int power)
    {
        Apply(character);
    }
}