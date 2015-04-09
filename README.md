# CachingManager
InMemory,Memcached(Couchbase) and Redis Cache Manager in my framework

<p>Example Config File</p>

<b>Section:</b>
  <section 
  name="CacheProvidersSection" 
  type="INGA.Framework.Helpers.Configuration.ConfigurationSections.CacheProvidersSection"/>
  </configSections>
<b>Elements</b>
 <CacheProvidersSection>
    <CacheProviders>
      <add Name="Redis" Host="127.0.0.1" Port="6996" Username="*" Password="*" IsActive="1" CacheName=""/>
      <add Name="CouchbaseMemcached" Host="127.0.0.1" Port="11211" Username="*" Password="*" IsActive="0" CacheName=""/>
      <add Name="InMemoryCache" Host="*" Port="*" Username="*" Password="*" IsActive="0" CacheName=""/>
    </CacheProviders>
  </CacheProvidersSection>
  
  
  <p>Code Sample</p>
  //get active provider which is defined on webconfig(in this example,ı use redis)
   var fac = CachingFactory.Instance;
   fac.SetAsync("test", "myValue");
   var value = fac.GetAsync<string>("test");
