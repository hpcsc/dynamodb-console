namespace DynamoDB.Console
{
    public class Song
    {
        private readonly string _artist;
        private readonly string _albumTitle;
        private readonly string _awards;
        private readonly string _songTitle;

        public Song(string artist, string albumTitle, string awards, string songTitle)
        {
            _artist = artist;
            _albumTitle = albumTitle;
            _awards = awards;
            _songTitle = songTitle;
        }

        public override string ToString()
        {
            return $"Song (Artist = {_artist}, AlbumTitle = {_albumTitle}, Awards = {_awards}, SongTitle = {_songTitle}";
        }
    }
}