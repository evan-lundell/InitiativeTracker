using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitiativeTracker
{
    public class MainWindowViewModel : NotifyBase
    {
        public ObservableCollection<Combatant> Combatants { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _initiative;
        public string Initiative
        {
            get { return _initiative; }
            set
            {
                _initiative = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand PreviousCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand LoadCommand { get; set; }

        public event EventHandler<SaveEventArgs> SaveTriggered;
        public event EventHandler<SaveEventArgs> LoadTriggered;
        public event EventHandler<TieInitiativeEventArgs> InitiativeTied;

        public MainWindowViewModel()
        {
            Combatants = new ObservableCollection<Combatant>();
            AddCommand = new RelayCommand((param) => Add());
            ResetCommand = new RelayCommand((param) => Reset());
            SaveCommand = new RelayCommand((param) => Save());
            LoadCommand = new RelayCommand((param) => Load());
            NextCommand = new RelayCommand((param) => Next());
            PreviousCommand = new RelayCommand((param) => Previous());
        }

        private void Add()
        {
            int initiative;
            if (!int.TryParse(Initiative, out initiative))
            {
                throw new Exception($"{Initiative} is not a valid value.");
            }

            bool initiativeTied = Combatants.Any(c => c.Initiative == initiative);
            var newCombatant = new Combatant(Name, initiative, Combatants.Count == 0);
            Combatants.Add(newCombatant);
            if (initiativeTied && InitiativeTied != null)
            {
                newCombatant.TieBreaker = Combatants.Max(c => c.TieBreaker) + 1;
                InitiativeTied?.Invoke(this, new TieInitiativeEventArgs(Combatants.Where(c => c.Initiative == initiative)));
            }
            Combatants.Sort();
            Name = null;
            Initiative = null;
        }

        private void Reset()
        {
            Combatants.Clear();
            Name = null;
            Initiative = null;
        }

        private void Save()
        {
            SaveTriggered?.Invoke(this, new SaveEventArgs(Combatants));
        }

        private void Load()
        {
            Combatants.Clear();
            var eventArgs = new SaveEventArgs();
            LoadTriggered?.Invoke(this, eventArgs);
            foreach (Combatant combatant in eventArgs.SaveData)
            {
                Combatants.Add(combatant);
            }
            Combatants.Sort();
        }

        private void Next()
        {
            Combatant activeCombatant = Combatants.FirstOrDefault(c => c.IsActive);
            activeCombatant.IsActive = false;
            int index = Combatants.IndexOf(activeCombatant);
            if (index == Combatants.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            Combatants[index].IsActive = true;
        }

        private void Previous()
        {
            Combatant activeCombatant = Combatants.FirstOrDefault(c => c.IsActive);
            activeCombatant.IsActive = false;
            int index = Combatants.IndexOf(activeCombatant);
            if (index == 0)
            {
                index = Combatants.Count - 1;
            }
            else
            {
                index--;
            }
            Combatants[index].IsActive = true;
        }
    }
}
