using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerInventoryTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void PlayerInventoryTestSimplePasses()
        {
            // TODO - small refactor to add necessaries Assembly file to load PlayerInventory here.
            // Cenary to be tested: Add 8 logs, 3 in firtst slot and 5 in second. Try to remove 4 then 
            // when then first slot is found first. The bug found its cant removed, because the method 
            // dont look at the second slot. Its fixed now.
            //PlayerInventory playerInventory;
            Assert.AreEqual(90f, 90f);
        }
    }
}
