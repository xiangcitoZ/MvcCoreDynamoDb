using Amazon.DynamoDBv2.DataModel;

namespace MvcCoreDynamoDb.Models
{
    public class Motor
    {
        [DynamoDBProperty("velocidad")]
        public int Velocidad { get; set; }
        [DynamoDBProperty("cilindrada")]
        public int Cilindrada { get; set; }
        [DynamoDBProperty("tipo")]
        public string Tipo { get; set; }
    }

}
