using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTracker
{
    public class Combatant : NotifyBase, IComparable<Combatant>
    {

        public Combatant(string name, int initiative, bool isActive = false)
        {
            Name = name;
            Initiative = initiative;
            TieBreaker = 0;
            IsActive = isActive;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            private set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private int _initiative;
        public int Initiative
        {
            get { return _initiative; }
            set
            {
                _initiative = value;
                OnPropertyChanged();
            }
        }

        private int _tieBreaker;
        public int TieBreaker
        {
            get { return _tieBreaker; }
            set
            {
                _tieBreaker = value;
                OnPropertyChanged();
            }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                OnPropertyChanged();
            }
        }

        public int CompareTo(Combatant other)
        {
            if (Initiative != other.Initiative)
            {
                return Initiative.CompareTo(other.Initiative);
            }
            // If it comes down to a tiebreaker, order by ascending
            return TieBreaker.CompareTo(other.TieBreaker) * -1;
        }
    }
}
