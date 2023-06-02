using Amazon.DynamoDBv2.DataModel;

namespace MvcCoreDynamoDb.Models
{
    [DynamoDBTable("coches")]
    public class Coche
    {
        [DynamoDBHashKey]
        [DynamoDBProperty("idcoche")]
        public int IdCoche { get; set; }
       
        [DynamoDBProperty("marca")]
        public string Marca { get; set; }
        [DynamoDBProperty("modelo")]
        public string Modelo { get; set; }
        [DynamoDBProperty("imagen")]
        public string Imagen { get; set; }
        [DynamoDBProperty("motor")]
        public Motor Motor { get; set; }
    }


}
