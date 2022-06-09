using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Tool : iTool
    {
        // sets up fields to be accessed by getters and setters
        private string name = "Empty";
        private int quantity = 2, availableQuantity = 2, noBorrowings = 0;
        // sets and gets name of tool
        public string Name { get { return name;  } set { name = value; } }
        // sets and gets quantity of tool
        public int Quantity { get {return quantity; } set {int noBorrowed = quantity - availableQuantity; quantity = value; availableQuantity = quantity - noBorrowed; } }
        // sets and gets available quantity of tool
        public int AvailableQuantity { get {return availableQuantity; } set {availableQuantity = value; } }
        // sets and gets number of borrowings of tool
        public int NoBorrowings { get {return noBorrowings; } set { noBorrowings = value; } }
        
        // create members that will borrow tools
        private MemberCollection members = new MemberCollection();
        // get and return all members borrowing tool
        public MemberCollection GetBorrowers { get 
            { return members; } 
        }

        // add a member to list of borrowers
        public void addBorrower(Member aMember)
        {
            members.add(aMember);
        }

        // delete member from list of borrowers
        public void deleteBorrower(Member aMember)
        {
            members.delete(aMember);
        }

        // return a string of name and available quantity of tool
        public override string ToString()
        {
            return "Name: " + Name + "\n\t The Available Quantity is: " + AvailableQuantity;
        }
    }
}
