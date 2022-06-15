using ADFS_Plug_in.Interfaces;
using System.DirectoryServices;

namespace ADFS_Plug_in.Checkers
{
    internal class ActiveDirectoryCheck : IChecker
    {
        public Task<bool> CheckAsync()
        {
            string path = "LDAP://my-ad-server";// it's dummy path to AD server
            return Task.Run(() =>
            {
                using (DirectoryEntry directoryRoot = new DirectoryEntry(path))
                using (DirectorySearcher searcher = new DirectorySearcher(directoryRoot, $"(&(objectClass=User)(samaccountname={input}))"))
                {
                    using (SearchResultCollection results = searcher.FindAll())
                        foreach (SearchResult result in results)
                        {
                            DirectoryEntry de = result.GetDirectoryEntry();

                            if (!IsActive(de))
                            {
                                return Task.FromResult(false);
                            }
                        }
                }
                return Task.FromResult(true);
            });
        }

        private bool IsActive(DirectoryEntry de)
        {
            if (de.NativeGuid == null) return false;

            int flags = (int)de.Properties["userAccountControl"].Value;

            return !Convert.ToBoolean(flags & 0x0002);
        }
    }
}
