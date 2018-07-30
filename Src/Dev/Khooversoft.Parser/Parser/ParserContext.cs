﻿using Khooversoft.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khooversoft.Parser
{
    internal class ParserContext
    {
        public ParserContext(IEnumerable<IToken> tokens)
        {
            tokens = tokens ?? throw new ArgumentException(nameof(tokens));
            InputTokens = new CursorList<IToken>(tokens);
        }

        public CursorList<IToken> InputTokens { get; }

        public IReadOnlyList<IAstNode> OutstandingNodes { get; set; }

        public AstNode LastGood { get; set; }
    }
}