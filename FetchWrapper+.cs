using System.Collections.Generic;
using System.Linq;

namespace FetchXmlWrapper

{
    //Generic additions
    public partial class FetchWrapper
    {
        //Aron Fischman 09/04/2016
        //commented out original constructor FetchWrapper(FetchExpression fetch)
        public FetchWrapper(FetchExpression fetch)
        {
            fetchEntity = (FetchEntity)fetch.Items[0];
            fetchEntity.name = fetch.Entity();
            fetchEntityItems = fetchEntity.Items.ToList();
            fetchExpression = fetch;
            fetchExpression.mapping = FetchMapping.logical;
        }

        //Aron Fischman 11/09/2015
        public void AddColumn(string columnName, Aggregate aggregate, string alias, bool distinct)
        {
            FetchExpression.aggregate = true;
            RemoveFetchEntityItem(typeof(AllAttributes));
            FetchAttribute att = new FetchAttribute();
            att.name = columnName;
            att.aggregate = aggregate;
            att.alias = alias;
            att.distinct = distinct;
            RemoveColumn(columnName);
            AddFetchEntityItem(att);
        }

        //Aron Fischman 12/21/2015
        protected void InternalAppendFilter(List<FetchCondition> conditions, FetchFilterType logicalOperator)
        {
            FetchFilter filter = new FetchFilter();
            filter.type = logicalOperator;
            List<object> items = new List<object>();
            foreach (FetchCondition condition in conditions)
            {
                items.Add(condition);
            }
            items.AddRange(fetchEntity.Items.Where(f => f.GetType() == typeof(FetchFilter)).ToList());
            filter.Items = items.ToArray();
            this.RemoveFetchEntityItem(typeof(FetchFilter));
            this.AddFetchEntityItem(filter);
        }

        public void AddActiveOnlyFilter()
        {
            AppendFilter("statecode", "0", FetchFilterType.and);
        }

        public string toXml()
        {
            return FetchExpression.ToXml();
        }
    }
}
