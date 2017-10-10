using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitiativeTracker
{
    public class TieBreakerWindowViewModel : NotifyBase
    {
        public ObservableCollection<Combatant> Combatants { get; set; }

        private Combatant _selectedCombatant;
        public Combatant SelectedCombatant
        {
            get { return _selectedCombatant; }
            set
            {
                _selectedCombatant = value;
                OnPropertyChanged();
            }
        }

        public ICommand MoveUpCommand { get; private set; }
        public ICommand MoveDownCommand { get; private set; }

        public TieBreakerWindowViewModel(IEnumerable<Combatant> combatants)
        {
            Combatants = new ObservableCollection<Combatant>(combatants);
            MoveUpCommand = new RelayCommand((param) => MoveUp());
            MoveDownCommand = new RelayCommand((param) => MoveDown());
        }

        private void MoveUp()
        {
            if (SelectedCombatant.TieBreaker != 0)
            {
                var previousCombatant = Combatants.FirstOrDefault(c => c.TieBreaker == SelectedCombatant.TieBreaker - 1);
                previousCombatant.TieBreaker++;
                SelectedCombatant.TieBreaker--;
            }
            Combatants.SortByTieBreaker();
        }

        private void MoveDown()
        {
            if (SelectedCombatant.TieBreaker != Combatants.Count - 1)
            {
                var nextCombatant = Combatants.FirstOrDefault(c => c.TieBreaker == SelectedCombatant.TieBreaker + 1);
                nextCombatant.TieBreaker--;
                SelectedCombatant.TieBreaker++;
            }
            Combatants.SortByTieBreaker();
        }
    }
}
