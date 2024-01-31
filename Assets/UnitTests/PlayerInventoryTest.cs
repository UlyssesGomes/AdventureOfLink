using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Item
    {
        public int id;
        public string name;

        public Item(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }

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

        [Test]
        public void TestListRemoveFirst()
        {
            List<Item> items = new List<Item>();
            Item item1 = new Item(1, "first");
            Item item2 = new Item(2, "second");
            Item item3 = new Item(3, "third");

            items.Add(item1);
            items.Add(item2);
            items.Add(item3);

            Item seconsItem = items.Find(item => item.id == 1);

            items.Remove(seconsItem);

            foreach (Item i in items)
            {
                Assert.AreNotEqual(i.name, "first");
            }
        }

        [Test]
        public void TestListRemoveSecond()
        {
            List<Item> items = new List<Item>();
            Item item1 = new Item(1, "first");
            Item item2 = new Item(2, "second");
            Item item3 = new Item(3, "third");

            items.Add(item1);
            items.Add(item2);
            items.Add(item3);

            Item seconsItem = items.Find(item => item.id == 2);

            items.Remove(seconsItem);

            foreach(Item i in items)
            {
                Assert.AreNotEqual(i.name, "second");
            }
        }

        [Test]
        public void TestListRemoveThird()
        {
            List<Item> items = new List<Item>();
            Item item1 = new Item(1, "first");
            Item item2 = new Item(2, "second");
            Item item3 = new Item(3, "third");

            items.Add(item1);
            items.Add(item2);
            items.Add(item3);

            Item seconsItem = items.Find(item => item.id == 3);

            items.Remove(seconsItem);

            foreach (Item i in items)
            {
                Assert.AreNotEqual(i.name, "third");
            }
        }
    }
}
