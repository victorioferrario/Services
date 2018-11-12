# Services
A comprehensive set of c# libs, that in reality act as the application facade, for all our internal WCF Services.

 Image each outside node in this xml representation of the application is a layer. 
 You have the following layers:
 - [ ] azure / cloud tenent
 - [ ] services layers
 - [ ] websites applications, with session state
 
 
 
```
<aspnet-azure>    
    <signalR-service></signalR-service>   
    <sql-services></sql-services>
    <table-storage></table-storage>
   <wcf-services></wcf-services>
   <identity-server></identity-server>
</aspnet-azure>
<aspnet-services>
    ## The repository for this later can be found:
    https://github.com/victorioferrario/Services
</aspnet-services>
<aspnet-site>  
  <asp-web-session>    
    <asp-web-api>
      <durandal-typescript-spa></durandal-typesrcipt-spa>
    </asp-web-api>
  </asp-web-session>
</aspnet-site>
```
