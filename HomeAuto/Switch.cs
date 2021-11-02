using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAuto
{
    public class Switch
    {
        private string name;
        private bool state = false;
        public Switch(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
        public bool State
        {
            get
            {
                return state;
            }
        }
        
        public void ChangeState()
        {
            state = !state;
        }
    }
}
