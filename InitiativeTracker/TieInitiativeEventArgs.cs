using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTracker
{
    public class TieInitiativeEventArgs : EventArgs
    {
        private IEnumerable<Combatant> Combatants { get; set; }

        public TieInitiativeEventArgs(IEnumerable<Combatant> combatants)
        {
            Combatants = combatants;
        }
    }
}
