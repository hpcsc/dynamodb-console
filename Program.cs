using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DynamoDB.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                IConfiguration config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
                var dynamoUrl = config["DYNAMO_URL"];
                var accessKey = config["AWS_ACCESS_KEY_ID"];
                var secretKey = config["AWS_SECRET_ACCESS_KEY"];
                System.Console.WriteLine($"=== Connecting to Dynamo DB at {dynamoUrl} with AWS access key id {accessKey}");
                
                var songRepository = new SongRepository(dynamoUrl, accessKey, secretKey);
                
                System.Console.WriteLine("===========");
                var songs = await songRepository.FindAll();
                songs.ToList().ForEach(System.Console.WriteLine);
                
                System.Console.WriteLine("===========");
                var song = await songRepository.FindByArtist("No One You Know");
                System.Console.WriteLine(song == null ? "No song with that name found" : song.ToString());
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
        }
    }
}
