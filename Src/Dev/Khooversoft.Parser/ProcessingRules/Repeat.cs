﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khooversoft.Parser
{
    public class Repeat : AstNodeCollection<IAstNode>, IAstNode
    {
        public Repeat(string name = null)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return $"{nameof(Repeat)}, Count={Count}, Children=({this.ToDelimitedString()})";
        }

        public static Repeat operator +(Repeat rootNode, IAstNode nodeToAdd)
        {
            rootNode.Add(nodeToAdd);
            return rootNode;
        }
    }
}