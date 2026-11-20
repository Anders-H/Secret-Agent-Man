using System.Collections.Generic;

namespace SecretAgentMan.Sprites;

public class NpcList : List<Npc>
{
    public void Act(ulong ticks)
    {
        foreach (var npc in this)
            npc.Act(ticks);
    }

    public void Die(ulong ticks, bool playSound)
    {
        foreach (var npc in this)
            npc.Die(ticks, playSound);
    }

    private readonly List<Npc> _drawOrder = [];
    private readonly List<(Npc Npc, int Index)> _sortBuffer = [];
    private static readonly IComparer<(Npc Npc, int Index)> DrawComparer =
        Comparer<(Npc Npc, int Index)>.Create(static (left, right) =>
        {
            var comparison = left.Npc.IntYForYSort.CompareTo(right.Npc.IntYForYSort);
            return comparison != 0 ? comparison : left.Index.CompareTo(right.Index);
        });

    public List<Npc> YSorted()
    {
        _sortBuffer.Clear();
        for (var i = 0; i < Count; i++)
            _sortBuffer.Add((this[i], i));
        _sortBuffer.Sort(DrawComparer);
        _drawOrder.Clear();
        foreach (var item in _sortBuffer)
            _drawOrder.Add(item.Npc);
        return _drawOrder;
    }

    public void Reset()
    {
        foreach (var npc in this)
            npc.PutTheGunAway();
    }

    public bool AllAreDead() =>
        !Exists(static npc => npc.AliveStatus == Character.StatusAlive
            && npc.Status is Npc.StatusSpyUndetected or Npc.StatusSpyDetected);
}