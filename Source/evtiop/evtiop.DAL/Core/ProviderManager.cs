using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evtiop.DAL.Core
{
    public class ProviderManager
    {
        #region Property
        public string ProviderName { get; set; }

        public DbProviderFactory Factory
        {
            get
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);
                return factory;
            }
        }
        #endregion

        #region Constructor
        public ProviderManager()
        {
            ProviderName = ConfigurationSetting.GetProviderName(ConfigurationSetting.DefaultConnection);
        }

        public ProviderManager(string providerName)
        {
            ProviderName = providerName;
        }
        #endregion
    }
}
