using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class MenusProgram
    {
        // creates tool library object to reference
        private ToolLibrarySystem LibrarySystem = new ToolLibrarySystem();
        // creates main menu interface
        public void CreateMainMenu()
        {            
            Console.Out.WriteLine("\nWelcome to the Tool Library !!!\n"
                                + "\n=========== Main Menu ==========="
                                + "\n 1. Staff Login"
                                + "\n 2. Member Login"
                                + "\n"
                                + "\n 0. Exit"
                                + "\n=================================\n"
                                + "\nPlease make a selection (1-2, or 0 to exit): ");

            string input = Console.ReadLine();
            Console.WriteLine("");
                        
            switch (input)
            {
                case "1":
                    LoginStaff();
                    break;
                case "2":
                    LoginMember();
                    break;
                case "0":
                    Console.WriteLine("Exiting System");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Incorrect selection! Try again\n");
                    break;

            }

        }
        // creates login procedure interface for staff
        public void LoginStaff()
        {
            Member[] curr = LibrarySystem.memberCollection.toArray();
            Console.WriteLine("==========Staff Login==========");
            Console.Write("Enter your username: ");
            string staffUser = Console.ReadLine();
            Console.Write("Enter your password: ");
            string staffPass = Console.ReadLine();
            Console.WriteLine("================================");
            Console.WriteLine();
            if (staffUser == "staff" && staffPass == "today123")
            {
                Console.WriteLine("Logged in successfully!\n");
                CreateStaffMenu();
            }
            else
            {
                Console.WriteLine("Incorrect login details! Try again\n");
                CreateMainMenu();
            }
        }
        // creates login procedure interface for member
        public void LoginMember()
        {
            Member[] curr = LibrarySystem.memberCollection.toArray();
            Console.WriteLine("==============================Member Login==============================");
            Console.Write("Enter your Username (LastNameFirstname): ");
            string username = Console.ReadLine();
            Console.Write("Enter your pin: ");
            string PIN = Console.ReadLine();
            Console.WriteLine("========================================================================");
            Console.WriteLine();

            for (int i = 0; i < curr.Length; i++)
            {
                if (curr[i].LastName + curr[i].FirstName == username && curr[i].PIN == PIN)
                {
                    LibrarySystem.currentMember = curr[i];

                    if (LibrarySystem.currentMember != null)
                    {
                        Console.WriteLine("Logged in successfully!\n");
                        CreateMemberMenu();

                    }
                    
                }

                else
                {
                    Console.WriteLine("Invalid Member login details.\n");
                    CreateMainMenu();

                }

            }
                
            
        }
        // creates staff menu interface
        public void CreateStaffMenu()
        {
            int user_input;

            Console.Out.WriteLine("Welcome to the Tool Library !!!\n");
            Console.Out.WriteLine("================ Staff Menu ================");
            Console.Out.WriteLine("1. Add a new tool");
            Console.Out.WriteLine("2. Add new pieces of an existing tool");
            Console.Out.WriteLine("3. Remove some pieces of a tool");
            Console.Out.WriteLine("4. Register a new member");
            Console.Out.WriteLine("5. Remove a member");
            Console.Out.WriteLine("6. Find the contact number of a member");
            Console.Out.WriteLine("0. Return to Main Menu");
            Console.Out.WriteLine("===========================================\n");
            Console.Out.WriteLine("Please make a selection (1-6, or 0 to return to Main Menu): ");

            string input = Console.ReadLine();
            user_input = int.Parse(input);
            Console.WriteLine("");

            switch (user_input)
            {
                case 1:
                    StaffAddNewTool();
                    CreateStaffMenu();
                    break;
                case 2:
                    StaffAddExistingTool();
                    CreateStaffMenu();
                    break;
                case 3:
                    StaffRemoveTool();
                    CreateStaffMenu();
                    break;
                case 4:
                    StaffAddNewMember();
                    CreateStaffMenu();
                    break;
                case 5:
                    StaffRemoveMember();
                    CreateStaffMenu();
                    break;
                case 6:
                    StaffContact();
                    CreateStaffMenu();
                    break;
                case 0:
                    CreateMainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.\n");
                    CreateMainMenu();
                    break;
            }

        }
        // creates interface for staff to add new tool to tool library
        private void StaffAddNewTool()
        {
            try
            {
                CreateToolCategories();
                Tool toolK = new Tool();
                Console.WriteLine("\n Enter Tool Name: ");
                toolK.Name = Console.ReadLine();

                try
                {
                    Console.WriteLine("\n Enter Number of Tools: ");
                    toolK.Quantity = Convert.ToInt32(Console.ReadLine());
                    LibrarySystem.add(toolK);
                    Console.WriteLine("\n " + toolK.Quantity + " piece(s) of the Tool: '" + toolK.Name + "' have been added successfully\n");
                }catch (Exception)
                {
                    Console.WriteLine("Incorrect Number! Try again\n");
                }
            }catch (Exception)
            {
                Console.WriteLine("Incorrect Number! Try again\n");
            }
        }
        // creates interface for staff to add to an existing tool to tool library
        private void StaffAddExistingTool()
        {
            try
            {
                CreateToolCategories();
                Tool[] tools = LibrarySystem.toolCollection.toArray();
                if (showTool(tools))
                {
                    try
                    {
                        Console.Write("Choose the Tool you want to add to: ");
                        int input = Convert.ToInt32(Console.ReadLine());

                        Tool tool = tools[input - 1];
                        Console.Write("\nEnter the quantity of tools to add: ");
                        
                        int qty = Convert.ToInt32(Console.ReadLine());
                        LibrarySystem.add(tool, qty);
                        
                        Console.WriteLine("\n '" + qty + "' " + tool.Name + "(s). ' have been added\n");
                        Console.WriteLine("The updated Available amount of " + tool.Name + " is: " + tool.AvailableQuantity + "\n");
                    
                    }catch (Exception)
                    {
                        Console.WriteLine("\nIncorrect number!\n");
                    }
                }
            }catch (Exception)
            {
                Console.WriteLine("\nIncorrect number! Try again\n");
            }
        }
        // creates interface for staff to remove tool from tool library
        private void StaffRemoveTool()
        {
            try
            {
                CreateToolCategories();
                Tool[] tools = LibrarySystem.toolCollection.toArray();
                if (showTool(tools))
                {
                    try
                    {
                        Console.Write("Choose the Tool you want to remove from: ");
                        int input = Convert.ToInt32(Console.ReadLine());

                        Console.Write("\nEnter quantity of tools to remove: ");
                        int qty = Convert.ToInt32(Console.ReadLine());

                        Tool tool = tools[input - 1];
                        int output = tool.Quantity - qty;
                        bool check = (output <= 0);

                        Console.WriteLine("\nRemoved " + (check ? "all" : Convert.ToString(qty)) + " " + tool.Name + "(s)." + (check ? "" : " " + output + " remaining.") + "\n");
                        LibrarySystem.delete(tool, qty);

                    }catch (Exception)
                    {
                        Console.WriteLine("\nIncorrect number!\n");
                    }
                }
            }catch (Exception)
            {
                Console.WriteLine("\nIncorrect number! Try again\n");
            }
        }
        // creates interface to add member to tool library
        private void StaffAddNewMember()
        {            
            Member member = new Member();

            Console.WriteLine("Enter Member's First Name: ");
            member.FirstName = Console.ReadLine();
            Console.WriteLine("Enter Member's Last Name: ");
            member.LastName = Console.ReadLine();
            Console.WriteLine("Enter Members Contact Number: ");
            member.ContactNumber = Console.ReadLine();
            Console.WriteLine("Enter Member's PIN: ");
            member.PIN = Console.ReadLine();
            LibrarySystem.memberCollection.add(member);
            Console.WriteLine("\nThe Member '" + member.FirstName + " " + member.LastName +"' has been added successfully.\n");
        }
        // creates interface for staff to remove a member from tool library
        private void StaffRemoveMember()
        {
            Member[] members = LibrarySystem.memberCollection.toArray();
            if (showMembers(members))
            {
                Console.Write("Choose the number of the member you want to remove: ");
                int input = Convert.ToInt16(Console.ReadLine());
                Member member = members[input - 1];
                try
                {
                    LibrarySystem.memberCollection.delete(member);
                    Console.WriteLine("\n The member " + member.FirstName + " " + member.LastName + " has been removed from the system");
                }
                catch (Exception)
                {
                    Console.WriteLine("\nInvalid number.");
                }
            }
        }
        // creates interface to search for members contact via their first and last names
        private void StaffContact()
        {
            Member[] members = LibrarySystem.memberCollection.toArray();

            if (members.Length == 0)
                Console.WriteLine("There are no members to be displayed!");
            else
            {
                string output = "There is no member found by that name!";
                Console.Write("Enter the the Member's first name: ");
                string fn = Console.ReadLine();
                Console.Write("Enter the the Member's last name: ");
                string ln = Console.ReadLine();
                for (int i = 0; i < members.Length; ++i)
                    if (members[i].FirstName == fn && members[i].LastName == ln)
                    {

                        string contact = fn + "'s contact number is: " + members[i].ContactNumber;
                        Console.WriteLine("\n" + contact);

                    }
                    else
                    {
                        Console.WriteLine("\n" + output);

                    }
                
                
            }
            Console.WriteLine("");
        }
        // creates user interface for member menu
        public void CreateMemberMenu()
        {
            int user_input;

            Console.Out.WriteLine("Welcome to the Tool Library !!!\n");
            Console.Out.WriteLine("================ Member Menu ================");
            Console.Out.WriteLine("1. Display tools by category");
            Console.Out.WriteLine("2. Borrow tool from library");
            Console.Out.WriteLine("3. Return tool(s) to library");
            Console.Out.WriteLine("4. List tool(s) on loan");
            Console.Out.WriteLine("5. Display most frequently borrowed tools");
            Console.Out.WriteLine("0. Return to Main Menu");
            Console.Out.WriteLine("============================================\n");
            Console.Out.WriteLine("Please make a selection (1-5, or 0 to return to Main Menu): ");

            string input = Console.ReadLine();
            user_input = int.Parse(input);

            Console.WriteLine("");

            switch (user_input)
            {
                case 1:
                    MemberTools();
                    CreateMemberMenu();
                    break;
                case 2:
                    MemberBorrow();
                    CreateMemberMenu();
                    break;
                case 3:
                    MemberReturn();
                    CreateMemberMenu();
                    break;
                case 4:
                    MemberBorrowed();
                    CreateMemberMenu();
                    break;
                case 5:
                    MemberTopThree();
                    CreateMemberMenu();
                    break;                
                case 0:
                    CreateMainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.\n");
                    CreateMemberMenu();
                    break;
            }
        }
        // creates interface to show all the tools in the tool library
        private void MemberTools()
        {
            try
            {
                CreateToolCategories();
                showTool(LibrarySystem.toolCollection.toArray());
                Console.WriteLine();
            }catch (Exception)
            {
                Console.WriteLine("\nInvalid option, try again.\n");
            }
        }
        // creates a user interface for member to borrow a tool from tool library
        private void MemberBorrow()
        {
            if (LibrarySystem.currentMember.Tools.Length < LibrarySystem.MAX)
            {
                try
                {
                    CreateToolCategories();
                    Tool[] tools = LibrarySystem.toolCollection.toArray();
                    if (showTool(tools))
                    {
                        Console.Write("\nEnter the name of the tool you want to borrow: ");
                        string input = Console.ReadLine();
                        string output = "There are no tools by that name.";
                        for (int i = 0; i < tools.Length; i++)
                        {
                            Tool tool = tools[i];
                            if (tool.Name == input && tool.AvailableQuantity <= 0)
                            {
                                output = "No available " + tool.Name + "(s) to borrow.";
                                break;
                            }
                            else if (tool.Name == input && tool.AvailableQuantity > 0)
                            {
                                LibrarySystem.borrowTool(LibrarySystem.currentMember, tools[i]);
                                output = "\tYou have borrowed a '" + tool.Name + "'.\n";
                                tool.NoBorrowings++;
                                break;
                            }
                        }
                        Console.WriteLine(output + "\n");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("\nInvalid option, try again.\n");
                }
            }
            else
            {
                Console.WriteLine("\nYou can't borrow anymore tools.\n");
            }
        }
        // creates a user interface for member to return a tool to tool library
        void MemberReturn()
        {
            try
            {
                Tool[] tools = LibrarySystem.currentMember.Tools;
                Console.WriteLine("Current borrowed tools for " + LibrarySystem.currentMember.FirstName + ":");
                if (tools.Length > 0)
                {
                    for (int i = 0; i < tools.Length; ++i)
                    {
                        Console.WriteLine("\t" + (i + 1) + ". " + tools[i].Name);

                    }
                    Console.Write("Enter the number of the tool you want to return: ");
                    int input = Convert.ToInt16(Console.ReadLine());
                    Tool tool = tools[input - 1];
                    LibrarySystem.returnTool(LibrarySystem.currentMember, tool);
                    tool.NoBorrowings--;
                    Console.WriteLine("\nYou have returned " + tool + ".\n");



                }
                else
                {
                    Console.WriteLine("\tThere are no tools to be returned.\n");
                    CreateMemberMenu();
                }
            }catch (Exception)
            {
                Console.WriteLine("\nInvalid number.\n");
            }
        }
        // creates interface to show the tools a member is currently borrowing
        private void MemberBorrowed()
        {
            LibrarySystem.displayBorrowingTools(LibrarySystem.currentMember);

        }
        // creates interface to display the top three most borrowed tools
        private void MemberTopThree()
        {
            
            LibrarySystem.displayTopTHree();          
            
        }
        // helper function to show tools
        private bool showTool(Tool[] tools)
        {
            Console.WriteLine("\nThe selected category:");
            if (tools.Length == 0)
            {
                Console.WriteLine("\n\tThere are no tools to be displayed!\n");
                return false;
            }
            else
            {
                for (int i = 0; i < tools.Length; i++)
                {
                    Console.Write("\t" + (i + 1) + ". ");
                    writeTools(tools[i].ToString());
                }

                return true;
            }
        }

        // helper function to print tools
        public void writeTools(string aToolType)
        {
            Console.WriteLine(aToolType);
        }
        // helper function to show members
        private bool showMembers(Member[] members)
        {
            Console.WriteLine("Members Registered:");
            if (members.Length == 0)
            {
                Console.WriteLine("\tThere are no members to display.");
                return false;
            }
            else
            {
                for (int i = 0; i < members.Length; ++i)
                    Console.WriteLine("\t" + (i + 1) + ". " + members[i].LastName + ", " + members[i].FirstName);
                return true;
            }
        }
        // creates the user interface for all the tool categories
        public void CreateToolCategories()
        {
            int user_input;
            
            Console.Out.WriteLine("Library System - Tool Library Categories\n");
            Console.Out.WriteLine("==================================================");
            Console.Out.WriteLine("1. Garderning Tools");
            Console.Out.WriteLine("2. FLooring Tools");
            Console.Out.WriteLine("3. Fencing Tools");
            Console.Out.WriteLine("4. Measuring Tool");
            Console.Out.WriteLine("5. Cleaning Tools");
            Console.Out.WriteLine("6. Painting Tools");
            Console.Out.WriteLine("7. Electronic Tools");
            Console.Out.WriteLine("8. Electricity Tools");
            Console.Out.WriteLine("9. Automotive Tools");
            Console.Out.WriteLine("0. Return to Main Menu");
            Console.Out.WriteLine("==================================================\n");
            Console.Out.WriteLine("Select a Tool category: ");

            string input = Console.ReadLine();
            user_input = int.Parse(input);

            switch (user_input)
            {
                case 1:
                    CreateGardeningTools();
                    break;
                case 2:
                    CreateFlooringTools();
                    break;
                case 3:
                    CreateFencingTools();
                    break;
                case 4:
                    CreateMeasuringTools();
                    break;
                case 5:
                    CreateCleaningTools();
                    break;
                case 6:
                    CreatePaintingTools();
                    break;
                case 7:
                    CreateElectronicTools();
                    break;
                case 8:
                    CreateElectricityTools();
                    break;
                case 9:
                    CreateAutomotiveTools();
                    break;
                case 0:
                    Console.WriteLine("Exiting Sytem");
                    Environment.Exit(0);
                    break;
            }
        }
        // creates the user interface for gardening tools
        public int CreateGardeningTools()
        {
            int user_input;

            Console.Out.WriteLine("==================Gardening Tools==================");
            Console.Out.WriteLine("1. Line Trimmers");
            Console.Out.WriteLine("2. Lawn Mowers");
            Console.Out.WriteLine("3. Hand Tools");
            Console.Out.WriteLine("4. Wheelbarrows");
            Console.Out.WriteLine("5. Garden Power Tools");
            Console.Out.WriteLine("==================================================\n");
            Console.Out.WriteLine("Select a Tool Type: ");

            string input = Console.ReadLine();
            user_input = int.Parse(input);

            return user_input;
        }
        // creates the user interface for flooring tools
        public int CreateFlooringTools()
        {
            int user_input;

            Console.Out.WriteLine("==================Flooring Tools================");
            Console.Out.WriteLine("1. Scrapers");
            Console.Out.WriteLine("2. Floor Lasers");
            Console.Out.WriteLine("3. Floor Levelling Tools");
            Console.Out.WriteLine("4. Floor Levelling Materials");
            Console.Out.WriteLine("5. Floor Hand Tools");
            Console.Out.WriteLine("6. Tiling Tools");
            Console.Out.WriteLine("===============================================\n");
            Console.Out.WriteLine("Select a Tool Type: ");

            string input = Console.ReadLine();
            user_input = int.Parse(input);

            return user_input;
        }
        // creates the user interface for fencing tools
        public int CreateFencingTools()
        {
            int user_input;

            Console.Out.WriteLine("=================Fencing Tools==================");
            Console.Out.WriteLine("1. Hand Tools");
            Console.Out.WriteLine("2. Electric Fencing");
            Console.Out.WriteLine("3. Steel Fencing Tools");
            Console.Out.WriteLine("4. Power Tools");
            Console.Out.WriteLine("5. Fencing Accessories");
            Console.Out.WriteLine("===============================================\n");
            Console.Out.WriteLine("Select a Tool Type: ");

            string input = Console.ReadLine();
            user_input = int.Parse(input);

            return user_input;
        }
        // creates the user interface for measuring tools
        public int CreateMeasuringTools()
        {
            int user_input;

            Console.Out.WriteLine("================Measuring Tools=================");
            Console.Out.WriteLine("1. Distance Tools");
            Console.Out.WriteLine("2. Laser Measurer");
            Console.Out.WriteLine("3. Measuring Jugs");
            Console.Out.WriteLine("4. Temperature & Humidity Tools");
            Console.Out.WriteLine("5. Levelling Tools");
            Console.Out.WriteLine("6. Markers");
            Console.Out.WriteLine("===============================================\n");
            Console.Out.WriteLine("Select a Tool Type: ");

            string input = Console.ReadLine();
            user_input = int.Parse(input);

            return user_input;
        }
        // creates the user interface for cleaning tools
        public int CreateCleaningTools()
        {
            int user_input;

            Console.Out.WriteLine("==============Cleaning Tools====================");
            Console.Out.WriteLine("1. Draining");
            Console.Out.WriteLine("2. Car Cleaning");
            Console.Out.WriteLine("3. Vaccuum");
            Console.Out.WriteLine("4. Pressure Cleaners");
            Console.Out.WriteLine("5. Pool Cleaning");
            Console.Out.WriteLine("6. Floor Cleaning");
            Console.Out.WriteLine("===============================================\n");
            Console.Out.WriteLine("Select a Tool Type: ");

            string input = Console.ReadLine();
            user_input = int.Parse(input);

            return user_input;
        }
        // creates the user interface for painting tools
        public int CreatePaintingTools()
        {
            int user_input;

            Console.Out.WriteLine("=================Painting Tools=================");
            Console.Out.WriteLine("1. Sanding Tools");
            Console.Out.WriteLine("2. Brushes");
            Console.Out.WriteLine("3. Rollers");
            Console.Out.WriteLine("4. Paint Removal Tools");
            Console.Out.WriteLine("5. Paint Scrapers");
            Console.Out.WriteLine("6. Sprayers");
            Console.Out.WriteLine("===============================================\n");
            Console.Out.WriteLine("Select a Tool Type: ");

            string input = Console.ReadLine();
            user_input = int.Parse(input);

            return user_input;
        }
        // creates the user interface for electronic tools
        public int CreateElectronicTools()
        {
            int user_input;

            Console.Out.WriteLine("================Electronic Tools================");
            Console.Out.WriteLine("1. Voltage Tester");
            Console.Out.WriteLine("2. Oscilloscopes");
            Console.Out.WriteLine("3. Thermal Imaging");
            Console.Out.WriteLine("4. Data Test Tool");
            Console.Out.WriteLine("5. Insulation Testers");
            Console.Out.WriteLine("===============================================\n");
            Console.Out.WriteLine("Select a Tool Type: ");

            string input = Console.ReadLine();
            user_input = int.Parse(input);

            return user_input;
        }
        // creates the user interface for electricity tools
        public int CreateElectricityTools()
        {
            int user_input;

            Console.Out.WriteLine("===============Electricity Tools================");
            Console.Out.WriteLine("1. Test Equipment");
            Console.Out.WriteLine("2. Safety Equipment");
            Console.Out.WriteLine("3. Basic Hand tools");
            Console.Out.WriteLine("4. Circuit Protection");
            Console.Out.WriteLine("5. Cable Tools");
            Console.Out.WriteLine("===============================================\n");
            Console.Out.WriteLine("Select a Tool Type: ");

            string input = Console.ReadLine();
            user_input = int.Parse(input);

            return user_input;
        }
        // creates the user interface for automotive tools
        public int CreateAutomotiveTools()
        {
            int user_input;

            Console.Out.WriteLine("================Automative Tools================");
            Console.Out.WriteLine("1. Jacks");
            Console.Out.WriteLine("2. Air Compressors");
            Console.Out.WriteLine("3. Battery Chargers");
            Console.Out.WriteLine("4. Socket Tools");
            Console.Out.WriteLine("5. Braking");
            Console.Out.WriteLine("6. Drivetrain");
            Console.Out.WriteLine("===============================================\n");
            Console.Out.WriteLine("Select a Tool Type: ");

            string input = Console.ReadLine();
            user_input = int.Parse(input);

            return user_input;
        }

        
    }
}
