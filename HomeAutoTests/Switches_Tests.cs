using NUnit.Framework;
using HomeAuto;

namespace HomeAutoTests
{
    public class Switches_Tests
    {
        private House _house;

        [SetUp]
        public void Setup()
        {
            _house = new House();
        }

        [Test]
        public void Empty_House_Switches_Counts_Are_Zero()
        {
            Assert.AreEqual(0, _house.SwitchedOnCount);
            Assert.AreEqual(0, _house.SwitchedOffCount);

            _house.SwitchOnAll();
            Assert.AreEqual(0, _house.SwitchedOnCount);
            Assert.AreEqual(0, _house.SwitchedOffCount);

            _house.SwitchOffAll();
            Assert.AreEqual(0, _house.SwitchedOnCount);
            Assert.AreEqual(0, _house.SwitchedOffCount);
        }

        [Test]
        public void Switch_Name_Is_Unique()
        {
            _house.AddSwitch("Lumière Couloir");
            _house.AddSwitch("Lumière Cave");
            Assert.Throws<NameAlreadyTakenException>(delegate
            {
                _house.AddSwitch("Lumière Couloir");
            });
        }

        [Test]
        public void Switch_All_Has_Correct_Counts()
        {
            _house.AddSwitch("Lumière Couloir");
            _house.AddSwitch("Lumière Cave");
            Assert.AreEqual(0, _house.SwitchedOnCount);
            Assert.AreEqual(2, _house.SwitchedOffCount);

            _house.SwitchOnAll();
            Assert.AreEqual(2, _house.SwitchedOnCount);
            Assert.AreEqual(0, _house.SwitchedOffCount);

            _house.AddSwitch("Cafetière");
            Assert.AreEqual(2, _house.SwitchedOnCount);
            Assert.AreEqual(1, _house.SwitchedOffCount);

            _house.SwitchOnAll();
            Assert.AreEqual(3, _house.SwitchedOnCount);
            Assert.AreEqual(0, _house.SwitchedOffCount);

            _house.SwitchOffAll();
            Assert.AreEqual(0, _house.SwitchedOnCount);
            Assert.AreEqual(3, _house.SwitchedOffCount);
        }

        [Test]
        public void Switch_On_Named_Items()
        {
            _house.AddSwitch("Lumière Couloir");
            _house.AddSwitch("Lumière Cave");
            _house.AddSwitch("Cafetière");
            Assert.AreEqual(0, _house.SwitchedOnCount);
            Assert.AreEqual(3, _house.SwitchedOffCount);

            _house.SwitchOn("Cafetière");
            Assert.AreEqual(1, _house.SwitchedOnCount);
            Assert.AreEqual(2, _house.SwitchedOffCount);

            _house.SwitchOn("Lumière Cave");
            Assert.AreEqual(2, _house.SwitchedOnCount);
            Assert.AreEqual(1, _house.SwitchedOffCount);

            _house.SwitchOn("Cafetière");
            Assert.AreEqual(2, _house.SwitchedOnCount);
            Assert.AreEqual(1, _house.SwitchedOffCount);
        }

        [Test]
        public void Switch_Off_Named_Items()
        {
            _house.AddSwitch("Lumière Couloir");
            _house.AddSwitch("Lumière Cave");
            _house.AddSwitch("Cafetière");
            _house.SwitchOnAll();
            Assert.AreEqual(3, _house.SwitchedOnCount);
            Assert.AreEqual(0, _house.SwitchedOffCount);

            _house.SwitchOff("Cafetière");
            Assert.AreEqual(2, _house.SwitchedOnCount);
            Assert.AreEqual(1, _house.SwitchedOffCount);

            _house.SwitchOff("Lumière Cave");
            Assert.AreEqual(1, _house.SwitchedOnCount);
            Assert.AreEqual(2, _house.SwitchedOffCount);

            _house.SwitchOff("Cafetière");
            Assert.AreEqual(1, _house.SwitchedOnCount);
            Assert.AreEqual(2, _house.SwitchedOffCount);
        }

        [Test]
        public void Switch_Unknown_Items_Fails()
        {
            Assert.Throws<UnknownNameException>(delegate
            {
                _house.SwitchOn("Cafetière");
            });

            _house.AddSwitch("Cafetière");
            _house.SwitchOn("Cafetière");

            Assert.Throws<UnknownNameException>(delegate
            {
                _house.SwitchOff("Lumière Couloir");
            });

            _house.AddSwitch("Lumière Couloir");
            _house.SwitchOff("Lumière Couloir");
        }
    }
}
