using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryBuilder
{
    public class PartialJoin
    {
        private string leftTable;
        private string rightTable;
        private string join;

        public PartialJoin(string leftTable, string rightTable, string join)
        {
            this.leftTable = leftTable;
            this.rightTable = rightTable;
            this.join = join;
        }

        public Join on(string leftColumn, string rightColumn)
        {
            Join join = new Join(leftTable, rightTable, this.join, Criteria.equalsColumn(leftColumn, rightColumn));
            return join;
        }

        public Join on(Criteria joinClause)
        {
            Join join = new Join(leftTable, rightTable, this.join, joinClause);
            return join;
        }
    }
}