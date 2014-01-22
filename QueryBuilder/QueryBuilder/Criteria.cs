using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace QueryBuilder
{
    public class Criteria : SimpleCriteria
    {
        private List<SimpleCriteria> _criterias;

        private Criteria()
            : base()
        {

            _criterias = new List<SimpleCriteria>();
        }

        public static Criteria create(string criteria, string[] parameters)
        {
            SimpleCriteria criteriaObj = new SimpleCriteria(criteria, TypeCriteria.AND, parameters);
            return create(criteriaObj);
        }

        public static Criteria create(SimpleCriteria criteria)
        {
            Criteria cg = new Criteria();
            cg._criterias.Add(criteria);

            return cg;
        }


        // isNull
        public static Criteria isNull(string column)
        {
            return create(column + " IS NULL", null);
        }

        public static Criteria notIsNull(string column)
        {
            return create(column + " IS NOT NULL", null);
        }

        public static Criteria isNull(QueryBuilder subQuery)
        {
            return create("(" + subQuery.buildQuery() + ") IS NULL", subQuery.buildParameters());
        }

        public static Criteria notIsNull(QueryBuilder subQuery)
        {
            return create("(" + subQuery.buildQuery() + ") IS NOT NULL", subQuery.buildParameters());
        }


        // string
        public static Criteria equals(string column, string value)
        {
            return create(column + " = ?", new string[] { value });
        }

        public static Criteria notEquals(string column, string value)
        {
            return create(column + " <> ?", new string[] { value });
        }

        public static Criteria greaterThan(string column, string value)
        {
            return create(column + " > ?", new string[] { value });
        }

        public static Criteria lesserThan(string column, string value)
        {
            return create(column + " < ?", new string[] { value });
        }

        public static Criteria greaterThanOrEqual(string column, string value)
        {
            return create(column + " >= ?", new string[] { value });
        }

        public static Criteria lesserThanOrEqual(string column, string value)
        {
            return create(column + " <= ?", new string[] { value });
        }

        public static Criteria startsWith(string column, string value)
        {
            return create(column + " LIKE ?", new string[] { value + "%" });
        }

        public static Criteria notStartsWith(string column, string value)
        {
            return create(column + " NOT LIKE ?", new string[] { value + "%" });
        }

        public static Criteria endsWith(string column, string value)
        {
            return create(column + " LIKE ?", new string[] { "%" + value });
        }

        public static Criteria notEndsWith(string column, string value)
        {
            return create(column + " NOT LIKE ?", new string[] { "%" + value });
        }

        public static Criteria contains(string column, string value)
        {
            return create(column + " LIKE ?", new string[] { "%" + value + "%" });
        }

        public static Criteria notContains(string column, string value)
        {
            return create(column + " NOT LIKE ?", new string[] { "%" + value + "%" });
        }

        public static Criteria between(string column, string valueMin, string valueMax)
        {
            return create(column + " BETWEEN ? AND ?", new string[] { valueMin, valueMax });
        }

        public static Criteria valueBetween(string value, string columnMin, string columnMax)
        {
            return create(" ? BETWEEN " + columnMin + " AND " + columnMax, new string[] { value });
        }


        // SubQuery + string
        public static Criteria equals(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("(" + subQuery.buildQuery() + ") = ?", parameters);
        }

        public static Criteria notEquals(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("(" + subQuery.buildQuery() + ") <> ?", parameters);
        }

        public static Criteria greaterThan(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("(" + subQuery.buildQuery() + ") > ?", parameters);
        }

        public static Criteria lesserThan(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("(" + subQuery.buildQuery() + ") < ?", parameters);
        }

        public static Criteria greaterThanOrEqual(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("(" + subQuery.buildQuery() + ") >= ?", parameters);
        }

        public static Criteria lesserThanOrEqual(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("(" + subQuery.buildQuery() + ") <= ?", parameters);
        }

        public static Criteria startsWith(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value + "%";

            return create("(" + subQuery.buildQuery() + ") LIKE ?", parameters);
        }

        public static Criteria notStartsWith(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value + "%";

            return create("(" + subQuery.buildQuery() + ") NOT LIKE ?", parameters);
        }

        public static Criteria endsWith(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = "%" + value;

            return create("(" + subQuery.buildQuery() + ") LIKE ?", parameters);
        }

        public static Criteria notEndsWith(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = "%" + value;

            return create("(" + subQuery.buildQuery() + ") NOT LIKE ?", parameters);
        }

        public static Criteria contains(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParametersDefaultEmpty();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = "%" + value + "%";

            return create("(" + subQuery.buildQuery() + ") LIKE ?", parameters);
        }

        public static Criteria notContains(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParametersDefaultEmpty();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = "%" + value + "%";

            return create("(" + subQuery.buildQuery() + ") NOT LIKE ?", parameters);
        }

        public static Criteria between(QueryBuilder subQuery, string valueMin, string valueMax)
        {
            string[] subQueryParams = subQuery.buildParametersDefaultEmpty();
            string[] parameters = new string[subQueryParams.Length + 2];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 2] = valueMin;
            parameters[parameters.Length - 1] = valueMax;

            return create("(" + subQuery.buildQuery() + ") BETWEEN ? AND ?", parameters);
        }


        // Integer
        public static Criteria equals(string column, int value)
        {
            return create(column + " = ?", new string[] { value.ToString() });
        }

        public static Criteria notEquals(string column, int value)
        {
            return create(column + " <> ?", new string[] { value.ToString() });
        }

        public static Criteria greaterThan(string column, int value)
        {
            return create("CAST(" + column + " AS INTEGER) > ?", new string[] { value.ToString() });
        }

        public static Criteria lesserThan(string column, int value)
        {
            return create("CAST(" + column + " AS INTEGER) < ?", new string[] { value.ToString() });
        }

        public static Criteria greaterThanOrEqual(string column, int value)
        {
            return create("CAST(" + column + " AS INTEGER) >= ?", new string[] { value.ToString() });
        }

        public static Criteria lesserThanOrEqual(string column, int value)
        {
            return create("CAST(" + column + " AS INTEGER) <= ?", new string[] { value.ToString() });
        }

        public static Criteria between(string column, int valueMin, int valueMax)
        {
            return create(column + " BETWEEN ? AND ?", new string[] { valueMin.ToString(), valueMax.ToString() });
        }

        public static Criteria valueBetween(int value, string columnMin, string columnMax)
        {
            return create(" ? BETWEEN " + columnMin + " AND " + columnMax, new string[] { value.ToString() });
        }


        // SubQuery + Integer
        public static Criteria equals(QueryBuilder subQuery, int value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value.ToString();

            return create("(" + subQuery.buildQuery() + ") = ?", parameters);
        }

        public static Criteria notEquals(QueryBuilder subQuery, int value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value.ToString();

            return create("(" + subQuery.buildQuery() + ") <> ?", parameters);
        }

        public static Criteria greaterThan(QueryBuilder subQuery, int value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value.ToString();

            return create("(" + subQuery.buildQuery() + ") > ?", parameters);
        }

        public static Criteria lesserThan(QueryBuilder subQuery, int value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value.ToString();

            return create("(" + subQuery.buildQuery() + ") < ?", parameters);
        }

        public static Criteria greaterThanOrEqual(QueryBuilder subQuery, int value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value.ToString();

            return create("(" + subQuery.buildQuery() + ") >= ?", parameters);
        }

        public static Criteria lesserThanOrEqual(QueryBuilder subQuery, int value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value.ToString();

            return create("(" + subQuery.buildQuery() + ") <= ?", parameters);
        }

        public static Criteria between(QueryBuilder subQuery, int valueMin, int valueMax)
        {
            string[] subQueryParams = subQuery.buildParametersDefaultEmpty();
            string[] parameters = new string[subQueryParams.Length + 2];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 2] = valueMin.ToString();
            parameters[parameters.Length - 1] = valueMax.ToString();

            return create("(" + subQuery.buildQuery() + ") BETWEEN ? AND ?", parameters);
        }


        // Float
        public static Criteria equals(string column, float value)
        {
            return create(column + " = ?", new string[] { Utils.toString(value) });
        }

        public static Criteria notEquals(string column, float value)
        {
            return create(column + " <> ?", new string[] { Utils.toString(value) });
        }

        public static Criteria greaterThan(string column, float value)
        {
            return create("CAST(" + column + " AS REAL) > ?", new string[] { Utils.toString(value) });
        }

        public static Criteria lesserThan(string column, float value)
        {
            return create("CAST(" + column + " AS REAL) < ?", new string[] { Utils.toString(value) });
        }

        public static Criteria greaterThanOrEqual(string column, float value)
        {
            return create("CAST(" + column + " AS REAL) >= ?", new string[] { Utils.toString(value) });
        }

        public static Criteria lesserThanOrEqual(string column, float value)
        {
            return create("CAST(" + column + " AS REAL) <= ?", new string[] { Utils.toString(value) });
        }

        public static Criteria between(string column, float valueMin, float valueMax)
        {
            return create(column + " BETWEEN ? AND ?", new string[] { Utils.toString(valueMin), Utils.toString(valueMax) });
        }

        public static Criteria valueBetween(float value, string columnMin, string columnMax)
        {
            return create(" ? BETWEEN " + columnMin + " AND " + columnMax, new string[] { Utils.toString(value) });
        }


        // SubQuery + Float
        public static Criteria equals(QueryBuilder subQuery, float value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = Utils.toString(value);

            return create("(" + subQuery.buildQuery() + ") = ?", parameters);
        }

        public static Criteria notEquals(QueryBuilder subQuery, float value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = Utils.toString(value);

            return create("(" + subQuery.buildQuery() + ") <> ?", parameters);
        }

        public static Criteria greaterThan(QueryBuilder subQuery, float value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = Utils.toString(value);

            return create("(" + subQuery.buildQuery() + ") > ?", parameters);
        }

        public static Criteria lesserThan(QueryBuilder subQuery, float value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = Utils.toString(value);

            return create("(" + subQuery.buildQuery() + ") < ?", parameters);
        }

        public static Criteria greaterThanOrEqual(QueryBuilder subQuery, float value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = Utils.toString(value);

            return create("(" + subQuery.buildQuery() + ") >= ?", parameters);
        }

        public static Criteria lesserThanOrEqual(QueryBuilder subQuery, float value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = Utils.toString(value);

            return create("(" + subQuery.buildQuery() + ") <= ?", parameters);
        }

        public static Criteria between(QueryBuilder subQuery, float valueMin, float valueMax)
        {
            string[] subQueryParams = subQuery.buildParametersDefaultEmpty();
            string[] parameters = new string[subQueryParams.Length + 2];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 2] = Utils.toString(valueMin);
            parameters[parameters.Length - 1] = Utils.toString(valueMax);

            return create("(" + subQuery.buildQuery() + ") BETWEEN ? AND ?", parameters);
        }


        // Double
        public static Criteria equals(string column, double value)
        {
            return create(column + " = ?", new string[] { Utils.toString(value) });
        }

        public static Criteria notEquals(string column, double value)
        {
            return create(column + " <> ?", new string[] { Utils.toString(value) });
        }

        public static Criteria greaterThan(string column, double value)
        {
            return create("CAST(" + column + " AS REAL) > ?", new string[] { Utils.toString(value) });
        }

        public static Criteria lesserThan(string column, double value)
        {
            return create("CAST(" + column + " AS REAL) < ?", new string[] { Utils.toString(value) });
        }

        public static Criteria greaterThanOrEqual(string column, double value)
        {
            return create("CAST(" + column + " AS REAL) >= ?", new string[] { Utils.toString(value) });
        }

        public static Criteria lesserThanOrEqual(string column, double value)
        {
            return create("CAST(" + column + " AS REAL) <= ?", new string[] { Utils.toString(value) });
        }

        public static Criteria between(string column, double valueMin, double valueMax)
        {
            return create(column + " BETWEEN ? AND ?", new string[] { Utils.toString(valueMin), Utils.toString(valueMax) });
        }

        public static Criteria valueBetween(double value, string columnMin, string columnMax)
        {
            return create(" ? BETWEEN " + columnMin + " AND " + columnMax, new string[] { Utils.toString(value) });
        }


        // SubQuery + Double
        public static Criteria equals(QueryBuilder subQuery, double value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = Utils.toString(value);

            return create("(" + subQuery.buildQuery() + ") = ?", parameters);
        }

        public static Criteria notEquals(QueryBuilder subQuery, double value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = Utils.toString(value);

            return create("(" + subQuery.buildQuery() + ") <> ?", parameters);
        }

        public static Criteria greaterThan(QueryBuilder subQuery, double value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = Utils.toString(value);

            return create("(" + subQuery.buildQuery() + ") > ?", parameters);
        }

        public static Criteria lesserThan(QueryBuilder subQuery, double value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = Utils.toString(value);

            return create("(" + subQuery.buildQuery() + ") < ?", parameters);
        }

        public static Criteria greaterThanOrEqual(QueryBuilder subQuery, double value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = Utils.toString(value);

            return create("(" + subQuery.buildQuery() + ") >= ?", parameters);
        }

        public static Criteria lesserThanOrEqual(QueryBuilder subQuery, double value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = Utils.toString(value);

            return create("(" + subQuery.buildQuery() + ") <= ?", parameters);
        }

        public static Criteria between(QueryBuilder subQuery, double valueMin, double valueMax)
        {
            string[] subQueryParams = subQuery.buildParametersDefaultEmpty();
            string[] parameters = new string[subQueryParams.Length + 2];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 2] = Utils.toString(valueMin);
            parameters[parameters.Length - 1] = Utils.toString(valueMax);

            return create("(" + subQuery.buildQuery() + ") BETWEEN ? AND ?", parameters);
        }


        // LocalDate
        public static Criteria equals(string column, DateTime value, bool includeTime = true)
        {
            if (includeTime)
                return create("DATETIME(" + column + ") = ?", new string[] { Utils.dateTimeToString(value) });
            else
                return create("DATE(" + column + ") = ?", new string[] { Utils.dateToString(value) });
        }

        public static Criteria notEquals(string column, DateTime value, bool includeTime = true)
        {
            if (includeTime)
                return create("DATETIME(" + column + ") <> ?", new string[] { Utils.dateTimeToString(value) });
            else
                return create("DATE(" + column + ") <> ?", new string[] { Utils.dateToString(value) });
        }

        public static Criteria greaterThan(string column, DateTime value, bool includeTime = true)
        {
            if (includeTime)
                return create("DATETIME(" + column + ") > ?", new string[] { Utils.dateTimeToString(value) });
            else
                return create("DATE(" + column + ") > ?", new string[] { Utils.dateToString(value) });
        }

        public static Criteria lesserThan(string column, DateTime value, bool includeTime = true)
        {
            if (includeTime)
                return create("DATETIME(" + column + ") < ?", new string[] { Utils.dateTimeToString(value) });
            else
                return create("DATE(" + column + ") < ?", new string[] { Utils.dateToString(value) });
        }

        public static Criteria greaterThanOrEqual(string column, DateTime value, bool includeTime = true)
        {
            if (includeTime)
                return create("DATETIME(" + column + ") >= ?", new string[] { Utils.dateTimeToString(value) });
            else
                return create("DATE(" + column + ") >= ?", new string[] { Utils.dateToString(value) });
        }

        public static Criteria lesserThanOrEqual(string column, DateTime value, bool includeTime = true)
        {
            if (includeTime)
                return create("DATETIME(" + column + ") <= ?", new string[] { Utils.dateTimeToString(value) });
            else
                return create("DATE(" + column + ") <= ?", new string[] { Utils.dateToString(value) });
        }

        public static Criteria between(string column, DateTime valueMin, DateTime valueMax, bool includeTime = true)
        {
            if (includeTime)
                return create("DATETIME(" + column + ") BETWEEN ? AND ?", new string[] { Utils.dateTimeToString(valueMin), Utils.dateTimeToString(valueMax) });
            else
                return create("DATE(" + column + ") BETWEEN ? AND ?", new string[] { Utils.dateToString(valueMin), Utils.dateToString(valueMax) });
        }

        public static Criteria valueBetween(DateTime value, string columnMin, string columnMax, bool includeTime = true)
        {
            if (includeTime)
                return create(" ? BETWEEN " + columnMin + " AND " + columnMax, new string[] { Utils.dateTimeToString(value) });
            else
                return create(" ? BETWEEN DATE(" + columnMin + ") AND DATE(" + columnMax + ")", new string[] { Utils.dateToString(value) });
        }


        // SubQuery + LocalDate
        public static Criteria equals(QueryBuilder subQuery, DateTime value, bool includeTime = true)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = includeTime ? Utils.dateTimeToString(value) : Utils.dateToString(value);

            return create("(" + subQuery.buildQuery() + ") = ?", parameters);
        }

        public static Criteria notEquals(QueryBuilder subQuery, DateTime value, bool includeTime = true)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = includeTime ? Utils.dateTimeToString(value) : Utils.dateToString(value);

            return create("(" + subQuery.buildQuery() + ") <> ?", parameters);
        }

        public static Criteria greaterThan(QueryBuilder subQuery, DateTime value, bool includeTime = true)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = includeTime ? Utils.dateTimeToString(value) : Utils.dateToString(value);

            return create("(" + subQuery.buildQuery() + ") > ?", parameters);
        }

        public static Criteria lesserThan(QueryBuilder subQuery, DateTime value, bool includeTime = true)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = includeTime ? Utils.dateTimeToString(value) : Utils.dateToString(value);

            return create("(" + subQuery.buildQuery() + ") < ?", parameters);
        }

        public static Criteria greaterThanOrEqual(QueryBuilder subQuery, DateTime value, bool includeTime = true)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = includeTime ? Utils.dateTimeToString(value) : Utils.dateToString(value);

            return create("(" + subQuery.buildQuery() + ") >= ?", parameters);
        }

        public static Criteria lesserThanOrEqual(QueryBuilder subQuery, DateTime value, bool includeTime = true)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = includeTime ? Utils.dateTimeToString(value) : Utils.dateToString(value);

            return create("(" + subQuery.buildQuery() + ") <= ?", parameters);
        }

        public static Criteria between(QueryBuilder subQuery, DateTime valueMin, DateTime valueMax, bool includeTime = true)
        {
            string[] subQueryParams = subQuery.buildParametersDefaultEmpty();
            string[] parameters = new string[subQueryParams.Length + 2];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 2] = includeTime ? Utils.dateTimeToString(valueMin) : Utils.dateToString(valueMin);
            parameters[parameters.Length - 1] = includeTime ? Utils.dateTimeToString(valueMax) : Utils.dateToString(valueMax);            

            return create("(" + subQuery.buildQuery() + ") BETWEEN ? AND ?", parameters);
        }

        // DATE CAST
        public static Criteria equalsAsDate(string column, string value)
        {
            return create("DATE(" + column + ") = ?", new string[] { value });
        }

        public static Criteria notEqualsAsDate(string column, string value)
        {
            return create("DATE(" + column + ") <> ?", new string[] { value });
        }

        public static Criteria greaterThanAsDate(string column, string value)
        {
            return create("DATE(" + column + ") > ?", new string[] { value });
        }

        public static Criteria lesserThanAsDate(string column, string value)
        {
            return create("DATE(" + column + ") < ?", new string[] { value });
        }

        public static Criteria greaterThanOrEqualAsDate(string column, string value)
        {
            return create("DATE(" + column + ") >= ?", new string[] { value });
        }

        public static Criteria lesserThanOrEqualAsDate(string column, string value)
        {
            return create("DATE(" + column + ") <= ?", new string[] { value });
        }

        public static Criteria betweenAsDate(string column, string valueMin, string valueMax)
        {
            return create("DATE(" + column + ") BETWEEN ? AND ?", new string[] { valueMin, valueMax });
        }

        public static Criteria valueBetweenAsDate(string value, string columnMin, string columnMax)
        {
            return create(" ? BETWEEN DATE(" + columnMin + ") AND DATE(" + columnMax + ")", new string[] { value });
        }


        // SubQuery + DATE CAST
        public static Criteria equalsAsDate(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("DATE((" + subQuery.buildQuery() + ")) = ?", parameters);
        }

        public static Criteria notEqualsAsDate(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("DATE((" + subQuery.buildQuery() + ")) <> ?", parameters);
        }

        public static Criteria greaterThanAsDate(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("DATE((" + subQuery.buildQuery() + ")) > ?", parameters);
        }

        public static Criteria lesserThanAsDate(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("DATE((" + subQuery.buildQuery() + ")) < ?", parameters);
        }

        public static Criteria greaterThanOrEqualAsDate(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("DATE((" + subQuery.buildQuery() + ")) >= ?", parameters);
        }

        public static Criteria lesserThanOrEqualAsDate(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("DATE((" + subQuery.buildQuery() + ")) <= ?", parameters);
        }

        public static Criteria betweenAsDate(QueryBuilder subQuery, string valueMin, string valueMax)
        {
            string[] subQueryParams = subQuery.buildParametersDefaultEmpty();
            string[] parameters = new string[subQueryParams.Length + 2];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 2] = valueMin;
            parameters[parameters.Length - 1] = valueMax;

            return create("DATE((" + subQuery.buildQuery() + ")) BETWEEN ? AND ?", parameters);
        }


        // DATETIME CAST
        public static Criteria equalsAsDateTime(string column, string value)
        {
            return create("DATETIME(" + column + ") = ?", new string[] { value });
        }

        public static Criteria notEqualsAsDateTime(string column, string value)
        {
            return create("DATETIME(" + column + ") <> ?", new string[] { value });
        }

        public static Criteria greaterThanAsDateTime(string column, string value)
        {
            return create("DATETIME(" + column + ") > ?", new string[] { value });
        }

        public static Criteria lesserThanAsDateTime(string column, string value)
        {
            return create("DATETIME(" + column + ") < ?", new string[] { value });
        }

        public static Criteria greaterThanOrEqualAsDateTime(string column, string value)
        {
            return create("DATETIME(" + column + ") >= ?", new string[] { value });
        }

        public static Criteria lesserThanOrEqualAsDateTime(string column, string value)
        {
            return create("DATETIME(" + column + ") <= ?", new string[] { value });
        }

        public static Criteria betweenAsDateTime(string column, string valueMin, string valueMax)
        {
            return create("DATETIME(" + column + ") BETWEEN ? AND ?", new string[] { valueMin, valueMax });
        }

        public static Criteria valueBetweenAsDateTime(string value, string columnMin, string columnMax)
        {
            return create(" ? BETWEEN DATETIME(" + columnMin + ") AND DATETIME(" + columnMax + ")", new string[] { value });
        }


        // SubQuery + DATETIME CAST
        public static Criteria equalsAsDateTime(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("DATETIME((" + subQuery.buildQuery() + ")) = ?", parameters);
        }

        public static Criteria notEqualsAsDateTime(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("DATETIME((" + subQuery.buildQuery() + ")) <> ?", parameters);
        }

        public static Criteria greaterThanAsDateTime(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("DATETIME((" + subQuery.buildQuery() + ")) > ?", parameters);
        }

        public static Criteria lesserThanAsDateTime(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("DATETIME((" + subQuery.buildQuery() + ")) < ?", parameters);
        }

        public static Criteria greaterThanOrEqualAsDateTime(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("DATETIME((" + subQuery.buildQuery() + ")) >= ?", parameters);
        }

        public static Criteria lesserThanOrEqualAsDateTime(QueryBuilder subQuery, string value)
        {
            string[] subQueryParams = subQuery.buildParameters();
            string[] parameters = new string[subQueryParams.Length + 1];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 1] = value;

            return create("DATETIME((" + subQuery.buildQuery() + ")) <= ?", parameters);
        }

        public static Criteria betweenAsDateTime(QueryBuilder subQuery, string valueMin, string valueMax)
        {
            string[] subQueryParams = subQuery.buildParametersDefaultEmpty();
            string[] parameters = new string[subQueryParams.Length + 2];

            System.Array.Copy(subQueryParams, 0, parameters, 0, subQueryParams.Length);
            parameters[parameters.Length - 2] = valueMin;
            parameters[parameters.Length - 1] = valueMax;

            return create("DATETIME((" + subQuery.buildQuery() + ")) BETWEEN ? AND ?", parameters);
        }


        // EQUALS between column
        public static Criteria equalsColumn(string leftColumn, string rightColumn)
        {
            return create(leftColumn + " = " + rightColumn, null);
        }

        public static Criteria equalsColumn(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") = " + rightColumn, subQuery.buildParameters());
        }

        public static Criteria equalsColumn(string leftColumn, QueryBuilder subQuery)
        {
            return create(leftColumn + " = (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }

        public static Criteria equalsColumn(QueryBuilder leftSubQuery, QueryBuilder rightSubQuery)
        {
            string[] leftSubQueryParams = leftSubQuery.buildParameters();
            string[] rightSubQueryParams = rightSubQuery.buildParameters();
            string[] parameters = new string[leftSubQueryParams.Length + rightSubQueryParams.Length];

            System.Array.Copy(leftSubQueryParams, 0, parameters, 0, leftSubQueryParams.Length);
            System.Array.Copy(rightSubQueryParams, 0, parameters, leftSubQueryParams.Length, rightSubQueryParams.Length);

            return create("(" + leftSubQuery.buildQuery() + ") = (" + rightSubQuery.buildQuery() + ")", parameters);
        }


        // NOT EQUALS between column
        public static Criteria notEqualsColumn(string leftColumn, string rightColumn)
        {
            return create(leftColumn + " <> " + rightColumn, null);
        }

        public static Criteria notEqualsColumn(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") <> " + rightColumn, subQuery.buildParameters());
        }

        public static Criteria notEqualsColumn(string leftColumn, QueryBuilder subQuery)
        {
            return create(leftColumn + " <> (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }

        public static Criteria notEqualsColumn(QueryBuilder leftSubQuery, QueryBuilder rightSubQuery)
        {
            string[] leftSubQueryParams = leftSubQuery.buildParameters();
            string[] rightSubQueryParams = rightSubQuery.buildParameters();
            string[] parameters = new string[leftSubQueryParams.Length + rightSubQueryParams.Length];

            System.Array.Copy(leftSubQueryParams, 0, parameters, 0, leftSubQueryParams.Length);
            System.Array.Copy(rightSubQueryParams, 0, parameters, leftSubQueryParams.Length, rightSubQueryParams.Length);

            return create("(" + leftSubQuery.buildQuery() + ") <> (" + rightSubQuery.buildQuery() + ")", parameters);
        }


        // LESSER THAN between column
        public static Criteria lesserThanColumn(string leftColumn, string rightColumn)
        {
            return create(leftColumn + " < " + rightColumn, null);
        }

        public static Criteria lesserThanColumn(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") < " + rightColumn, subQuery.buildParameters());
        }

        public static Criteria lesserThanColumn(string leftColumn, QueryBuilder subQuery)
        {
            return create(leftColumn + " < (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }

        public static Criteria lesserThanColumn(QueryBuilder leftSubQuery, QueryBuilder rightSubQuery)
        {
            string[] leftSubQueryParams = leftSubQuery.buildParameters();
            string[] rightSubQueryParams = rightSubQuery.buildParameters();
            string[] parameters = new string[leftSubQueryParams.Length + rightSubQueryParams.Length];

            System.Array.Copy(leftSubQueryParams, 0, parameters, 0, leftSubQueryParams.Length);
            System.Array.Copy(rightSubQueryParams, 0, parameters, leftSubQueryParams.Length, rightSubQueryParams.Length);

            return create("(" + leftSubQuery.buildQuery() + ") < (" + rightSubQuery.buildQuery() + ")", parameters);
        }


        // LESSER THAN OR EQUALS between column
        public static Criteria lesserThanOrEqualsColumn(string leftColumn, string rightColumn)
        {
            return create(leftColumn + " <= " + rightColumn, null);
        }

        public static Criteria lesserThanOrEqualsColumn(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") <= " + rightColumn, subQuery.buildParameters());
        }

        public static Criteria lesserThanOrEqualsColumn(string leftColumn, QueryBuilder subQuery)
        {
            return create(leftColumn + " <= (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }

        public static Criteria lesserThanOrEqualsColumn(QueryBuilder leftSubQuery, QueryBuilder rightSubQuery)
        {
            string[] leftSubQueryParams = leftSubQuery.buildParameters();
            string[] rightSubQueryParams = rightSubQuery.buildParameters();
            string[] parameters = new string[leftSubQueryParams.Length + rightSubQueryParams.Length];

            System.Array.Copy(leftSubQueryParams, 0, parameters, 0, leftSubQueryParams.Length);
            System.Array.Copy(rightSubQueryParams, 0, parameters, leftSubQueryParams.Length, rightSubQueryParams.Length);

            return create("(" + leftSubQuery.buildQuery() + ") <= (" + rightSubQuery.buildQuery() + ")", parameters);
        }


        // GREATER THAN between column
        public static Criteria greaterThanColumn(string leftColumn, string rightColumn)
        {
            return create(leftColumn + " > " + rightColumn, null);
        }

        public static Criteria greaterThanColumn(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") > " + rightColumn, subQuery.buildParameters());
        }

        public static Criteria greaterThanColumn(string leftColumn, QueryBuilder subQuery)
        {
            return create(leftColumn + " > (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }

        public static Criteria greaterThanColumn(QueryBuilder leftSubQuery, QueryBuilder rightSubQuery)
        {
            string[] leftSubQueryParams = leftSubQuery.buildParameters();
            string[] rightSubQueryParams = rightSubQuery.buildParameters();
            string[] parameters = new string[leftSubQueryParams.Length + rightSubQueryParams.Length];

            System.Array.Copy(leftSubQueryParams, 0, parameters, 0, leftSubQueryParams.Length);
            System.Array.Copy(rightSubQueryParams, 0, parameters, leftSubQueryParams.Length, rightSubQueryParams.Length);

            return create("(" + leftSubQuery.buildQuery() + ") > (" + rightSubQuery.buildQuery() + ")", parameters);
        }


        // GREATER THAN OR EQUALS between column
        public static Criteria greaterThanOrEqualsColumn(string leftColumn, string rightColumn)
        {
            return create(leftColumn + " >= " + rightColumn, null);
        }

        public static Criteria greaterThanOrEqualsColumn(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") >= " + rightColumn, subQuery.buildParameters());
        }

        public static Criteria greaterThanOrEqualsColumn(string leftColumn, QueryBuilder subQuery)
        {
            return create(leftColumn + " >= (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }

        public static Criteria greaterThanOrEqualsColumn(QueryBuilder leftSubQuery, QueryBuilder rightSubQuery)
        {
            string[] leftSubQueryParams = leftSubQuery.buildParameters();
            string[] rightSubQueryParams = rightSubQuery.buildParameters();
            string[] parameters = new string[leftSubQueryParams.Length + rightSubQueryParams.Length];

            System.Array.Copy(leftSubQueryParams, 0, parameters, 0, leftSubQueryParams.Length);
            System.Array.Copy(rightSubQueryParams, 0, parameters, leftSubQueryParams.Length, rightSubQueryParams.Length);

            return create("(" + leftSubQuery.buildQuery() + ") >= (" + rightSubQuery.buildQuery() + ")", parameters);
        }


        // EQUALS between column + DATE CAST
        public static Criteria equalsColumnAsDate(string leftColumn, string rightColumn)
        {
            return create("DATE(" + leftColumn + ") = DATE(" + rightColumn + ")", null);
        }

        public static Criteria equalsColumnAsDate(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") = DATE(" + rightColumn + ")", subQuery.buildParameters());
        }

        public static Criteria equalsColumnAsDate(string leftColumn, QueryBuilder subQuery)
        {
            return create("DATE(" + leftColumn + ") = (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }


        // NOT EQUALS between column + DATE CAST
        public static Criteria notEqualsColumnAsDate(string leftColumn, string rightColumn)
        {
            return create("DATE(" + leftColumn + ") <> DATE(" + rightColumn + ")", null);
        }

        public static Criteria notEqualsColumnAsDate(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") <> DATE(" + rightColumn + ")", subQuery.buildParameters());
        }

        public static Criteria notEqualsColumnAsDate(string leftColumn, QueryBuilder subQuery)
        {
            return create("DATE(" + leftColumn + ") <> (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }


        // LESSER THAN between column + DATE CAST
        public static Criteria lesserThanColumnAsDate(string leftColumn, string rightColumn)
        {
            return create("DATE(" + leftColumn + ") < DATE(" + rightColumn + ")", null);
        }

        public static Criteria lesserThanColumnAsDate(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") < DATE(" + rightColumn + ")", subQuery.buildParameters());
        }

        public static Criteria lesserThanColumnAsDate(string leftColumn, QueryBuilder subQuery)
        {
            return create("DATE(" + leftColumn + ") < (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }


        // LESSER THAN OR EQUALS between column + DATE CAST
        public static Criteria lesserThanOrEqualsColumnAsDate(string leftColumn, string rightColumn)
        {
            return create("DATE(" + leftColumn + ") <= DATE(" + rightColumn + ")", null);
        }

        public static Criteria lesserThanOrEqualsColumnAsDate(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") <= DATE(" + rightColumn + ")", subQuery.buildParameters());
        }

        public static Criteria lesserThanOrEqualsColumnAsDate(string leftColumn, QueryBuilder subQuery)
        {
            return create("DATE(" + leftColumn + ") <= (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }


        // GREATER THAN  + DATE CAST
        public static Criteria greaterThanColumnAsDate(string leftColumn, string rightColumn)
        {
            return create("DATE(" + leftColumn + ") > DATE(" + rightColumn + ")", null);
        }

        public static Criteria greaterThanColumnAsDate(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") > DATE(" + rightColumn + ")", subQuery.buildParameters());
        }

        public static Criteria greaterThanColumnAsDate(string leftColumn, QueryBuilder subQuery)
        {
            return create("DATE(" + leftColumn + ") > (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }


        // GREATER THAN OR EQUALS + DATE CAST
        public static Criteria greaterThanOrEqualsColumnAsDate(string leftColumn, string rightColumn)
        {
            return create("DATE(" + leftColumn + ") >= DATE(" + rightColumn + ")", null);
        }

        public static Criteria greaterThanOrEqualsColumnAsDate(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") >= DATE(" + rightColumn + ")", subQuery.buildParameters());
        }

        public static Criteria greaterThanOrEqualsColumnAsDate(string leftColumn, QueryBuilder subQuery)
        {
            return create("DATE(" + leftColumn + ") >= (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }


        // EQUALS between column + DATETIME CAST
        public static Criteria equalsColumnAsDateTime(string leftColumn, string rightColumn)
        {
            return create("DATETIME(" + leftColumn + ") = DATETIME(" + rightColumn + ")", null);
        }

        public static Criteria equalsColumnAsDateTime(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") = DATETIME(" + rightColumn + ")", subQuery.buildParameters());
        }

        public static Criteria equalsColumnAsDateTime(string leftColumn, QueryBuilder subQuery)
        {
            return create("DATETIME(" + leftColumn + ") = (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }


        // NOT EQUALS between column + DATETIME CAST
        public static Criteria notEqualsColumnAsDateTime(string leftColumn, string rightColumn)
        {
            return create("DATETIME(" + leftColumn + ") <> DATETIME(" + rightColumn + ")", null);
        }

        public static Criteria notEqualsColumnAsDateTime(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") <> DATETIME(" + rightColumn + ")", subQuery.buildParameters());
        }

        public static Criteria notEqualsColumnAsDateTime(string leftColumn, QueryBuilder subQuery)
        {
            return create("DATETIME(" + leftColumn + ") <> (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }


        // LESSER THAN between column + DATETIME CAST
        public static Criteria lesserThanColumnAsDateTime(string leftColumn, string rightColumn)
        {
            return create("DATETIME(" + leftColumn + ") < DATETIME(" + rightColumn + ")", null);
        }

        public static Criteria lesserThanColumnAsDateTime(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") < DATETIME(" + rightColumn + ")", subQuery.buildParameters());
        }

        public static Criteria lesserThanColumnAsDateTime(string leftColumn, QueryBuilder subQuery)
        {
            return create("DATETIME(" + leftColumn + ") < (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }


        // LESSER THAN OR EQUALS between column + DATETIME CAST
        public static Criteria lesserThanOrEqualsColumnAsDateTime(string leftColumn, string rightColumn)
        {
            return create("DATETIME(" + leftColumn + ") <= DATETIME(" + rightColumn + ")", null);
        }

        public static Criteria lesserThanOrEqualsColumnAsDateTime(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") <= DATETIME(" + rightColumn + ")", subQuery.buildParameters());
        }

        public static Criteria lesserThanOrEqualsColumnAsDateTime(string leftColumn, QueryBuilder subQuery)
        {
            return create("DATETIME(" + leftColumn + ") <= (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }


        // GREATER THAN between column + DATETIME CAST
        public static Criteria greaterThanColumnAsDateTime(string leftColumn, string rightColumn)
        {
            return create("DATETIME(" + leftColumn + ") > DATETIME(" + rightColumn + ")", null);
        }

        public static Criteria greaterThanColumnAsDateTime(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") > DATETIME(" + rightColumn + ")", subQuery.buildParameters());
        }

        public static Criteria greaterThanColumnAsDateTime(string leftColumn, QueryBuilder subQuery)
        {
            return create("DATETIME(" + leftColumn + ") > (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }


        // GREATER THAN OR EQUALS between column + DATETIME CAST
        public static Criteria greaterThanOrEqualsColumnAsDateTime(string leftColumn, string rightColumn)
        {
            return create("DATETIME(" + leftColumn + ") >= DATETIME(" + rightColumn + ")", null);
        }

        public static Criteria greaterThanOrEqualsColumnAsDateTime(QueryBuilder subQuery, string rightColumn)
        {
            return create("(" + subQuery.buildQuery() + ") >= DATETIME(" + rightColumn + ")", subQuery.buildParameters());
        }

        public static Criteria greaterThanOrEqualsColumnAsDateTime(string leftColumn, QueryBuilder subQuery)
        {
            return create("DATETIME(" + leftColumn + ") >= (" + subQuery.buildQuery() + ")", subQuery.buildParameters());
        }


        // IN
        public static Criteria In(string column, Object[] parameters)
        {
            return In(column, parameters, false);
        }

        public static Criteria notIn(string column, Object[] parameters)
        {
            return In(column, parameters, true);
        }

        public static Criteria In(string column, Object[] parameters, bool not)
        {
            if (parameters == null || parameters.Length <= 0)
                return null;

            StringBuilder sb = new StringBuilder();
            sb.Append(column);

            if (not)
                sb.Append(" NOT");

            sb.Append(" IN");
            sb.Append("(");

            string[] stringParameters = new string[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i > 0)
                    sb.Append(", ");

                sb.Append("?");
                stringParameters[i] = Utils.toString(parameters[i]);
            }

            sb.Append(")");

            //SimpleCriteria criteria = new SimpleCriteria(sb.ToString(), TypeCriteria.AND, stringParameters);
            Criteria cg = new Criteria();
            //cg._criterias.Add(criteria);

            return cg;
        }


        // IN + SubQuery
        public static Criteria In(QueryBuilder subQuery, Object[] parameters)
        {
            return In(subQuery, parameters, false);
        }

        public static Criteria NotIn(QueryBuilder subQuery, Object[] parameters)
        {
            return In(subQuery, parameters, true);
        }

        public static Criteria In(QueryBuilder subQuery, Object[] parameters, bool not)
        {
            if (parameters == null || parameters.Length <= 0)
                return null;

            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            sb.Append(subQuery.buildQuery());
            sb.Append(")");

            if (not)
                sb.Append(" NOT");

            sb.Append(" IN");
            sb.Append("(");

            string[] stringParameters = new string[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i > 0)
                    sb.Append(", ");

                sb.Append("?");
                stringParameters[i] = Utils.toString(parameters[i]);
            }

            sb.Append(")");

            string[] subQueryParams = subQuery.buildParameters();
            string[] prmtrs = new string[subQueryParams.Length];

            System.Array.Copy(subQueryParams, 0, prmtrs, 0, subQueryParams.Length);
            System.Array.Copy(stringParameters, 0, prmtrs, subQueryParams.Length, stringParameters.Length);

            SimpleCriteria criteria = new SimpleCriteria(sb.ToString(), TypeCriteria.AND, prmtrs);
            Criteria cg = new Criteria();
            cg._criterias.Add(criteria);

            return cg;
        }


        // EXISTS
        public static Criteria exists(QueryBuilder query)
        {
            return create("EXISTS (" + query.buildQuery() + ")", query.buildParameters());
        }

        public static Criteria notExists(QueryBuilder query)
        {
            return create("NOT EXISTS (" + query.buildQuery() + ")", query.buildParameters());
        }


        // Logical operators
        public static Criteria or(Criteria left, Criteria right)
        {
            return or(left.build(), right.build(), left.getParameters(), right.getParameters());
        }

        public static Criteria and(Criteria left, Criteria right)
        {
            return and(left.build(), right.build(), left.getParameters(), right.getParameters());
        }

        public static Criteria and(string left, string right, string[] parametersLeft, string[] parametersRight)
        {
            SimpleCriteria leftCriteria = new SimpleCriteria(left, TypeCriteria.AND, parametersLeft);
            SimpleCriteria rightCriteria = new SimpleCriteria(right, TypeCriteria.AND, parametersRight);

            Criteria cg = new Criteria();
            cg._criterias.Add(leftCriteria);
            cg._criterias.Add(rightCriteria);

            return cg;
        }

        public static Criteria or(string left, string right, string[] parametersLeft, string[] parametersRight)
        {
            SimpleCriteria leftCriteria = new SimpleCriteria(left, TypeCriteria.AND, parametersLeft);
            SimpleCriteria rightCriteria = new SimpleCriteria(right, TypeCriteria.OR, parametersRight);

            Criteria cg = new Criteria();
            cg._criterias.Add(leftCriteria);
            cg._criterias.Add(rightCriteria);

            return cg;
        }

        public override string build()
        {
            StringBuilder sb = new StringBuilder();
            bool firstItem = true;

            sb.Append("(");

            foreach (SimpleCriteria c in _criterias)
            {
                if (firstItem)
                {
                    firstItem = false;
                }
                else if (c.getType() == TypeCriteria.AND)
                {
                    sb.Append(" AND ");
                }
                else if (c.getType() == TypeCriteria.OR)
                {
                    sb.Append(" OR ");
                }

                sb.Append(c.build());
            }

            sb.Append(")");
            return sb.ToString();
        }


        public override string[] getParameters()
        {
            List<string> parameters = new List<string>();
            string[] parametersArray;

            foreach (SimpleCriteria criteria in _criterias)
            {
                parametersArray = criteria.getParameters();

                if (parametersArray == null)
                    continue;

                parameters.AddRange(parametersArray);

            }

            return parameters.ToArray();
        }

        public Criteria and(string criteria, string[] parameters)
        {
            SimpleCriteria c = new SimpleCriteria(criteria, TypeCriteria.AND, parameters);
            return and(c);
        }

        public Criteria or(string criteria, string[] parameters)
        {
            SimpleCriteria c = new SimpleCriteria(criteria, TypeCriteria.OR, parameters);
            return or(c);
        }

        public Criteria and(SimpleCriteria criteria)
        {
            criteria.setType(TypeCriteria.AND);
            _criterias.Add(criteria);

            return this;
        }

        public Criteria or(SimpleCriteria criteria)
        {
            criteria.setType(TypeCriteria.OR);
            _criterias.Add(criteria);

            return this;
        }
    }
}