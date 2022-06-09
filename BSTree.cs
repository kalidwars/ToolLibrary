// File: BTree.cs
// An implementation of BTree ADT
// Maolin Tang 
// 24/3/06

using System;
using System.Collections.Generic;
namespace Assignment
{
	class BTreeNode
	{
		private Member member; // value
		private BTreeNode lchild; // reference to its left child 
		private BTreeNode rchild; // reference to its right child

		public BTreeNode(Member member)
		{
			this.member = member;
			lchild = null;
			rchild = null;
		}

		public Member Member
		{
			get { return member; }
			set { member = value; }
		}

		public BTreeNode LChild
		{
			get { return lchild; }
			set { lchild = value; }
		}

		public BTreeNode RChild
		{
			get { return rchild; }
			set { rchild = value; }
		}
	}


	public class BSTree
	{
		private BTreeNode root;


		public BSTree()
		{
			root = null;
		}

		public bool IsEmpty()
		{
			return root == null;
		}

		public bool Search(Member member)
		{
			return Search(member, root);
		}

		private bool Search(Member member, BTreeNode r)
		{
			if(r != null)
			{
				if(member.CompareTo(r.Member) == 0)
					return true;
				else
					if(member.CompareTo(r.Member) < 0 )
					return Search(member, r.LChild);
				else
					return Search(member, r.RChild);
			}
			else
				return false;
		}

		public void Insert(Member member)
		{
			if(root == null)
				root = new BTreeNode(member);
			else
				Insert(member, root);
		}

		// pre: ptr != null
		// post: item is inserted to the binary search tree rooted at ptr
		private void Insert (Member member, BTreeNode ptr)
		{
			if (member.CompareTo(ptr.Member) < 0)
			{
				if (ptr.LChild == null)
					ptr.LChild = new BTreeNode(member);
				else 
					Insert(member, ptr.LChild);
			}
			else 
			{
				if (ptr.RChild == null)			
					ptr.RChild = new BTreeNode(member);
				else 
					Insert(member, ptr.RChild);
			}
		} 
	
		// there are three cases to consider:
		// 1. the node to be deleted is a leaf
		// 2. the node to be deleted has only one child 
		// 3. the node to be deleted has both left and right children
		public void Delete(Member member)
		{
			// search for item and its parent
			BTreeNode ptr = root; // search reference
			BTreeNode parent = null; // parent of ptr
			while((ptr!=null)&&(member.CompareTo(ptr.Member)!=0))
			{
				parent = ptr;
				if(member.CompareTo(ptr.Member) < 0) // move to the left child of ptr
					ptr = ptr.LChild;
				else
					ptr = ptr.RChild;
			}

			if(ptr != null) // if the search was successful
			{
				// case 3: item has two children
				if((ptr.LChild != null)&&(ptr.RChild != null))
				{
					// find the right-most node in left subtree of ptr
					if(ptr.LChild.RChild == null) // a special case: the right subtree of ptr.LChild is empty
					{
						ptr.Member = ptr.LChild.Member;
						ptr.LChild = ptr.LChild.LChild;
					}
					else 
					{
						BTreeNode p = ptr.LChild;
						BTreeNode pp = ptr; // parent of p
						while(p.RChild != null)
						{
							pp = p;
							p = p.RChild;
						}
						// copy the item at p to ptr
						ptr.Member = p.Member;
						pp.RChild = p.LChild;
					}
				}
				else // cases 1 & 2: item has no or only one child
				{
					BTreeNode c;
					if(ptr.LChild != null)
						c = ptr.LChild;
					else
						c = ptr.RChild;

					// remove node ptr
					if(ptr == root) //need to change root
						root = c;
					else
					{
						if(ptr == parent.LChild)
							parent.LChild = c;
						else
							parent.RChild = c;
					}
				}

			}
		}

		public void PreOrderTraverse()
		{
			Console.Write("PreOrder: ");
			PreOrderTraverse(root);
			Console.WriteLine();
		}

		private void PreOrderTraverse(BTreeNode root)
		{
			if(root != null)
			{
				Console.Write(root.Member);
				PreOrderTraverse(root.LChild);
				PreOrderTraverse(root.RChild);
			}
		}



		private void InorderToArray(BTreeNode root, List<Member> array)
		{
			if (root == null) return;

			InorderToArray(root.LChild, array);
			array.Add(root.Member);
			InorderToArray(root.RChild, array);
		}

		public Member[] toArrayBST
		{
			//int counter = size;
			//Member[] final = new Member[counter];
			//         InOrderTraverse(root, final, 0);
			//return final;

			get
			{
				List<Member> membersList = new List<Member>();
				InorderToArray(root, membersList);
				return membersList.ToArray();
			}
		}
	

	public void InOrderTraverse()
		{
			InOrderTraverse(root);
		}

		private void InOrderTraverse(BTreeNode root)
		{
			if(root != null)
			{
				InOrderTraverse(root.LChild);
				Console.Write(root.Member);
				InOrderTraverse(root.RChild);
			}
		}

		public void PostOrderTraverse()
		{
			Console.Write("PostOrder: ");
			PostOrderTraverse(root);
			Console.WriteLine();
		}

		private void PostOrderTraverse(BTreeNode root)
		{
			if(root != null)
			{
				PostOrderTraverse(root.LChild);
				PostOrderTraverse(root.RChild);
				Console.Write(root.Member); 
			}
		}

		public void Clear()
		{
			root = null;
		}

	}
}




