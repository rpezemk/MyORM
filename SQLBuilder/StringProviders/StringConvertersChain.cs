using SQLBuilder.StringProviders.Nodes;
using SQLBuilder.StringProviders.ForQuery;
using SQLBuilder.StringProviders.Interfaces;
using SQLBuilder.StringProviders.ProviderChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder.StringProviders.Chains
{


    /// <summary>
    /// Singleton class with 
    /// </summary>
    internal sealed class StringConvertersChain
    {
        #region Constructor (private only!)
        /// <summary>
        /// Constructor private, to disable public new() calls. 
        /// </summary>
        private StringConvertersChain() { }
        private static StringConvertersChain instance;
        #endregion

        #region Private Static fields
        private GeneralQueryStringProvider generalQueryStringProvider; //
        private AStringProvider aSelectStringProviderChain; //
        private AStringProvider aInsertStringProviderChain; //
        #endregion

        #region Private Methods
        private static void InstantiateWhenNull()
        {
            if (instance == null)
                instance = new StringConvertersChain();
        }
        #endregion


        #region Public Methods (instance getters)

        /// <summary>
        /// Gets StringProvider for query. 
        /// </summary>
        /// <returns></returns>
        public static IQueryStringProvider GetQueryStringProviderInstance()
        {
            InstantiateWhenNull();
            if (instance.generalQueryStringProvider == null)
                instance.generalQueryStringProvider = new GeneralQueryStringProvider();
            return instance.generalQueryStringProvider;
        }

        /// <summary>
        /// Gets aliased Query/Expression
        /// </summary>
        /// <returns></returns>
        public static AStringProvider GetAliasedProviderChainInstance()
        {
            InstantiateWhenNull();
            if (instance.aSelectStringProviderChain == null)
            {
                instance.aSelectStringProviderChain = 
                    new NullProvider()
                    .SetNext(new WhereSP())
                    .SetNext(new CaseSP())
                    .SetNext(new ComparableSP())
                    .SetNext(new FieldSP())  // <== this one is different in select vs others
                    .SetNext(new UnarySP())
                    .SetNext(new ExistsSP())
                    .SetNext(new TwoValueSP())
                    .SetNext(new InSP())
                    .SetNext(new NotSP())
                    .SetNext(new GeneralStringProvider()).RootProvider;
            }
            return instance.aSelectStringProviderChain;
        }

        /// <summary>
        /// Gets Query/Expression non aliased 
        /// </summary>
        /// <returns></returns>
        public static AStringProvider GetNonAliasedProviderChainInstance()
        {
            InstantiateWhenNull();
            if (instance.aInsertStringProviderChain == null)
            {
                instance.aInsertStringProviderChain = 
                    new NullProvider()
                    .SetNext(new WhereSP())
                    .SetNext(new CaseSP())
                    .SetNext(new ComparableSP())
                    .SetNext(new FieldNonAliasSP())  // <== this one is different in select vs others
                    .SetNext(new UnarySP())
                    .SetNext(new ExistsSP())
                    .SetNext(new TwoValueSP())
                    .SetNext(new InSP())
                    .SetNext(new NotSP())
                    .SetNext(new GeneralStringProvider()).RootProvider;
            }
            return instance.aInsertStringProviderChain;
        }

        #endregion



    }
}
