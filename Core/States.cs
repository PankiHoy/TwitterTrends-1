using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class States
    {
        private static States statesInstance;

        //could be changed to ObservableCollection<States>
        private List<State> statesCollection;
        public List<State> StatesCollection { get => statesCollection; }

        private States(List<State> states)
        {
            statesCollection = states;
        }

        public static States GetStatesInstance()
        {
            if (statesInstance == null)
                statesInstance = new States(new List<State>());

            return statesInstance;
        }

        public static void CreateStatesInstance(List<State> states)
        {
            statesInstance = new States(states);
        }
    }
}
