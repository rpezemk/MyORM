using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SQLBuilder.StructureTypes.Chainable;
using SQLBuilder.StringProviders.Experimental.SResult;

namespace SQLBuilder.StringProviders.ProviderChain
{

    

    public abstract class AStringProvider : AChainable<AStringProvider>
    {
        public virtual ASPResult Manage(object condition)
        {
            if (ChildProvider != null)
                return ChildProvider.Manage(condition);
            else
                return new SPErrorResult() { ResultValue = "no compatible converter found in chain!" };
        }

    }



}
