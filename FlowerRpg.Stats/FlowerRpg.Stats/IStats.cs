namespace FlowerRpg.Stats;

public interface IStats
{
    public Vital GetVital(int statType);
    public IStat GetStat(int statType);
}