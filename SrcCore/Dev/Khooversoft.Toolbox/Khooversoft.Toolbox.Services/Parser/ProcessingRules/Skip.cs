﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khooversoft.Toolbox.Parser
{
    public class Skip<T> : NodeCollection<INode>, INode
    {
        public Skip(bool supportNested = true, string name = null)
        {
            SupportNested = supportNested;
            Name = name;
        }

        public bool SupportNested { get; }

        public string Name { get; }

        public override string ToString()
        {
            return $"{nameof(Skip<T>)}, SupportNested={SupportNested}, Count={Count}, Children=({this.ToDelimitedString()})";
        }

        public static Skip<T> operator +(Skip<T> rootNode, INode nodeToAdd)
        {
            rootNode.Add(nodeToAdd);
            return rootNode;
        }
    }
}
