using SQLBuilder.Querries.DeleteQueryNS.Interfaces;

namespace SQLBuilder.Querries.DeleteQueryNS
{
    public class Delete : DeleteQueryModel, IDWhereable
    {
        public Delete() { }
        public Delete(DeleteQueryModel deleteQueryModel) : base(deleteQueryModel) { }
        public DWhere Where(ACondition condition)
        {
            return new DWhere(this) { QWhere = condition };
        }
    }

}
