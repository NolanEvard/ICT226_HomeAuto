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
            if (!(CheckIfSwitchExists(name)))
            {
                throw new UnknownNameException();
            }

            bool itemHasBeenChanged = false;
            foreach (Switch item in switches)
            {
                if (item.Name == name)
                {
                    if (!(item.State)){
                        item.ChangeState();
                        itemHasBeenChanged = true;
                        UpdateSwitchedOffCount(switchedOffCount - 1);
                        UpdateSwitchedOnCount(SwitchedOnCount + 1);
                    }
                    else
                    {
                        itemHasBeenChanged = true;
                    }
                }
            }
            if (!(itemHasBeenChanged))
            {
                throw new UnknownNameException();
            }
        }
        public void SwitchOff(string name)
        {
            bool itemHasBeenChanged = false;

            if (!(CheckIfSwitchExists(name)))
            {
                throw new UnknownNameException();
            }

            foreach (Switch item in switches)
            {
                if (item.Name == name)
                {
                    if (item.State)
                    {
                        item.ChangeState();
                        itemHasBeenChanged = true;
                        UpdateSwitchedOffCount(switchedOffCount + 1);
                        UpdateSwitchedOnCount(SwitchedOnCount - 1);
                    }
                    else
                    {
                        itemHasBeenChanged = true;
                    }
                }
            }
            if (!(itemHasBeenChanged))
            {
                throw new UnknownNameException();
            }
        }
        public void SwitchOnAll()
        {
            foreach (Switch item in switches)
            {
                if (!(item.State)) {
                    item.ChangeState();
                }
            }
            UpdateSwitchedOnCount(switches.Count);
            UpdateSwitchedOffCount(0);
        }
        public void SwitchOffAll()
        {
            foreach (Switch item in switches)
            {
                if (item.State)
                {
                    item.ChangeState();
                }
                UpdateSwitchedOnCount(0);
                UpdateSwitchedOffCount(switches.Count);
            }
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
    }
    public class HouseExceptions : Exception {}
    public class NameAlreadyTakenException : HouseExceptions {}
    public class UnknownNameException : HouseExceptions {}

}
