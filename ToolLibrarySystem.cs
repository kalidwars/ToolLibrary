using System;
using System.Collections.Generic;

namespace Assignment
{
    public class ToolLibrarySystem : iToolLibrarySystem
    {
   
        // creates member collection
        public MemberCollection memberCollection =  new MemberCollection();
        // creates the current tool collection
        public ToolCollection toolCollection = new ToolCollection();
        // creates string list of borrowed tools
        private string[] toolsBorrowed;
        // creates public current member
        public Member currentMember;
        // creates public list of tools
        public Tool[] tools = new Tool[0];
        // creates the max number of tools to be borrowed
        public int MAX = 3;
        // creates dictionary for frequency of tools borrowed
        IDictionary<Tool, int> toolFrequence = new Dictionary<Tool, int>();



        // gets index of tool
        public Tool getTool (int index)
        {
            return toolCollection.get(index);
        }
        // adds tool to current tool collection
        public void add(Tool aTool)
        {
            toolCollection.add(aTool);

        }
        // adds tool with quantity to current tool collection
        public void add(Tool aTool, int quantity)
        {
            aTool.Quantity += quantity;
        }
        // adds member to member collection
        public void add(Member aMember)
        {
            memberCollection.add(aMember);
        }
        // borrows tool from tool library and adds member to borrow list and decrements the available quantity of tool
        public void borrowTool(Member aMember, Tool aTool)
        {
            aMember.addTool(aTool);
            aTool.addBorrower(aMember);
            aTool.AvailableQuantity--;

            if (toolFrequence.ContainsKey(aTool))
            {
                toolFrequence[aTool] = (int)toolFrequence[aTool] + 1;
            }
            else
            {
                toolFrequence[aTool] = 1;
            }

        }
        // deletes tool from tool collection
        public void delete(Tool aTool)
        {
            toolCollection.delete(aTool);
        }
        // removes a given quantity of tools from selected tool and decrements the quantity of tool from tool collection
        public void delete(Tool aTool, int quantity)
        {
            if ((aTool.Quantity - quantity) <= 0)
            {
                delete(aTool);
            }
            else
            {
                aTool.Quantity -= quantity;
            }
        }
        // deletes member from member collection
        public void delete(Member aMember)
        {
            memberCollection.delete(aMember);
        }
        // displays the tools a member is borrowing
        public void displayBorrowingTools(Member aMember)
        {
            string[] tools = listTools(aMember);
            if (tools.Length == 0)
            {
                Console.WriteLine("No tools borrowed");
            }
            else
            {
                Console.WriteLine("The Current borrowed tools for member " + aMember.FirstName + " are: ");
                for (int i = 0; i < tools.Length; i++)
                {
                    Console.WriteLine("\n\t" + (i + 1) + ":" + tools[i]);
                }
            }
        }
        // displays all the tools of a given tool type
        public void displayTools(string aToolType)
        {
            Console.WriteLine(aToolType);
        }
        // displays the top three most borrowed tools
        public void displayTopTHree()
        {
            List<KeyValuePair<Tool, int>> arr = new List<KeyValuePair<Tool, int>>();

            foreach (KeyValuePair<Tool, int> item in toolFrequence)

            {
                KeyValuePair<Tool, int> toolFreq = new KeyValuePair<Tool, int>(item.Key, item.Value);
                arr.Add(toolFreq);
            }

            heapSort(arr, arr.Count);
            for (int i = 0; i < arr.Count; i++)
            {
                Console.WriteLine("The no. " + (i + 1) + " most used tool is: "+ arr[arr.Count - i-1].Key.Name + "\n\tThe tool has been borrowed " + arr[arr.Count - i-1].Value + " times");
                
            }
            
        }
        // displays all the tools in the tool collection
        public void displayAllTools()
        {
            for (int i = 0; i < toolCollection.Number; i++)
            {
                Console.WriteLine(i + 1  + " " + toolCollection.get(i));
            }
        }
        // creates and returns a list of tools
        public string[] listTools(Member aMember)
        {
            toolsBorrowed = new string[aMember.Tools.Length];
            for (int i = 0; i < toolsBorrowed.Length; i++)
            {
                if (toolCollection.toArray()[i] != null)
                {
                    toolsBorrowed[i] = aMember.Tools[i].Name;

                }
                
            }
            return toolsBorrowed;
        }
        // returns a tool to the tool collection and removes member from borrower list and increments tool quantity
        public void returnTool(Member aMember, Tool aTool)
        {
            aMember.deleteTool(aTool);
            aTool.AvailableQuantity++;
            aTool.deleteBorrower(aMember);
        }


        //Heap Sort Pair
        private void heapSort(List<KeyValuePair<Tool, int>> arr, int n)
        {
            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(arr, n, i);
            for (int i = n - 1; i >= 0; i--)
            {
                KeyValuePair<Tool, int> temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;
                heapify(arr, i, 0);
            }
        }

        // heapify function returns most frequented tool
        static void heapify(List<KeyValuePair<Tool, int>> arr, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < n && arr[left].Value > arr[largest].Value)
                largest = left;
            if (right < n && arr[right].Value > arr[largest].Value)
                largest = right;
            if (largest != i)
            {
                KeyValuePair<Tool, int> swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;
                heapify(arr, n, largest);
            }
        }

    }
}
