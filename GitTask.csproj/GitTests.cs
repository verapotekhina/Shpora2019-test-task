using System;
using NUnit.Framework;

namespace GitTask
{
    [TestFixture]
    public class GitTests
    {
        private const int DefaultFilesCount = 3;
        private Git sut;

        [SetUp]
        public void SetUp()
        {
            sut = new Git(DefaultFilesCount);
        }

        [Test]
        public void Update_Commit_Update_Checkout()
        {
            sut.Update(0, 5);     
            var com = sut.Commit();         
            sut.Update(0, 6);
            var check = sut.Checkout(0, 0);

            Assert.AreEqual(0, com);
            Assert.AreEqual(5, check);
        }

        [Test]
        public void Update_Commit_Update_Checkout_Commit_Checkout()
        {
            sut.Update(0, 5);
            var com = sut.Commit();     
            Assert.AreEqual(0, com);
            sut.Update(0, 6);
            var check = sut.Checkout(0, 0);
            Assert.AreEqual(5, check);
            com = sut.Commit();
            Assert.AreEqual(1, com);
            check = sut.Checkout(1, 0);
            Assert.AreEqual(6, check);
        }
        
        [Test]
        public void Checkout_NoUpdate_Commit___ReturnsZero()
        {
            var com = sut.Commit(); 
            var check = sut.Checkout(0, 1);    
            Assert.AreEqual(0, com);
            Assert.AreEqual(0, check);
        }
        
        [Test]
        public void NoUpdate_Commit___ReturnsZero()
        {
            var com = sut.Commit(); 
            Assert.AreEqual(0, com);
        }
        
        [Test]
        public void Update_Checkout___Exception()
        {
            sut.Update(0, 5);
            Assert.Throws<ArgumentException>(() =>sut.Checkout(0,0));
        }
        [Test]
        public void Checkout_CommitNumberIsBiggerThanMaxCommit_ArgumentException()
        {
            sut.Update(0, 5);
            var com = sut.Commit();
            Assert.AreEqual(0, com);
            Assert.Throws<ArgumentException>(() =>sut.Checkout(5,0));
        }
        [Test]
        public void MoreCommit_And_Exception()
        {
            sut.Update(0, 5);
            var com = sut.Commit();
            Assert.AreEqual(0, com);
            com = sut.Commit();
            Assert.AreEqual(1, com);
            com = sut.Commit();
            Assert.AreEqual(2, com);
            com = sut.Commit();
            Assert.AreEqual(3, com);
            com = sut.Commit();
            Assert.AreEqual(4, com);
            Assert.Throws<ArgumentException>(() =>sut.Checkout(5,0));
        }
        [Test]
        public void Checkout_ReturnsValueFromLastUpdate()
        {
            sut.Update(0, 5);
            var com = sut.Commit();
            com = sut.Commit();
            com = sut.Commit();
            com = sut.Commit();
            com = sut.Commit();
            com = sut.Commit();
            sut.Update(1, 5);
            com = sut.Commit();
            com = sut.Commit();
            sut.Update(2, 30);
            com = sut.Commit(); 
            sut.Update(0, 10);
            com = sut.Commit();
            sut.Update(1, 20);
            com = sut.Commit();
            Assert.AreEqual(10, com);
            var check = sut.Checkout(10, 0); 
            Assert.AreEqual(10, check);
            check = sut.Checkout(10, 1); 
            Assert.AreEqual(20, check);
            check = sut.Checkout(10, 2); 
            Assert.AreEqual(30, check);
            check = sut.Checkout(7, 2); 
            Assert.AreEqual(0, check);
            check = sut.Checkout(7, 1); 
            Assert.AreEqual(5, check);
            check = sut.Checkout(3, 1); 
            Assert.AreEqual(0, check);
        }
        
        [Test]
        public void Checkout_ReturnsValueFromLastUpdate_1()
        {
            var a = (0, 1);
            var b = (1, 0);
            Console.WriteLine(a.CompareTo(b));

        }
    }
}