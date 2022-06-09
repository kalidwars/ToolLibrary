using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class MemberCollection : iMemberCollection
    {
        private int number;
        public int Number { get { return number; } }

        public BSTree community; 

        // instantiate new BSTree
        public MemberCollection ()
        {
            community = new BSTree();
        }

        // insert a member to BSTree and increment number
        public void add(Member aMember)
        {
            community.Insert(aMember);
            number++;

        }

        // delete a member from BSTree and decrement number
        public void delete(Member aMember)
        {
            community.Delete(aMember);
            number--;

        }

        // boolean to search for member in BSTree
        public bool search(Member aMember)
        {
            for (int i = 0; i < Number; i++)
            {
                if (community.Search(aMember))
                {
                    return true;
                }
            }
            return false;
        }

        // returns the members in the collection to an array 
        public Member[] toArray()
        {
            return community.toArrayBST;
        }
    }
}
