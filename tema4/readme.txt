.NET Remoting - ce se schimba?

- obiectele din domain au atributul Serializable
- dispare Networking
- Service extinde din MarshalByRefObject
- Controllerele extind din MarshalByRefObject
- pornirea clientului si a serverului

+ in Service : redefinirea metodei InitializeLifetimeService