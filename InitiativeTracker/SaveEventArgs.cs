using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTracker
{
    public class SaveEventArgs : EventArgs
    {
        public IEnumerable<Combatant> SaveData { get; set; }

        public SaveEventArgs()
        {
        }

        public SaveEventArgs(IEnumerable<Combatant> saveData)
        {
            SaveData = saveData;
        }
    }
}
