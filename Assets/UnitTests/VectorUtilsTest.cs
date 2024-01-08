using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class VectorUtilsTest
    {
        // ========================= Vector3 =================================

        [Test]
        public void TestVector3With90Degrees()
        {
            Vector3 origin = Vector3.zero;
            Vector3 target = new Vector3(0, 6);

            float theta = VectorUtils.angleBetweenPoints3(origin, target);

            Assert.AreEqual(90f, theta);
        }

        [Test]
        public void TestVector3With45Degrees()
        {
            Vector3 origin = Vector3.zero;
            Vector3 target = new Vector3(6, 6);

            float theta = VectorUtils.angleBetweenPoints3(origin, target);

            Assert.AreEqual(45f, theta);
        }

        [Test]
        public void TestVector3With146Degrees()
        {
            Vector3 origin = Vector3.zero;
            Vector3 target = new Vector3(-3, 2);

            float theta = VectorUtils.angleBetweenPoints3(origin, target);

            Assert.AreEqual(146.309937f, theta);
        }

        [Test]
        public void TestVector3With254Degrees()
        {
            Vector3 origin = Vector3.zero;
            Vector3 target = new Vector3(-2, -7);

            float theta = VectorUtils.angleBetweenPoints3(origin, target);

            Assert.AreEqual(254.054596f, theta);
        }

        [Test]
        public void TestVector3With293Degrees()
        {
            Vector3 origin = Vector3.zero;
            Vector3 target = new Vector3(3, -7);

            float theta = VectorUtils.angleBetweenPoints3(origin, target);

            Assert.AreEqual(293.198608f, theta);
        }

        [Test]
        public void TestSingleVector3With45Degrees()
        {
            Vector3 target = new Vector3(6, 6);

            float theta = VectorUtils.angleInVector3(target);

            Assert.AreEqual(45f, theta);
        }

        [Test]
        public void TestSingleVector3With146Degrees()
        {
            Vector3 target = new Vector3(-3, 2);

            float theta = VectorUtils.angleInVector3(target);

            Assert.AreEqual(146.309937f, theta);
        }

        [Test]
        public void TestSingleVector3With254Degrees()
        {
            Vector3 target = new Vector3(-2, -7);

            float theta = VectorUtils.angleInVector3(target);

            Assert.AreEqual(254.054596f, theta);
        }

        [Test]
        public void TestSingleVector3With293Degrees()
        {
            Vector3 target = new Vector3(3, -7);

            float theta = VectorUtils.angleInVector3(target);

            Assert.AreEqual(293.198608f, theta);
        }

        // ========================= Vector2 =================================

        [Test]
        public void TestVector2With45Degrees()
        {
            Vector2 origin = Vector2.zero;
            Vector2 target = new Vector2(6, 6);

            float theta = VectorUtils.angleBetweenPoints3(origin, target);

            Assert.AreEqual(45f, theta);
        }

        [Test]
        public void TestVector2With146Degrees()
        {
            Vector2 origin = Vector2.zero;
            Vector2 target = new Vector2(-3, 2);

            float theta = VectorUtils.angleBetweenPoints3(origin, target);

            Assert.AreEqual(146.309937f, theta);
        }

        [Test]
        public void TestVector2With254Degrees()
        {
            Vector2 origin = Vector2.zero;
            Vector2 target = new Vector2(-2, -7);

            float theta = VectorUtils.angleBetweenPoints3(origin, target);

            Assert.AreEqual(254.054596f, theta);
        }

        [Test]
        public void TestVector2With293Degrees()
        {
            Vector2 origin = Vector2.zero;
            Vector2 target = new Vector2(3, -7);

            float theta = VectorUtils.angleBetweenPoints3(origin, target);

            Assert.AreEqual(293.198608f, theta);
        }

        [Test]
        public void TestSingleVector2With45Degrees()
        {
            Vector2 target = new Vector2(6, 6);

            float theta = VectorUtils.angleInVector3(target);

            Assert.AreEqual(45f, theta);
        }

        [Test]
        public void TestSingleVector2With146Degrees()
        {
            Vector2 target = new Vector2(-3, 2);

            float theta = VectorUtils.angleInVector3(target);

            Assert.AreEqual(146.309937f, theta);
        }

        [Test]
        public void TestSingleVector2With254Degrees()
        {
            Vector2 target = new Vector2(-2, -7);

            float theta = VectorUtils.angleInVector3(target);

            Assert.AreEqual(254.054596f, theta);
        }

        [Test]
        public void TestSingleVector2With293Degrees()
        {
            Vector2 target = new Vector2(3, -7);

            float theta = VectorUtils.angleInVector3(target);

            Assert.AreEqual(293.198608f, theta);
        }

        // ========================= Mathf.Atan =================================

        [Test]
        public void TestingAtan1Q3()
        {
            float theta = Mathf.Atan(-7f / -2f);

            Assert.AreEqual(254.054611f, theta * Mathf.Rad2Deg + 180f);
        }

        [Test]
        public void TestingAtan1Q4()
        {
            float theta = Mathf.Atan(-7f / 3f);

            Assert.AreEqual(293.198578f, theta * Mathf.Rad2Deg + 360f);
        }
    }
}
