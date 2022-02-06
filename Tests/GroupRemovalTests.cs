using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            // indexToRemove MUST be greater than zero and less that total groups existing
            int indexToRemove = 2;
            app.Groups.AddUntilPossibleToRemove();

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Remove(indexToRemove);
            oldGroups.RemoveAt(indexToRemove - 1);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
