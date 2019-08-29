using System.Collections;
using System.Collections.Generic;

namespace BuildCalypse.CodeGen
{
    public class ShopTriggerObjectives : IEnumerable<TriggerObjective>
    {
        private readonly Dictionary<string, TriggerObjective> Triggers = new Dictionary<string, TriggerObjective>();

        public void AddTriggerFor(Structure structure)
        {
            var trigger = new TriggerObjective("buy-" + structure.Id);
            Triggers[structure.Id] = trigger;
        }

        public TriggerObjective Get(string id)
        {
            return Triggers[id];
        }

        public IEnumerator<TriggerObjective> GetEnumerator()
        {
            return Triggers.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}