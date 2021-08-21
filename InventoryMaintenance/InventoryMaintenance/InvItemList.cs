using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMaintenance
{
    public class InvItemList    {
        private List<InvItem> invItems;
        //private int index;

        public InvItemList()        {
            invItems = new List<InvItem>();
        }

        public delegate void ChangeHandler(InvItemList invItems);
        public event ChangeHandler Changed;

        public InvItem this[int i]{
            get {
               
                if ( i< 0 || i >= invItems.Count) {
                    throw new ArgumentOutOfRangeException("The index " + i + " does not exist!");
                }
                return invItems[i];

            }
            set {
                invItems[i] = value ;
                Changed(this);
            }
        }

        public int Count => invItems.Count;

        //public InvItem GetItemByIndex(int i) => invItems[i];

        public static InvItemList operator +(InvItemList list, InvItem newItem) {
            list.Add(newItem);
            return list;
        }

        public void Add(InvItem invItem) {
            invItems.Add(invItem);
            Changed(this);
        } 

        public void Add(int itemNo, string description, decimal price)        {
            InvItem i = new InvItem(itemNo, description, price);
            invItems.Add(i);
            Changed(this);
        }

        public static InvItemList operator -(InvItemList list, InvItem newItem){
            list.Remove(newItem);
            return list;
        }

        public void Remove(InvItem invItem) {
            invItems.Remove(invItem);
            Changed(this);
        }

        public void Fill() => invItems = InvItemDB.GetItems();

        public void Save() => InvItemDB.SaveItems(invItems);
    }
}
