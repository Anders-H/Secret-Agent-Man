using System.Collections.Generic;
using System.Linq;

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

    public IOrderedEnumerable<Npc> YSorted() =>
        this.OrderBy(x => x.IntYForYSort);

    public void Reset()
    {
        foreach (var npc in this)
            npc.PutTheGunAway();
    }

    public bool AllAreDead()
    {
        var undetected = this.Count(x => x.Status == Npc.StatusSpyUndetected && x.AliveStatus == Character.StatusAlive);
        var detected = this.Count(x => x.Status == Npc.StatusSpyDetected && x.AliveStatus == Character.StatusAlive);
        return detected + undetected <= 0;
    }
}