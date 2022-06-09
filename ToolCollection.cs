using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class ToolCollection : iToolCollection
    {
        // creates a new array of tools for this tool collection
        private Tool[] collection;


        // creates the number of tools to be in the array
        private int number;

        // public reference of tool array
        public ToolCollection()
        {
            collection = new Tool[50];
        }

        // gets and returns the number 
        public int Number { get { return number; } }

        
        // adds tool to tool collection
        public void add(Tool aTool)
        {
            for (int i = 0; i < collection.Length; i++)
            {
                if (collection[i] == null)
                {
                    collection[i] = aTool;
                    break;
                }
            }
            number++;
        }

        // deletes tool from tool collection
        public void delete(Tool aTool)
        {
            for (int i = 0; i < collection.Length; i++)
            {
                if (collection[i] == aTool)
                {
                    collection[i] = null;
                    break;
                }
            }
            number--;
        }

        // search for a tool in tool collection; return true if found and false otherwise
        public bool search(Tool aTool)
        {
            for (int i = 0; i < Number; i++)
            {
                if (collection[i].Equals(aTool))
                {
                    return true;
                }
                
            }
            return false;
        }

        // return tools in selected tool collection
        public Tool[] toArray()
        {
            Tool[] tools = new Tool[Number];
            int index = 0;

            for (int i = 0; i < collection.Length; i++)
            {
                if(collection[i] != null)
                {
                    tools[index] = collection[i];
                    index++;
                }

            }
            return tools;


        }

        public Tool get(int i )
        {
            return collection[i];
        } 
    }
}
