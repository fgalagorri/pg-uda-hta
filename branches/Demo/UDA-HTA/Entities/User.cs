namespace Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; } //encriptado
        public string Role { get; set; }
        public bool Enabled { get; set; }
    }
}
