using StatSystem;

public interface IDeathSaver 
{
    int Priority { get; }
    bool TrySave(Resource health);
}
