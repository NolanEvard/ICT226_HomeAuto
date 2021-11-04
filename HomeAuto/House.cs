using System;
using System.Collections.Generic;

namespace HomeAuto
{
    public class House
    {
        //Attributes

        private int switchedOnCount;
        private int switchedOffCount;
        private List<Switch> switches = new List<Switch>();

        //properties
        public int SwitchedOnCount
        {
            get
            {
                return switchedOnCount;
            }
        }

        public int SwitchedOffCount
        {
            get
            {
                return switchedOffCount;
            }
        }

        //methods

        public void AddSwitch(string name)
        {
            Switch newSwitch = new Switch(name);

            if (CheckIfSwitchExists(name)){
                throw new NameAlreadyTakenException();
            }
            else
            {
                switches.Add(newSwitch);
                UpdateSwitchedOffCount(switchedOffCount + 1);
            }
        }
        public void SwitchOn(string name)
        {
            ChangeOneItemState(false, name);
        }
        public void SwitchOff(string name)
        {
            ChangeOneItemState(true, name);
        }
        public void SwitchOnAll()
        {
            ChangeAllItemState(false);
        }
        public void SwitchOffAll()
        {
            ChangeAllItemState(true);
        }

        private bool CheckIfSwitchExists(string name)
        {
            foreach (Switch item in switches)
            {
                if(item.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        private void UpdateSwitchedOffCount(int count)
        {
            switchedOffCount = count;
        }
        private void UpdateSwitchedOnCount(int count)
        {
            switchedOnCount = count;
        }
        private void ChangeOneItemState(bool initialState, string name)
        {
            if (!(CheckIfSwitchExists(name)))
            {
                throw new UnknownNameException();
            }
            foreach (Switch item in switches)
            {
                if (item.Name == name)
                {
                    if (item.State == initialState)
                    {
                        item.ChangeState();
                        if (initialState)
                        {
                            UpdateSwitchedOffCount(switchedOffCount + 1);
                            UpdateSwitchedOnCount(SwitchedOnCount - 1);
                        }
                        else
                        {
                            UpdateSwitchedOffCount(switchedOffCount - 1);
                            UpdateSwitchedOnCount(SwitchedOnCount + 1);
                        }
                    }
                }
            }
        }
        private void ChangeAllItemState(bool initialState)
        {
            foreach (Switch item in switches)
            {
                if (item.State == initialState)
                {
                    item.ChangeState();
                }
            }
            if (initialState)
            {
                UpdateSwitchedOnCount(0);
                UpdateSwitchedOffCount(switches.Count);
            }
            else
            {
                UpdateSwitchedOnCount(switches.Count);
                UpdateSwitchedOffCount(0);
            }
        }
    }
    public class HouseExceptions : Exception {}
    public class NameAlreadyTakenException : HouseExceptions {}
    public class UnknownNameException : HouseExceptions {}

}
