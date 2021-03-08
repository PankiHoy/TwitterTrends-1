using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class States
    {
        private static States _statesInstance;

        //could be changed to ObservableCollection<States>
        private List<State> _statesCollection;
        public List<State> StatesCollection { get => _statesCollection; }

        private States(List<State> states)
        {
            _statesCollection = states;
        }

        public static States GetStatesInstance()
        {
            if (_statesInstance == null)
                _statesInstance = new States(new List<State>());

            return _statesInstance;
        }

        public static void CreateStatesInstance(List<State> states)
        {
            _statesInstance = new States(states);
        }
    }
}
