using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace QueryBuilder
{
public class QueryBuilder {
	private List<string> projections;
	private List<string> tables;
	private Criteria criterias;
	private List<string> _groupBy;
	private List<string> orderBy;
	private int limit = -1;
	private int _offset = -1;
	private bool distinct = false;
	private List<string> joinParameters = null;
	private List<string> selectParameters = null;
	private List<QueryBuilder> unions;
	private bool isUnionAll = false;

	public QueryBuilder() {
		projections = new List<string>();
		tables = new List<string>();
		_groupBy = new List<string>();
		orderBy = new List<string>();
		unions = new List<QueryBuilder>();
		joinParameters = new List<string>();
		selectParameters = new List<string>();

		criterias = null;
	}

	public QueryBuilder elect(QueryBuilder subQuery, string alias) {
		projections.Add("(" + subQuery.buildQuery() + ") AS " + alias);

		string[] parameters = subQuery.buildParameters();
		for (int i = 0; i < parameters.Length; i++) {
			selectParameters.Add(parameters[i]);
		}

		return this;
	}

	public QueryBuilder select(string projection) {
		projections.Add(projection);
		return this;
	}

	public QueryBuilder select(string projection, string alias) {
		projections.Add(projection + " AS " + alias);
		return this;
	}

	public QueryBuilder select(string projection, string alias, string tableAlias) {
		if (alias == null || alias.Length == 0)
			projections.Add(tableAlias + "." + projection);
		else
			projections.Add(tableAlias + "." + projection + " AS " + alias);

		return this;
	}

	public QueryBuilder selectDistinct(string projection) {
		distinct = true;
		return select(projection);
	}

	public QueryBuilder selectDistinct(string projection, string alias) {
		distinct = true;
		return select(projection, alias);
	}

	public QueryBuilder selectDistinct(string projection, string alias, string tableAlias) {
		distinct = true;
		return select(projection, alias, tableAlias);
	}

	public QueryBuilder from(string table) {
		tables.Add(table);
		return this;
	}

	public QueryBuilder from(string table, string alias) {
		tables.Add(table + " AS " + alias);
		return this;
	}

	public QueryBuilder from(Join join) {
		tables.Add(join.build());

		string[] parameters = join.getParameters();

		if (parameters != null) {
			for (int i = 0; i < parameters.Length; i++) {
				joinParameters.Add(parameters[i]);
			}
		}

		return this;
	}

	public QueryBuilder whereAnd(string criteria, params string[] parameters) {
		if (criterias == null)
			criterias = Criteria.create(criteria, parameters);
		else
			criterias.and(criteria, parameters);

		return this;
	}

	public QueryBuilder whereOr(string criteria, params string[] parameters) {
		if (criterias == null)
			criterias = Criteria.create(criteria, parameters);
		else
			criterias.or(criteria, parameters);

		return this;
	}

	public QueryBuilder whereAnd(SimpleCriteria criteria) {
		if (criteria != null) {
			if (criterias == null)
				criterias = Criteria.create(criteria);
			else
				criterias.and(criteria);
		}

		return this;
	}

	public QueryBuilder whereOr(SimpleCriteria criteria) {
		if (criteria != null) {
			if (criterias == null)
				criterias = Criteria.create(criteria);
			else
				criterias.or(criteria);
		}

		return this;
	}

	public QueryBuilder union(QueryBuilder unionQuery) {
		unionQuery.isUnionAll = false;
		unions.Add(unionQuery);

		return this;
	}

	public QueryBuilder unionAll(QueryBuilder unionQuery) {
		unionQuery.isUnionAll = true;
		unions.Add(unionQuery);

		return this;
	}

	public QueryBuilder groupBy(string group) {
		_groupBy.Add(group);
		return this;
	}

	public QueryBuilder orderByAscending(string order) {
		orderBy.Add(order + " ASC");
		return this;
	}
	
	public QueryBuilder orderByAscendingIgnoreCase(string order) {
		orderBy.Add(order + " COLLATE NOCASE ASC");
		return this;
	}

	public QueryBuilder orderByDescending(string order) {
		orderBy.Add(order + " DESC");
		return this;
	}
	
	public QueryBuilder orderByDescendingIgnoreCase(string order) {
		orderBy.Add(order + " COLLATE NOCASE DESC");
		return this;
	}

	public QueryBuilder offset(int offset) {
		this._offset = offset;
		return this;
	}

	public QueryBuilder Limit(int limit) {
		this.limit = limit;
		return this;
	}

	public string buildQuery() {
		bool firstItem;

		StringBuilder sb = new StringBuilder();
		sb.Append("SELECT ");

		if (distinct)
			sb.Append("DISTINCT ");

		if (projections.Count() > 0) {
			firstItem = true;
			foreach (string projection in projections) {
				if (firstItem)
					firstItem = false;
				else
					sb.Append(", ");

				sb.Append(projection);
			}
		} else {
			sb.Append("*");
		}

		if(tables.Count() > 0) {
			sb.Append(" FROM ");
		
			firstItem = true;
			foreach (string table in tables) {
				if (firstItem)
					firstItem = false;
				else
					sb.Append(", ");
		
				sb.Append(table);
			}
		}

		if (criterias != null) {
			sb.Append(" WHERE ");
			sb.Append(criterias.build());
		}

		if (_groupBy.Count() > 0) {
			sb.Append(" GROUP BY ");

			firstItem = true;
			foreach (string group in _groupBy) {
				if (firstItem)
					firstItem = false;
				else
					sb.Append(", ");

				sb.Append(group);
			}
		}

		if (orderBy.Count() > 0) {
			sb.Append(" ORDER BY ");

			firstItem = true;
			foreach (string order in orderBy) {
				if (firstItem)
					firstItem = false;
				else
					sb.Append(", ");

				sb.Append(order);
			}
		}

		if (limit > 0) {
			sb.Append(" LIMIT ");
			sb.Append(limit);
		}

		if (_offset > 0) {
			sb.Append(" OFFSET ");
			sb.Append(_offset);
		}

		foreach (QueryBuilder union in unions) {
			if (union.isUnionAll)
				sb.Append(" UNION ALL ");
			else
				sb.Append(" UNION ");

			sb.Append(union.buildQuery());
		}

		// SOME DATABASES ALLOWS ONLY 999 PARAMETER IN A QUERY.
		// THIS PIECE OF CODE REMOVES PARAMETERS THAT EXCEED THIS LIMIT
		// AND REPLACES THEM FOR THE VALUE IN THE GENERATED QUERY STRING
		List<string> parameters = getParametersCollection();
		int parametersSize = parameters.Count();
		int charIndex;

        var stringQuery = sb.ToString();
		while (parametersSize > 999) {
			charIndex = stringQuery.LastIndexOf("?");
			stringQuery.Remove(charIndex, 1);
            stringQuery.Insert(charIndex,  parameters.ElementAt(parametersSize - 1).Replace("'", "''")); // DatabaseUtils.sqlEscapeString(parameters.ElementAt(parametersSize - 1)));
			parametersSize--;
		}

		return stringQuery;
	}

	public string[] buildParameters() {
		List<string> parameters = getParametersCollection();

		if (parameters.Count() > 0) {

			// Removes some parameters to keep the 
			// collection within the 999 parameters limit
			while (parameters.Count() > 999)
				parameters.RemoveAt(parameters.Count() - 1);

			return parameters.ToArray();
		} else
			return null;
	}
	
	public string[] buildParametersDefaultEmpty() {
		string[] parameters = buildParameters();
		parameters = (parameters != null ? parameters : new string[0]);
		
		return parameters;
	}

	public Criteria getCriteria() {
		return criterias;
	}

	private List<string> getParametersCollection() {
		// Join every parameter needed for the query in a single collection
		// taking into account the order of the parameters
		
		List<string> parameters = new List<string>();

		if (selectParameters != null)
			parameters.AddRange(selectParameters);

		if (joinParameters != null)
			parameters.AddRange(joinParameters);

		string[] criteriasParams = (criterias == null ? new string[0] : criterias.getParameters());
		criteriasParams = (criteriasParams == null ? new string[0] : criteriasParams);
		for (int i = 0; i < criteriasParams.Length; i++) {
			parameters.Add(criteriasParams[i]);
		}

		string[] unionParams;
		foreach (QueryBuilder union in unions) {
			unionParams = union.buildParameters();

			if (unionParams != null) {
				for (int i = 0; i < unionParams.Length; i++) {
					parameters.Add(unionParams[i]);
				}
			}
		}

		return parameters;
	}

	public string toDebugSqlString() {
		string[] parameters = buildParameters();
		string sqlString = buildQuery();
		
		if (parameters != null) {
			foreach (string par in parameters) {
                var regex = new Regex(Regex.Escape("\\?"));
                var newText = regex.Replace(sqlString, par.Replace("'", "''"), 1);                
			}
		}
		
		return sqlString;
	}
}
}