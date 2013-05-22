#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file Extensions.cs is part of CInject.
// 
// CInject is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// 
// CInject is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along with CInject. If not, see http://www.gnu.org/licenses/.
// 
// History:
// ______________________________________________________________
// Created        09-2011              Punit Ganshani

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CInject.Extensions
{
    public static class Extensions
    {
        public static List<T> GetCheckedNodes<T>(this TreeNode root, Func<T, bool> predicate)
        {
            List<T> items = root.GetCheckedNodes<T>();

            return items.Where(predicate).ToList();
        }

        public static List<T> GetCheckedNodes<T>(this TreeNode root)
        {
            var types = new List<T>();

            if (root.Checked && root.Tag != null)
            {
                if (root.Tag is T) // except for first node
                {
                    var item = (T) root.Tag;
                    types.Add(item);
                }
            }

            // check for nodes in the root node
            for (int i = 0; i < root.Nodes.Count; i++)
            {
                TreeNode currentNode = root.Nodes[i];

                if (currentNode.Checked && currentNode.Tag != null)
                {
                    if (currentNode.Tag is T) // except for first node
                    {
                        var item = (T) currentNode.Tag;
                        types.Add(item);
                    }
                }

                for (int x = 0; x < currentNode.Nodes.Count; x++)
                {
                    types.AddRange(currentNode.Nodes[x].GetCheckedNodes<T>());
                }
            }

            return types;
        }

        public static List<T> GetNodes<T>(this TreeNode root, Func<T, bool> predicate)
        {
            List<T> items = root.GetNodes<T>();

            return items.Where(predicate).ToList();
        }

        public static List<T> GetNodes<T>(this TreeNode root)
        {
            var types = new List<T>();

            if (root.Tag != null)
            {
                if (root.Tag is T) // except for first node
                {
                    var item = (T) root.Tag;
                    types.Add(item);
                }
            }

            // check for nodes in the root node
            for (int i = 0; i < root.Nodes.Count; i++)
            {
                TreeNode currentNode = root.Nodes[i];

                if (currentNode.Tag != null)
                {
                    if (currentNode.Tag is T) // except for first node
                    {
                        var item = (T) currentNode.Tag;
                        types.Add(item);
                    }
                }

                for (int x = 0; x < currentNode.Nodes.Count; x++)
                {
                    types.AddRange(currentNode.Nodes[x].GetNodes<T>());
                }
            }

            return types;
        }

        // Updates all child tree nodes recursively.
        public static void CheckAllChildNodes(this TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    node.CheckAllChildNodes(nodeChecked);
                }
            }
        }

        public static void CheckParentNode(this TreeNode treeNode, bool nodeChecked)
        {
            if (treeNode.Parent != null)
            {
                if (treeNode.Parent.Nodes.Count == 1)
                {
                    treeNode.Parent.Checked = nodeChecked;
                }
                else
                {
                    bool childNodeState = nodeChecked;
                    foreach (TreeNode node in treeNode.Parent.Nodes)
                    {
                        childNodeState &= node.Checked;
                    }

                    treeNode.Parent.Checked = childNodeState;
                }
            }
        }
    }
}