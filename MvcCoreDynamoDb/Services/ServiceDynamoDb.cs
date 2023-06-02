using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2;
using MvcCoreDynamoDb.Models;

namespace MvcCoreDynamoDb.Services
{
    public class ServiceDynamoDb
    {
        private DynamoDBContext context;

        public ServiceDynamoDb(DynamoDBContext context)
        {

            this.context = context;
        }

        public async Task CreateCocheAsync(Coche car)
        {
            await this.context.SaveAsync<Coche>(car);
        }

        public async Task DeleteCocheAsync(int idcoche)
        {
            await this.context.DeleteAsync<Coche>(idcoche);
        }

        public async Task<Coche> FindCocheAsync(int idcoche)
        {
            //CUANDO BUSCAMOS POR PARTITION KEY (HASH) EXISTE UN 
            //METODO QUE LO HACE POR MI...
            return await this.context.LoadAsync<Coche>(idcoche);
        }

        public async Task<List<Coche>> GetCochesAsync()
        {
            //LO PRIMERO ES RECUPERAR LA TABLA DE LOS OBJETOS
            Table tabla = this.context.GetTargetTable<Coche>();
            //PARA RECUPERAR TODOS O PARA BUSCAR, SE UTILIZA UN 
            //OBJETO LLAMADO ScanOperationConfig
            var operation = new ScanOperationConfig();
            //LO QUE DEBEMOS HACER ES CONFIGURAR DICHO OBJETO CON LO 
            //QUE DESEARAMOS BUSCAR, QUE EN ESTE CASO, ES NADA
            var results = tabla.Scan(operation);
            //LO QUE DEVUELVE ES SIEMPRE UN OBJETO DE LA CLASE 
            //Document
            List<Document> documents = await results.GetNextSetAsync();
            //CONVERTIMOS LOS Documents ENCONTRADOS A NUESTROS OBJETOS
            //Coche
            var cars = this.context.FromDocuments<Coche>(documents);
            return cars.ToList();
        }


        public async Task<List<Coche>> SearchCochesAsync(string marca)
        {
            //DEBEMOS CREAR UNA COLECCION DE CONDICIONES QUE PODRIAMOS 
            //ENVIAR A LA VEZ A NUESTRA BUSQUEDA PARA FILTRAR POR OR/AND
            List<ScanCondition> conditions =
                new List<ScanCondition>();
            conditions.Add(new ScanCondition("Marca", ScanOperator.Equal
                , marca));
            var cars = await this.context.ScanAsync<Coche>
                (conditions).GetRemainingAsync();
            return cars.ToList();
        }


    }
}
