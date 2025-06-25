using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace ErpMobile.Api.Data
{
    /// <summary>
    /// IConfiguration için proxy sınıf
    /// </summary>
    public class ConnectionStringProxy : IConfiguration
    {
        private readonly IConfiguration _originalConfiguration;

        public ConnectionStringProxy(IConfiguration originalConfiguration)
        {
            _originalConfiguration = originalConfiguration;
        }

        public string this[string key]
        {
            get => _originalConfiguration[key];
            set => _originalConfiguration[key] = value;
        }

        public IConfigurationSection GetSection(string key)
        {
            // Eğer ConnectionStrings:ErpConnection isteniyorsa, özel işlem yap
            if (key == "ConnectionStrings")
            {
                return new ConnectionStringsSection(_originalConfiguration.GetSection(key));
            }
            
            return _originalConfiguration.GetSection(key);
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return _originalConfiguration.GetChildren();
        }

        public IChangeToken GetReloadToken()
        {
            return _originalConfiguration.GetReloadToken();
        }

        /// <summary>
        /// ConnectionStrings bölümü için özel proxy
        /// </summary>
        private class ConnectionStringsSection : IConfigurationSection
        {
            private readonly IConfigurationSection _originalSection;

            public ConnectionStringsSection(IConfigurationSection originalSection)
            {
                _originalSection = originalSection;
            }

            public string this[string key]
            {
                get
                {
                    // ErpConnection isteniyorsa, güncel bağlantı dizesini döndür
                    if (key == "ErpConnection")
                    {
                        string connectionString = ErpConnectionStringProvider.GetConnectionString();
                        if (!string.IsNullOrEmpty(connectionString))
                        {
                            Console.WriteLine($"ConnectionStringProxy: ErpConnection için güncel bağlantı dizesi döndürülüyor");
                            return connectionString;
                        }
                    }
                    
                    return _originalSection[key];
                }
                set => _originalSection[key] = value;
            }

            public string Key => _originalSection.Key;
            public string Path => _originalSection.Path;
            public string Value
            {
                get => _originalSection.Value;
                set => _originalSection.Value = value;
            }

            public IConfigurationSection GetSection(string key)
            {
                return _originalSection.GetSection(key);
            }

            public IEnumerable<IConfigurationSection> GetChildren()
            {
                return _originalSection.GetChildren();
            }
            
            public IChangeToken GetReloadToken()
            {
                return _originalSection.GetReloadToken();
            }
        }
    }
}
