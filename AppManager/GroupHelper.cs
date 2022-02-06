using System;
using System.Collections.Generic;

namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor"; 
        public GroupHelper(ApplicationManager manager) : base(manager) { }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();
            OpenGroupsDialogue();
            string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.1f1128c_r7_ad11", 
                "GetItemCount", "#0", "");
            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.1f1128c_r7_ad11",
                "GetText", $"#0|#{i}", "");
                list.Add(new GroupData(item));
            }
            CloseGroupsDialogue();
            return list;
        }

        // Not possible to remove group when only 1 group remaining. Need to create at least one more.
        // TODO: add "AddUntilGroupsPresented" to add multiple groups like in web tests
        internal void AddUntilPossibleToRemove()
        {
            if (GetGroupList().Count <= 1)
            {
                CloseGroupsDialogue();
                Add(new GroupData("autoCreated"));
            }
        }

        public void Remove(int index)
        {
            OpenGroupsDialogue();
            SelectGroup(index);
            ClickDeleteGroup();
            AcceptDeletion();
            CloseGroupsDialogue();
        }

        private void AcceptDeletion()
        {
            aux.Send("{ENTER}");
        }

        private void ClickDeleteGroup()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.1f1128c_r7_ad11");
        }

        private void SelectGroup(int index)
        {
            aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.1f1128c_r7_ad11",
                "Select", $"#0|#{index-1}", "");            
        }

        public void Add(GroupData newGroup)
        {
            OpenGroupsDialogue();
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.1f1128c_r7_ad13");
            aux.Send(newGroup.Name);
            aux.Send("{ENTER}");
            CloseGroupsDialogue();
        }

        private void CloseGroupsDialogue()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.1f1128c_r7_ad14");
        }

        private void OpenGroupsDialogue()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.1f1128c_r7_ad112");
            aux.WinWait(GROUPWINTITLE);
        }

    }
}