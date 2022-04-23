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
            SoftwareSystem librarySystem = model.AddSoftwareSystem("Easy Store", "Plataforma web donde puedes publicar y leer historias de otros usuarios regitrados pudiendo suscribirse a contenido exclusivo");
                        
            Person escritor = model.AddPerson("Escritor", "Usuario capaz de publicar contenido textual.");
            Person lector = model.AddPerson("Lector", "Usuario que solo podra leer contenido y suscribirse");
            Person developer = model.AddPerson("Developer", "Developer - Open Data.");

            
            lector.Uses(librarySystem, "Realiza consultas para mantenerse al tanto de las publicaciones que puede leer");
            escritor.Uses(librarySystem, "Realiza consultas para mantenerse al tanto de los lectors que seleccionan su negocio");
            developer.Uses(librarySystem, "Realiza consultas a la REST API para mantenerse al tanto de los datos de las historias");

            
            SystemContextView contextView = viewSet.CreateSystemContextView(librarySystem, "Contexto", "Diagrama de contexto");
            contextView.PaperSize = PaperSize.A3_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // Tags
            escritor.AddTags("Ciudadano");
            lector.AddTags("Ciudadano");
            librarySystem.AddTags("SistemaLibros");
            developer.AddTags("Developer");


            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle("Ciudadano") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("SistemaLibros") { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Developer") { Background = "#facc2e", Shape = Shape.Robot });

            

            // 2. Diagrama de Contenedores
            Container mobileApplication =       librarySystem.AddContainer("Mobile App", "Permite a los usuarios visualizar las actividades turisticas que pueden realizar cercanas a su destino.", "Flutter");
            Container webApplication =          librarySystem.AddContainer("Web App", "Permite a los usuarios visualizar las actividades turisticas que pueden realizar cercanas a su destino.", "Vue");
            Container landingPage =             librarySystem.AddContainer("Landing Page", "", "Bootstrap");
            Container pagoContext =             librarySystem.AddContainer("Pagos Context", "Bounded Context del Microservicio de pagos de suscripciones", "NodeJS (NestJS)");
            Container registroContext =         librarySystem.AddContainer("Registro Context", "Bounded Context del Microservicio de registro de segmentos objetivos", "NodeJS (NestJS)");
            Container historiasContext =        librarySystem.AddContainer("Post Context", "Bounded Context del Microservicio de contenido de las historias", "NodeJS (NestJS)");
            Container calificacionesContext =   librarySystem.AddContainer("Calificaciones Context", "Bounded Context del microservicio de información de las calificaciones", "NodeJS (NestJS)");
            Container apiGateway =              librarySystem.AddContainer("API Gateway", "API Gateway", "Spring Boot port 8080");
            Container database1 =               librarySystem.AddContainer("Pagos DB", "", "MySQL");
            Container database2 =               librarySystem.AddContainer("Registro DB", "", "MySQL");
            Container database3 =               librarySystem.AddContainer("Post DB", "", "MySQL");
            Container database4 =               librarySystem.AddContainer("Calificaciones DB", "", "MySQL");
            Container database5 =               librarySystem.AddContainer("Post DB Replica", "", "MySQL");
            Container messageBus = librarySystem.AddContainer("Bus de Mensajes en Cluster de Alta Disponibilidad", "Transporte de eventos del dominio.", "RabbitMQ");

            
            

            lector.Uses(mobileApplication, "Consulta");
            lector.Uses(webApplication, "Consulta");
            lector.Uses(landingPage, "Consulta");
            escritor.Uses(mobileApplication, "Consulta");
            escritor.Uses(webApplication, "Consulta");
            escritor.Uses(landingPage, "Consulta");
            developer.Uses(apiGateway, "API Request", "JSON/HTTPS");
                     

            mobileApplication.Uses(apiGateway,"API Request", "JSON/HTTPS");
            webApplication.Uses(apiGateway,"API Request", "JSON/HTTPS");

            apiGateway.Uses(pagoContext,         "Request", "JSON/HTTPS");
            apiGateway.Uses(calificacionesContext,         "Request", "JSON/HTTPS");
            apiGateway.Uses(registroContext,       "Request", "JSON/HTTPS");
            apiGateway.Uses(historiasContext,   "Request", "JSON/HTTPS");

            pagoContext.Uses(database1, "", "JDBC");
            registroContext.Uses(database2, "", "JDBC");
            historiasContext.Uses(database3, "", "JDBC");
            historiasContext.Uses(database5, "", "JDBC");
            database3.Uses(database5, "", "JDBC");
            calificacionesContext.Uses(database4, "", "JDBC");
            
            pagoContext.Uses(messageBus,"Publica y consume eventos del dominio");
            registroContext.Uses(messageBus, "Publica y consume eventos del dominio");
            historiasContext.Uses(messageBus, "Publica y consume eventos del dominio");
            calificacionesContext.Uses(messageBus, "Publica y consume eventos del dominio");
                        
            // Tags
            mobileApplication.AddTags("MobileApp");
            webApplication.AddTags("WebApp");
            landingPage.AddTags("LandingPage");            
            apiGateway.AddTags("APIGateway");
            database1.AddTags("Database");
            database2.AddTags("Database");
            database3.AddTags("Database");
            database4.AddTags("Database");
            database5.AddTags("Database");
            pagoContext.AddTags("BoundedContext");            
            registroContext.AddTags("BoundedContext");            
            historiasContext.AddTags("BoundedContext");            
            calificacionesContext.AddTags("BoundedContext");
            messageBus.AddTags("MessageBus");


            styles.Add(new ElementStyle("MobileApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
            styles.Add(new ElementStyle("WebApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("LandingPage") { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("APIGateway") { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("Database") { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("BoundedContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MessageBus") { Width = 850, Background = "#fd8208", Color = "#ffffff", Shape = Shape.Pipe, Icon = "" });


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