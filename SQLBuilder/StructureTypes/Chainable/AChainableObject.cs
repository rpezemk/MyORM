using System.Collections.Generic;

namespace SQLBuilder.StructureTypes.Chainable
{
    public abstract class AChainable<T1> where T1 : AChainable<T1>
    {
        public T1 ChildProvider;
        public AChainable<T1> ParentProvider;
        public T1 RootProvider { get => GetRootProvider(); }
        public AChainable<T1> SetNext(T1 provider)
        {
            ChildProvider = provider;
            ChildProvider.ParentProvider = this;
            return ChildProvider;
        }

        public string GetChainList()
        {
            var res = RootProvider.GetSubChainList();
            var resList = string.Join("\n ==> ", res);
            return resList;
        }

        protected List<string> GetSubChainList()
        {
            var res = new List<string>();
            res.Add(GetType().Name);
            if (ChildProvider != null)
                res.AddRange(ChildProvider.GetSubChainList());
            return res;
        }

        protected T1 GetRootProvider()
        {
            if (ParentProvider != null)
                return ParentProvider.GetRootProvider();
            else
                return (T1)this;
        }
    }



}
