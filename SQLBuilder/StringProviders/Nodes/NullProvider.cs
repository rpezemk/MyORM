using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLBuilder.StringProviders.Experimental.SResult;
using SQLBuilder.StringProviders.ProviderChain;

namespace SQLBuilder.StringProviders.Nodes
{
    class NullProvider  : AStringProvider
    {
        public override ASPResult Manage(object condition)
        {
            if (condition == null)
                return new SPResultOK() { ResultValue = "" };  
            else
                return base.Manage(condition);
        }
    }
}
