using SQLBuilder.Querries.DeleteQueryNS.Interfaces;
using SQLBuilder.StringProviders.Interfaces;

namespace SQLBuilder.Querries.DeleteQueryNS
{
    public class DeleteQueryModel : IStringableQuery
    {
        public DeleteQueryModel() { }
        public DeleteQueryModel(DeleteQueryModel deleteQueryModel) 
        {
            QTable = deleteQueryModel.QTable;
            QWhere = deleteQueryModel.QWhere;
        }
        public ATable QTable;
        public ACondition QWhere;

        public string GetString(IQueryStringProvider stringProvider)
        {
            return stringProvider.GetString(this);
        }
    }

}
