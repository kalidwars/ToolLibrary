using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Member : iMember, IComparable<Member>
    {
        // sets up fields to be accessed by getters and setters
        private string firstName;
        private string lastName;
        private string contactNumber;
        private string pin;

        // creates the tools that will be borrowed by members
        private ToolCollection toolCollection = new ToolCollection();

        // creates public member object for reference
        public Member() { }

        // sets up constructors for member object
        public Member(string firstName, string lastName, string contactNumber, string pin)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ContactNumber = contactNumber;
            this.PIN = pin;

        }

        // gets and sets first name of member
        public string FirstName { get { return firstName; } set { firstName = value; } }
        // gets and sets last name of member
        public string LastName { get { return lastName; } set { lastName = value; } }
        // gets and sets contact number of member
        public string ContactNumber { get { return contactNumber; } set { contactNumber = value; } }
        // gets and sets pin of member
        public string PIN { get { return pin; } set { pin = value; } }

        // returns a list of tools for member
        public Tool[] Tools
        {
            get
            {
                return toolCollection.toArray();
            }
        }

        // adds tool to list of tools member is borrowing
        public void addTool(Tool aTool)
        {
            toolCollection.add(aTool);

        }

        // compares members first and last names
        public int CompareTo(Member aMember)
        {
            Member m = (Member)aMember;

            if (this.LastName.CompareTo(m.LastName) < 0)
                return -1;
            else
                 if (this.LastName.CompareTo(m.LastName) == 0)
                return this.FirstName.CompareTo(m.FirstName);
            else
                return 1;
        }

        // deletes tool from list of tools member is borrowing
        public void deleteTool(Tool aTool)
        {
            toolCollection.delete(aTool);
            
        }

        // returns string of first and last name of member
        public override string ToString()
        {
            return FirstName + " " + LastName + " " + ContactNumber;
        }
    }
}
