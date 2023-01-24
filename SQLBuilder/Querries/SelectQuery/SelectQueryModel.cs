using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SQLBuilder.StringProviders.Interfaces;

namespace SQLBuilder.Querries.SelectQueryNS
{
    public class SelectQueryModel : IStringableQuery
    {
        public SelectQueryModel()
        {

        }
        public SelectQueryModel(SelectQueryModel queryModel)
        {
            if(queryModel.QSelect != null) 
                QSelect = queryModel.QSelect;
            QTop = queryModel.QTop;
            if(queryModel.QFrom != null)
                QFrom = queryModel.QFrom;
            if(queryModel.QJoins != null)
                QJoins = queryModel.QJoins;
            if (queryModel.QWhere != null)
                QWhere = queryModel.QWhere;
            WithNolock = queryModel.WithNolock;
        }
        public ITuple QSelect;
        public int QTop = 0;
        public ATable QFrom;
        public List<Join> QJoins = new List<Join>();
        public ACondition QWhere;
        public bool WithNolock = false;

        public string GetString(IQueryStringProvider stringProvider)
        {
            return stringProvider.GetString(this);
        }

    }


}
