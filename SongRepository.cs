using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace DynamoDB.Console
{
    public class SongRepository
    {
        private readonly string _serviceUrl;
        private readonly string _accessKey;
        private readonly string _secretKey;

        public SongRepository(string serviceUrl, string accessKey, string secretKey)
        {
            _serviceUrl = serviceUrl;
            _accessKey = accessKey;
            _secretKey = secretKey;
        }
        
        public async Task<IEnumerable<Song>> FindAll()
        {
            using (var client = CreateDynamoDbClient())
            {
                var tables = (await client.ListTablesAsync()).TableNames;
                var scanRequest = new ScanRequest
                {
                    TableName = "Music"
                };
                var response = await client.ScanAsync(scanRequest);

                return response.Items.Select(ToSongModel);
            }
        }

        public async Task<Song> FindByArtist(string artist)
        {
            using (var client = CreateDynamoDbClient())
            {
                var queryRequest = new QueryRequest
                {
                    TableName = "Music",
                    KeyConditionExpression = "Artist = :artist",
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                    {
                        {":artist", new AttributeValue {S = artist}}
                    }
                };
                
                var response = await client.QueryAsync(queryRequest);
                return response.Count == 0 ? null : ToSongModel(response.Items[0]);
            }
        }

        private static Song ToSongModel(Dictionary<string, AttributeValue> item)
        {
            return new Song
            (
                item["Artist"].S,
                item["AlbumTitle"].S,
                item["Awards"].N,
                item["SongTitle"].S
            );
        }

        private AmazonDynamoDBClient CreateDynamoDbClient()
        {
            var config = new AmazonDynamoDBConfig
            {
                ServiceURL = _serviceUrl
            };

            return new AmazonDynamoDBClient(new BasicAWSCredentials(_accessKey, _secretKey), config);
        }
    }
}