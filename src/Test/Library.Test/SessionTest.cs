using NUnit.Framework;
using System;

namespace BankerBot.Test
{
    public class SessionTest
    {
        
        private Session session;

        private EndUser facu, feli;    

        [SetUp]
        public void Setup()
        {
            this.session =  Session.Instance;
            this.facu = new EndUser("Facu", "123");
            this.feli = new EndUser("Feli", "123");
            session.AddUser(facu.Username, facu.Password);
        }

        [Test]
        public void TestAddsUserCorrectly()
        {
            int inicialQuantity = session.AllUsers.Count;

            session.AddUser(feli.Username, feli.Password);
            
            Assert.AreNotEqual(session.AllUsers.Count, inicialQuantity);
        }
        
        [Test]
        public void TestCantAddUserWithRepeatedUsername()
        {
            int inicialQuantity = session.AllUsers.Count;

            session.AddUser(facu.Username, facu.Password);
            
            Assert.AreEqual(session.AllUsers.Count, inicialQuantity);
            
        }
        
        [Test]
        public void TestUsernameExistsReturnsExpectedValue()
        {
            Assert.IsFalse(session.UsernameExists("Juan"));
            Assert.IsTrue(session.UsernameExists("Facu"));

        }

        [Test]
        public void TestGetUserReturnsExpectedEndUser()
        {
            EndUser prueba = session.GetUser("Facu", "123");

            Assert.AreEqual(facu.Username, prueba.Username);
            Assert.AreEqual(facu.Password, prueba.Password);        
        }
        
        [Test]
        public void TestGetUserReturnsNullIfEndUserDoesntExist()
        {
            EndUser prueba = session.GetUser("Juan", "123");

            Assert.IsNull(prueba);
        }

    }
}