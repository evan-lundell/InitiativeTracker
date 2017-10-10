using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTracker
{
    public static class Extensions
    {
        public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable<T>
        {
            for (int counter = 0; counter < collection.Count - 1; counter++)
            {
                int index = counter + 1;
                while (index > 0)
                {
                    if (collection[index - 1].CompareTo(collection[index]) < 0)
                    {
                        T temp = collection[index - 1];
                        collection[index - 1] = collection[index];
                        collection[index] = temp;
                    }
                    index--;
                }
            }
        }

        public static void SortByTieBreaker(this ObservableCollection<Combatant> combatants)
        {
            for (int counter = 0; counter < combatants.Count - 1; counter++)
            {
                int index = counter + 1;
                while (index > 0)
                {
                    if (combatants[index - 1].TieBreaker > combatants[index].TieBreaker)
                    {
                        Combatant temp = combatants[index - 1];
                        combatants[index - 1] = combatants[index];
                        combatants[index] = temp;
                    }
                    index--;
                }
            }
        }
    }
}
