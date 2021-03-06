﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Migraine.Core.Visitors;

namespace Migraine.Core.Nodes
{
    public class IfStatementNode : Node
    {
        public ConditionNode Condition { get; private set; }
        public BlockNode Body { get; private set; }

        public IfStatementNode(ConditionNode condition, BlockNode body)
        {
            Condition = condition;
            Body = body;
        }

        public override TReturn Accept<TReturn>(IMigraineAstVisitor<TReturn> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
