using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
    class Program
    {
        static void Main(string[] args)
        {
            Banking();
        }

        static void Banking()
        {
            const long workspaceId = 73645;
            const string apiKey = "5c338edb-1cbe-4c61-a82f-fee879a4e120";
            const string apiSecret = "36a34f8b-8c21-41ca-9efc-d405b2453ea3";

            StructurizrClient structurizrClient = new StructurizrClient(apiKey, apiSecret);
            Workspace workspace = new Workspace("C4 Model - Easy Store", "Sistema sample-content");
            ViewSet viewSet = workspace.Views;
            Model model = workspace.Model;

            // 1. Diagrama de Contexto
            SoftwareSystem librarySystem = model.AddSoftwareSystem("Sistema sample-content", "sample message");
                        
            Person escritor = model.AddPerson("Escritor", "Usuario capaz de publicar contenido textual.");
            Person lector = model.AddPerson("Lector", "Usuario que solo podra leer contenido y suscribirse");
            
            lector.Uses(librarySystem, "Realiza consultas para mantenerse al tanto de las publicaciones que puede leer");
            escritor.Uses(librarySystem, "Realiza consultas para mantenerse al tanto de los lectors que seleccionan su negocio");
            
            SystemContextView contextView = viewSet.CreateSystemContextView(librarySystem, "Contexto", "Diagrama de contexto");
            contextView.PaperSize = PaperSize.A3_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // Tags
            escritor.AddTags("Ciudadano");
            lector.AddTags("Ciudadano");
            librarySystem.AddTags("SistemaLibros");

            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle("Ciudadano") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("SistemaLibros") { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
            

            // 2. Diagrama de Contenedores
            Container mobileApplication =       librarySystem.AddContainer("Mobile App", "Permite a los usuarios visualizar las actividades turisticas que pueden realizar cercanas a su destino.", "Flutter");
            Container webApplication =          librarySystem.AddContainer("Web App", "Permite a los usuarios visualizar las actividades turisticas que pueden realizar cercanas a su destino.", "Vue");
            Container landingPage =             librarySystem.AddContainer("Landing Page", "", "Bootstrap");
            Container pagoContext =             librarySystem.AddContainer("Trip Plan Context", "Bounded Context del Microservicio de Planificación de viajes y hospedajes en el destino seleccionado", "NodeJS (NestJS)");
            Container registroContext =         librarySystem.AddContainer("registro Context", "Bounded Context del Microservicio de promociones existentes", "NodeJS (NestJS)");
            Container notificacionesContext =   librarySystem.AddContainer("notificaciones Context", "Bounded Context del Microservicio de información de los notificacioness", "NodeJS (NestJS)");
            Container calificacionesContext =   librarySystem.AddContainer("calificaciones Context", "Bounded Context del microservicio de información del calificaciones", "NodeJS (NestJS)");
            Container chatContext =             librarySystem.AddContainer("chat Context", "Bounded Context del microservicio de autenticación para calificaciones y notificaciones", "NodeJS (NestJS)");
            Container database1 =                librarySystem.AddContainer("Database1", "", "MySQL");
            Container database2 =                librarySystem.AddContainer("Database2", "", "MySQL");
            Container database3 =                librarySystem.AddContainer("Database3", "", "MySQL");
            Container database4 =                librarySystem.AddContainer("Database4", "", "MySQL");
            Container database5 =                librarySystem.AddContainer("Database5", "", "MySQL");
            
            lector.Uses(mobileApplication, "Consulta");
            lector.Uses(webApplication, "Consulta");
            lector.Uses(landingPage, "Consulta");
            escritor.Uses(mobileApplication, "Consulta");
            escritor.Uses(webApplication, "Consulta");
            escritor.Uses(landingPage, "Consulta");                        

            mobileApplication.Uses(pagoContext,         "Request", "JSON/HTTPS");
            mobileApplication.Uses(calificacionesContext,         "Request", "JSON/HTTPS");
            mobileApplication.Uses(chatContext,   "Request", "JSON/HTTPS");
            mobileApplication.Uses(registroContext,       "Request", "JSON/HTTPS");
            mobileApplication.Uses(notificacionesContext,   "Request", "JSON/HTTPS");
            webApplication.Uses(pagoContext,            "Request", "JSON/HTTPS");
            webApplication.Uses(chatContext,      "Request", "JSON/HTTPS");
            webApplication.Uses(calificacionesContext,            "Request", "JSON/HTTPS");
            webApplication.Uses(registroContext,          "Request", "JSON/HTTPS");
             
            
            pagoContext.Uses(database1, "", "JDBC");
            registroContext.Uses(database2, "", "JDBC");
            notificacionesContext.Uses(database3, "", "JDBC");
            chatContext.Uses(database4, "", "JDBC");
            calificacionesContext.Uses(database5, "", "JDBC");
                        

            // Tags
            mobileApplication.AddTags("MobileApp");
            webApplication.AddTags("WebApp");
            landingPage.AddTags("LandingPage");
            database1.AddTags("Database");
            database2.AddTags("Database");
            database3.AddTags("Database");
            database4.AddTags("Database");
            database5.AddTags("Database");
            pagoContext.AddTags("BoundedContext");            
            registroContext.AddTags("BoundedContext");            
            notificacionesContext.AddTags("BoundedContext");            
            calificacionesContext.AddTags("BoundedContext");            
            chatContext.AddTags("BoundedContext");            

            styles.Add(new ElementStyle("MobileApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
            styles.Add(new ElementStyle("WebApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("LandingPage") { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });            
            styles.Add(new ElementStyle("Database") { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("BoundedContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });            

            ContainerView containerView = viewSet.CreateContainerView(librarySystem, "Contenedor", "Diagrama de contenedores");
            contextView.PaperSize = PaperSize.A4_Landscape;
            containerView.AddAllElements(); 
            
            // // 3. Diagrama de Componentes -> Traveler
            // Component domainLayerTraveler =         travelerContext.AddComponent("Domain Layer", "", "NodeJS (NestJS)");
            // Component travelerController =          travelerContext.AddComponent("Traveler Controller", "REST Api endpoints de travelers", "NodeJS (NestJS)");
            // Component travelerApplicationService =  travelerContext.AddComponent("Traveler Application Service", "Provee metodos para los datos de traveler", "NodeJS (NestJS)");
            // Component travelerRepository =          travelerContext.AddComponent("Traveler Repository", "Informacion de traveler", "NodeJS (NestJS)");
            // Component friendsRepository =           travelerContext.AddComponent("Friends Repository", "Informacion de los friends del traveler", "NodeJS (NestJS)");

            // mobileApplication.Uses(travelerController,"JSON");
            // webApplication.Uses(travelerController,"JSON");
            // travelerController.Uses(travelerApplicationService,"Usa");
            // travelerApplicationService.Uses(friendsRepository,"Usa");
            // travelerApplicationService.Uses(travelerRepository,"Usa");
            // travelerApplicationService.Uses(domainLayerTraveler,"Usa");
            // friendsRepository.Uses(database,"","JDBC");
            // travelerRepository.Uses(database,"","JDBC");
            
            // //tags
            // domainLayerTraveler.AddTags("Component");
            // travelerRepository.AddTags("Component");
            // travelerController.AddTags("Component");
            // travelerApplicationService.AddTags("Component");
            // friendsRepository.AddTags("Component");

            // //style
            // //styles.Add(new ElementStyle("Component") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            // ComponentView travelerComponentView = viewSet.CreateComponentView(travelerContext, "Traveler Components", "Component Diagram");
            // travelerComponentView.PaperSize = PaperSize.A4_Landscape;
            // travelerComponentView.Add(mobileApplication);   
            // travelerComponentView.Add(webApplication);
            // travelerComponentView.Add(database);
            // travelerComponentView.AddAllComponents();            

            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
        }
    }
}